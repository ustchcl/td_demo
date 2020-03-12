using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

public class Building3 : MonoBehaviour
{
    #region  UI
    public Button levelUpBtn;
    public Text levelUpCounterDownText;
    public Image progressImage;
    public Canvas subPanel;
    public RectTransform actionButtonsPos;
    public Button completeNowBtn;
    public Button collectCoinBtn;

    public ProgressBar progressBar;
    public new Renderer renderer;
    public Animator ani;
    public Text nameText;
    [Range(1, 8)]
    public int buildingId;
    #endregion


    #region  state
    public Text coinAmountText;
    private double _coinAmount = 0;
    public double coinAmount {
        get { return _coinAmount; }
        set {
            _coinAmount = value;
            coinAmountText.gameObject.SetActive(value > 0);
            coinAmountText.text = ToolBox.Instance.FormatNum(_coinAmount);
        }
    }

    private TimeItem timeItem;
    private int _timerCount = 0;
    
    private int period = 5;
    private int timerCount {
        get {return _timerCount; }
        set { _timerCount = value; progressImage.fillAmount = _timerCount/ (period * 10f); }
    }

    private int _level = 1;
    private int level {
        get { return _level; }
        set {
            _level = value;
        }
    }

    private bool _showPopup;
    public bool showPopup {
        get { return _showPopup; }
        set {
            _showPopup = value;
            if (_showPopup) {
                controller.Reset();
            } else {
                controller.Cancel();
            }
        }
    }
    public AutoDoController controller;

    private bool completeNow = false;

    private BulidInfo buildInfo;
    #endregion

    int getCoinPerPeriod() {
        return _level * 20 + 100;
    }
    void Awake() {
        ToolBox.Instance.FirstWhere(BulidInfoFactory.Instance.BulidInfoArray, build => build.id == buildingId).Fold((bi) => {
            buildInfo = bi;
        }, () => { 
            Log.E("[Building.cs] 配置表初始化错误");
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        coinAmount = 0;
        period = Random.Range(5, 10);
        timeItem = Timer.Instance.Post2Really(OnTick, 0.1f, -1);
        levelUpBtn.gameObject.SetActive(true);
        levelUpCounterDownText.gameObject.SetActive(false);
        controller.Init((visible) => {
            _showPopup = visible;
            subPanel.gameObject.SetActive(visible);
        }, 20f);
        QEventSystem.Instance.Register(Constants.Instance.HIDE_BUILDING_SUBPANEL, (int key, object[] msg) =>
        {
            showPopup = false;
        });
    }
    private void OnGUI() {
        Vector3 worldPos = gameObject.transform.position;
        Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);
        actionButtonsPos.Position(pos);
    }


    #region Events 
    public void LevelUp() {
        StartCoroutine(LevelUpCounterDown());
        levelUpBtn.gameObject.SetActive(false);
        levelUpCounterDownText.gameObject.SetActive(true);
    }

    private IEnumerator LevelUpCounterDown() {
        int timeRange = System.Array.Find(UpgradeTimeFactory.Instance.UpgradeTimeArray, (ut) => ut.id == level).upgradeTime;
        int count = timeRange;
        progressBar.gameObject.SetActive(true);
        while (count > 0 && !completeNow) {
            progressBar.progress = (timeRange - count) / (float)timeRange;
            count -= 1;
            levelUpCounterDownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
        progressBar.gameObject.SetActive(false);

        doLevelUp();
    }

    private void doLevelUp() {
        level += 1;
        completeNow = false;
        levelUpBtn.gameObject.SetActive(level < buildInfo.limitLevel);
        progressBar.gameObject.SetActive(false);
    }

    public void CollectCoin() {
        if (coinAmount <= 0 || isPlaying) {
            return;
        }
        StartCoroutine(PlayTween());
    }

    private void _doCollect() {
        SimulationOfOperation.Instance.coinAmount += (int)coinAmount;
        coinAmount = 0;
    }

    private bool isPlaying = false;
    private IEnumerator PlayTween() {
        isPlaying = true;
        ani.Play("gain_coin");
        yield return new WaitForSeconds(0.55f);
        _doCollect();
        isPlaying = false;

    }

    public void OnClick() {
        if (!showPopup)
        {
            QEventSystem.Instance.Send(Constants.Instance.HIDE_BUILDING_SUBPANEL);
        }
        showPopup = !showPopup;
        
    }

    public void CompleteNow() {
        var requiredAmount = ToolBox.Instance.FirstWhere(UpgradeTimeFactory.Instance.UpgradeTimeArray, (ut) =>  ut.id == level).Map(ut => ut.finishDiamonds).GetOrElse(0);
        if (SimulationOfOperation.Instance.diamondAmount < requiredAmount) {
            Log.I("[ERROR]: 砖石不足");
            return;
        }
        SimulationOfOperation.Instance.diamondAmount -= requiredAmount;
        completeNow = true;
    }

    void OnTick(int tick) {
        if (timerCount == period * 10 - 1) {
            coinAmount += getCoinPerPeriod();
        }
        timerCount = (timerCount + 1) % (period * 10);
        
    }

    void AddToCoinPool(double amount) {
        double max = buildInfo.productCoefficient * level * 7200 / buildInfo.time;
        coinAmount = ToolBox.Instance.Min(coinAmount + amount, max);
    }

    
    #endregion
}
