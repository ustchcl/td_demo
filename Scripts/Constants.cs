using QFramework;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Constants : Singleton<Constants> {
    private Constants() { }

    public int TOWER_MOVE_TO {
        get { return 4; }
    }

    public int HIGH_LIGHT {
        get { return 5; }
    }

    public int HIDE_BUILDING_SUBPANEL
    {
        get { return 6; }
    }

    public int BUILDING_ON_CLICK
    {
        get { return 7; }
    }

    #region  Ground Property
    
    public readonly int MonsterStartZ = 10;
    public readonly int MonsterEndZ = -10;
    #endregion
    #region 模拟器属性
    // int useConfig = 0; // 1 启动， 0 不启动
    int LevelId = 10;  // 要启动的关卡ID
    public List<int> TowerIds = new List<int>(
        // 从左到右填写践踏的等级
        new int[] { 1, 2, 1, 2, 1 }
    );
    // 怪物 [min, max)
    public const int MONSTER_LIFE_MAX = 100;   // 血量最大值
    public const int MONSTER_LIFE_MIN = 80;    // 血量最小值


    private List<int> towerIds = new List<int>(new int[]{1, 2, 3, 2, 1}); 
    public float[] shotSpds {
        get {
            return towerIds.Select(id => {
                var b = Array.Find(BartizanInfoFactory.Instance.BartizanInfoArray, bi => bi.id == id);
                Log.I("b.spd = {0}", b.Spd);
                return (float)b.Spd;
            }).ToArray();
        }
    }

    public double[] bulletDamage {
        get {
            return towerIds.Select(id => {
                var b = Array.Find(BartizanInfoFactory.Instance.BartizanInfoArray, bi => bi.id == id);
                return (double)b.Dps;
            }).ToArray();
        }
    }
    // = new float[] { 2f, 2f, 3f, 3f, 1f }; // {} 内为5个数字  意涵为每秒发射的次数
    // public int[] bulletDamage = new int[] { 10, 10, 20, 10, 10 }; // {} 内为5个数字

    public CustomsInfo  LevelInfo
    {
        get
        {
            return ToolBox.Instance.FirstWhere(CustomsInfoFactory.Instance.CustomsInfoArray, ci => ci.id == LevelId).GetOrElse(null);
        }    
    }
    public int GetMonsterLife()
    {
        return UnityEngine.Random.Range(MONSTER_LIFE_MIN, MONSTER_LIFE_MAX);
    }
    public List<float> ShotSpds { get { return new List<float>(shotSpds); } }
    public List<double> BulletDamage { get { return new List<double>(bulletDamage); } }

    #endregion


}