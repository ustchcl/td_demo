using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using QFramework;

public class Building : MonoBehaviour
{
    public int BuildingId = 1;
    public Bounds bounds;

    public readonly BehaviorSubject<double> coinAmount = new BehaviorSubject<double>(0f);
    public readonly BehaviorSubject<float> coinProgressCount = new BehaviorSubject<float>(0f);
    public readonly float ProgressMax = 100f;
    
    public readonly BehaviorSubject<int> level = new BehaviorSubject<int>(0);
    private double PoolMax = 0f;
    private double AwardRate = 1f; // 产出模型表

    public double AwardPerSecond
    {
        get
        {
            return buildInfo.productCoefficient * AwardRate / buildInfo.time;
        }
    }
   
    

    public IObservable<UniRx.Unit> onClick;

    // subs
    List<System.IDisposable> subs = new List<System.IDisposable>();

    public ProgressBar bar { get; set; }
    // config
    private BulidInfo buildInfo;

    private bool isPlaying = false;

 

    void Awake()
    {
        ToolBox.Instance.FirstWhere(BulidInfoFactory.Instance.BulidInfoArray, build => build.id == BuildingId).Fold((bi) => {
            buildInfo = bi;
        }, () => {
            Log.E("[Building.cs] 配置表初始化错误");
        });
        onClick = gameObject.OnMouseUpAsObservable();
        
        level.Subscribe(l =>
        {
            AwardRate = ProduceModelFactory.Instance.ProduceModelArray.First((p) => p.id == l).num;
            PoolMax = buildInfo.productCoefficient * AwardRate * 7200 / buildInfo.time;
        });
        level.OnNext(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        subs.Add(
            Observable.Interval(System.TimeSpan.FromMilliseconds(100)).Subscribe(tick =>
            {
                if (CoinPoolIsFull)
                {
                    return;
                }
                coinProgressCount.Update(c =>
                {
                    var times = MainSceneManager.Instance.Effeciency5Times ? 5 : 1;
                    float step = ProgressMax / ((float)buildInfo.time * 10);
                    var result = c + step * times;
                    if (result >= ProgressMax)
                    {
                        AddCoinToPool(buildInfo.productCoefficient * AwardRate);
                        result -= ProgressMax;
                    }
                    return result;
                });
            })
        );
       
    }


    private void OnDestroy()
    {
        subs.ForEach(s => s.Dispose());
    }

    #region events
    public void CollectCoin()
    {
        if (coinAmount.Value > 0 && !isPlaying)
        {
            MainSceneManager.Instance.coinAmount += coinAmount.Value;
            coinAmount.Update(_ => 0);
        }
    }

    public void Upgrade() 
    {
        StartCoroutine(LevelUpCounterDown());
        IsUpgrading = true;
    }

    public void UpgradeNow() {
        var requiredAmount = UpgradeNowCost();
        if (MainSceneManager.Instance.diamondAmount < requiredAmount)
        {
            Log.I("[ERROR]: 钻石不足");
            return;
        }
        MainSceneManager.Instance.diamondAmount -= requiredAmount;
        completeNow = true;
    }

    private IEnumerator LevelUpCounterDown()
    {
        if (bar != null)
        {
            int timeRange = System.Array.Find(UpgradeTimeFactory.Instance.UpgradeTimeArray, (ut) => ut.id == level.Value).upgradeTime;
            int count = timeRange;
            bar.gameObject.SetActive(true);
            while (count > 0 && !completeNow)
            {
                bar.progress = (timeRange - count) / (float)timeRange;
                count -= 1;
                yield return new WaitForSeconds(1f);
            }
            bar.gameObject.SetActive(false);

            doUpgrade();
        }
    }

    private void doUpgrade()
    {
        level.Update(_ => _ + 1);
        completeNow = false;
        IsUpgrading = false;
    }

    void AddCoinToPool(double amount)
    {
        coinAmount.Update(_ => ToolBox.Instance.Min(_ + amount, PoolMax));
    }
    #endregion

    #region flags
    public bool IsMaxLevel
    {
        get { return level.Value == buildInfo.limitLevel; }
    }

    public bool IsUpgrading = false;

    public bool CoinPoolIsFull
    {
        get { return coinAmount.Value >= PoolMax; }
    }

    private bool completeNow = false;

    public int UpgradeNowCost()
    {
        return ToolBox.Instance.FirstWhere(UpgradeTimeFactory.Instance.UpgradeTimeArray, (ut) => ut.id == level.Value).Map(ut => ut.finishDiamonds).GetOrElse(0);
    }
    #endregion
}
