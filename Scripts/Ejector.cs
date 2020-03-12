using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Ejector : MonoBehaviour
{
    private int Id = 0;
    private int remain = 0;
    private float Speed = 0.1f;
    LauncherInfo info;

    private TimeItem timer;
    private float DelayTime = 0.0f;
    private float VerticalSpd = 0.1f;
    public EjectorCarry carry;

    private float Pos; // 发射器x轴位置
        
    public void Init(int id, string json, float pos, float delay)
    {
        Pos = pos;
        DelayTime = delay;
        Id = id;
        carry = new EjectorCarry(json);
    }

    public void Reset() {
        carry.Reset();
        remain = carry.enemyInfoList.Aggregate(0, (acc, pairs) => acc + pairs.Aggregate(0, (acc2, p) => acc2 + p.Item2));
    }

    private void Start()
    {
        var arr = LauncherInfoFactory.Instance.LauncherInfoArray;
        info = Array.Find(arr, launchInfo => launchInfo.id == Id);
        if (info == null)
        {
            throw new Exception(string.Format("不存在ID={0}的配置", Id));
        }
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(DelayTime);
        /*
        var Amount = ToolBox.Instance.Fold(
            carry.enemyInfoList, 
            (sum, pairs) => sum + 
                ToolBox.Instance.Fold(
                    pairs, 
                    (sum_, pair) => sum_ + pair.Item2, 
                    0
                ), 
            0
        );
        */

        var Amount = 0;
        foreach (var pairs in carry.enemyInfoList)
        {
            foreach (var pair in pairs)
            {
                Amount += pair.Item2;
            }
        }
        remain = Amount;
        // carry.enemyInfoList.Aggregate(0, (acc, pairs) => acc + pairs.Aggregate(0, (acc2, p) => acc2 + p.Item2));
        Log.I("step={0}, amount={1}", info.interval, Amount);
        timer = Timer.Instance.Post2Really(GenerateEnemy, (float)info.interval, -1);
    }

    int GetEnemeyId()
    {
        var ins = ToolBox.Instance;
        foreach (var pairs in carry.enemyInfoList)
        {
            var arr = pairs.Where(p => p.Item2 != 0).ToList();
            if (arr.Count > 0)
            {
                var pair = arr[UnityEngine.Random.Range(0, arr.Count)];
                pair.Item2--;
                return pair.Item1;
            }
        }
        return -1;
    }

    public void GenerateEnemy(int tick)
    {
        if (GameManager.gameEnd || remain == 0)
        {
            // timer.Stop();
            return;
        }
        var enemyId = GetEnemeyId();
        Log.I("id: {0}", enemyId);

        var enemy = EnemyPool.Instance.ep.Allocate();
        enemy.gameObject.SetActive(true);
        
        enemy.Init(
            enemyId,
            UnityEngine.Random.Range((float)info.minTransverseSpeed, (float)info.maxTransverseSpeed), 
            (float)info.portraitSpeed
        );
        float posx = 0;
        if (Pos > 100) { // 随机
            posx = UnityEngine.Random.Range(0f, 8f);
        } else {
            posx = Pos / 100f * 8f;
        }
        enemy.transform.position = new Vector3(-4f + posx, 0.3f, 14);
        if (remain > 0)
        {
            remain--;
        }
    }


}
