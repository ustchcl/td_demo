using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;
using System.Reflection;

public class SimulationOfOperation : MonoBehaviour
{

    private TowerInUI[] towers;
    // private TimeItem genTowerTimeItem;
    public GameObject towersNode;
    public GameObject groundNode;

    public static SimulationOfOperation Instance; 

    public Text coinText;
    public Text diamondText;
    public Button buyBtn;
    private double _coinAmount = 0;
    private int buyIndex = 0;
    public double coinAmount {
        get { return _coinAmount; }
        set { 
            _coinAmount = value;
            coinText.text = string.Format("Coin: {0}", ToolBox.Instance.FormatNum(_coinAmount));
        }
    }

    private int _diamondAmount = 0;
    public int diamondAmount {
        get { return _diamondAmount; }
        set { _diamondAmount = value; diamondText.text = string.Format("Diamond: {0}", _diamondAmount);  }
    }
    public RawImage[] frames;
    // Start is called before the first frame update
    private void Awake() {
        Instance = this;
        diamondAmount = 3000;
    }
    void Start()
    {
        towers = new TowerInUI[10];   
        QEventSystem.Instance.Register(Constants.Instance.TOWER_MOVE_TO, OnTowerMoveTo); 
        QEventSystem.Instance.Register(Constants.Instance.HIGH_LIGHT, OnHighLight);
        // genTowerTimeItem = Timer.Instance.Post2Really(GenTower, 3f, -1);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    #region Events

    private int getPrice() {
        return 100 + 20 * buyIndex;
    }
    public void GenTower() {
        var index = ToolBox.Instance.FindIndex(towers, t => t.IsNull());
        if (index == -1) {
            return;
        }
        var price = getPrice();
        if (coinAmount < price) {
            return;
        }
        coinAmount -= price;
        buyIndex += 1;
        
        var tower = TowerInUIPool.Instance.ep.Allocate();
        tower.transform.SetParent(towersNode.transform);
        tower.gameObject.SetActive(true);
        tower.MoveToIndex(index);
        tower.selfIndex = index;
        towers[index] = tower;
    }


    void OnTowerMoveTo(int key, object[] msg) {
        frames.ForEach(f => f.color = Color.white);
        if (msg.Length < 2) {
            return;
        }
        int fromIndex = (int)msg[0];
        int toIndex = (int)msg[1];
        if (towers[fromIndex].IsNull()) {
            return;
        }
        if (toIndex == 10)
        {
            var towerInUI = towers[fromIndex];
            RecycleTower(towerInUI.level);
            towerInUI.gameObject.SetActive(false);
            TowerInUIPool.Instance.ep.Recycle(towerInUI);
            towers[fromIndex] = null;
            return;
        }
        if (towers[toIndex].IsNull()) {
            ToolBox.Instance.Swap(towers, fromIndex, toIndex);
            towers[toIndex].selfIndex = toIndex;
        } else {
            if (towers[fromIndex].level == towers[toIndex].level) {
                towers[toIndex].LevelUp();
                var towerInUI = towers[fromIndex];
                towerInUI.gameObject.SetActive(false);
                TowerInUIPool.Instance.ep.Recycle(towerInUI);
                towers[fromIndex] = null;
            } else {
                towers[fromIndex].selfIndex = toIndex;
                towers[toIndex].selfIndex = fromIndex;
                ToolBox.Instance.Swap(towers, fromIndex, toIndex);
                towers[fromIndex].MoveToIndex(fromIndex);
                towers[toIndex].MoveToIndex(toIndex);
            }
        }
        
    }

    void RecycleTower(int towerLevel)
    { 
        var mbGpi = ToolBox.Instance.FirstWhere(
            GoldPriceInfoFactory.Instance.GoldPriceInfoArray
            , (gpi) => gpi.id == buyIndex
        );
        mbGpi.Fold((gpi) =>
        {
            Log.I(towerLevel);
            Log.I(gpi.level1);
            Log.I(string.Format("level{0}", towerLevel));
            var propName = string.Format("level{0}", towerLevel);
            double coinGain = ToolBox.Instance.GetPropertyValue<double>(gpi, propName);
            coinAmount += (int)coinGain;
        }, () => { });
    }

    void OnHighLight(int key, object[] msg) {
        if (msg.Length < 1) {
            return;
        }
        int index = (int)(msg[0]);
        frames.ForEach(f => f.color = Color.white);
        frames[index].color = Color.red;
        
    }
    #endregion
}
