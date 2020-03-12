using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System.Linq;
using UniRx;
using System;

public class MainSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Building[] buildings;
    public RectTransform popUp;
    public OperationPopUp operationPopUp;
    public GameObject ground;
    // 和building一一对应
    public RectTransform[] coins;
    private CoinProgress[] coinProgresses;
    public ProgressBar[] bars;
    private RectTransform[] barRectTransforms;

    public AutoDoController controller;
    public MainUI mainUI;

    public Collector collector;

    private bool _show = false;

    private List<IDisposable> subs;

    #region 常量
    private readonly float minDistance = 300f;
    private readonly float maxDistance = 900f;
    private readonly float scaleByDistance = 0.5f;
    #endregion

    public static MainSceneManager Instance;

    #region state
    private int buyIndex = 0;
    private double _coinAmount = 3000;
    public double coinAmount
    {
        get { return _coinAmount; }
        set
        {
            _coinAmount = value;
            mainUI.coinText.text = ToolBox.Instance.FormatNum(value);
        }
    }

    private int _diamondAmount = 3000;
    public int diamondAmount
    {
        get { return _diamondAmount; }
        set
        {
            _diamondAmount = value;
            mainUI.diamondText.text = ToolBox.Instance.FormatNum(value);
        }
    }


    public bool Effeciency5Times = false;
    private double _sumEffecency = 0f;
    public double SumEffecency { 
        get
        {
            return _sumEffecency;
        }
        set
        {
            _sumEffecency = value;
            mainUI.efficiencyText.text = "+" + ToolBox.Instance.FormatNum(value) + "/秒";
        }
    }
    #endregion

    private bool show
    {
        get { return _show; }
        set
        {
            _show = value;
            popUp.gameObject.SetActive(_show);
        }
    }

    private Building currentBuilding;

    private void Awake()
    {
        coinProgresses = coins.Select(b => b.gameObject.GetComponent<CoinProgress>()).ToArray();
        barRectTransforms = bars.Select(b => b.gameObject.GetComponent<RectTransform>()).ToArray();
        Instance = this;
    }

    void Start()
    {

        // player info
        diamondAmount = 100;
        coinAmount = 5000;

        Observable.Merge(buildings.Select(b => b.level.AsObservable())).Subscribe(_ => SumEffecency = calculateEffecency());
        subs = buildings.Select(b =>
        {
            return b.onClick.Subscribe(_ =>
            {
                if (popUp.gameObject.activeSelf)
                {
                    controller.Cancel();
                }
                else
                {
                    currentBuilding = b;
                    controller.Reset();
                    UpdatePopUP();
                    operationPopUp.Init(b);
                }
            });
        }
        ).ToList();

        ground.transform.ObserveEveryValueChanged(x => x.position).Subscribe(y =>
        {
            if (show)
            {
                UpdatePopUP();
            }
            UpdateCoinPos();
        });

        controller.Init(_ => show = _, 5);

        popUp.gameObject.SetActive(false);

        ToolBox.Instance.Range(0, buildings.Length).ForEach(i =>
        {
            buildings[i].bar = bars[i];
            buildings[i].coinAmount.Subscribe(c =>
            {
                coinProgresses[i].coinText.gameObject.SetActive(c > 0);
                coinProgresses[i].coinText.text = ToolBox.Instance.FormatNum(c);
            });
            bars[i].gameObject.SetActive(false);
            buildings[i].coinProgressCount.Subscribe(c => coinProgresses[i].progress = c / buildings[i].ProgressMax);
        });

        // events
        mainUI.harvestBtn.onClick.AsObservable().Subscribe(_ =>
        {

        });
        mainUI.efficiencyX5Btn.onClick.AsObservable().Subscribe(_ =>
        {
            if (Effeciency5Times)
            {
                return;
            } else
            {
                StartCoroutine(EffeciencyX5());
            }
        });

    }

    void UpdatePopUP()
    {
        if (currentBuilding == null)
        {
            return;
        }
        var pos = currentBuilding.gameObject.transform.position;
        var pos2 = Camera.main.WorldToScreenPoint(pos);
        var distancePos = new Vector3(pos.x, pos2.y, pos2.z);
        var distance = (pos - distancePos).magnitude;
        var scale = (distance - minDistance) * scaleByDistance / (maxDistance - minDistance);
        popUp.Position(pos2);
        popUp.localScale = new Vector2(1 - scale, 1 - scale);
    }

    void UpdateCoinPos()
    {
        ToolBox.Instance.Range(0, coins.Length).ForEach(i =>
        {
            var pos = buildings[i].gameObject.transform.position;
            var pos2 = Camera.main.WorldToScreenPoint(pos);
            var distancePos = new Vector3(pos.x, pos2.y, pos2.z);
            var distance = (pos - distancePos).magnitude;
            var scale = (distance - minDistance) * scaleByDistance / (maxDistance - minDistance);
            coins[i].Position(pos2 + Vector3.up * 40);
            coins[i].localScale = new Vector2(1 - scale, 1 - scale);

            barRectTransforms[i].Position(pos2);
            barRectTransforms[i].localScale = new Vector2(1 - scale, 1 - scale);
        });
    }


    private void OnDestroy()
    {
        subs.ForEach(sub => sub.Dispose());
    }

    IEnumerator EffeciencyX5()
    {
        Effeciency5Times = true;
        yield return new WaitForSeconds(10f);
        Effeciency5Times = false;
    }

    public double calculateEffecency()
    {
        return buildings.Select(b => b.AwardPerSecond).Aggregate((x, y) => x + y);
    }

    public void OnTouchStartHarvest() {
        collector.gameObject.SetActive(true);
        var rect = collector.GetComponent<RectTransform>();
        rect.Position(Input.mousePosition);
    }
}
