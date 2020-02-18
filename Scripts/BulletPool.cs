
using UnityEngine;
using QFramework;
using UniRx;
using System.Collections.Generic;
using System;


public class  BulletPool : MonoBehaviour {
    List<Bullet> mObjList = new List<Bullet>();

    private void Start()
    {
        this.Repeat()
            .Until(() => { return Input.GetKeyDown(KeyCode.Space); })
            .Event(() =>
            {   
                Log.I("util, event space");
                var temp = SafeObjectPool<Bullet>.Instance.Allocate();
                mObjList.Add(temp);
            })
            .Begin();

        Observable.EveryUpdate()
            .Where(x => Input.GetKeyDown(KeyCode.C) && mObjList.Count > 0)
            .Subscribe(_ => {
                Log.I("C");
                SafeObjectPool<Bullet>.Instance.Recycle(mObjList[0]); 
                mObjList.RemoveAt(0); 
                Debug.Log("回收"); 
            });

    }


}