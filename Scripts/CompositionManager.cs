using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using QFramework;
using UniRx;

public class CompositionManager : MonoBehaviour
{
    public TowerComposition[] towers;

    public UnityEngine.UI.Button buyBtn;

    private int buyIndex = 0;

    private void Start() {
        QEventSystem.Instance.Register(Constants.Instance.TOWER_MOVE_TO, OnTowerMoveTo);
        buyBtn.onClick.AsObservable().Subscribe(_ => BuyTower());
    }

    void BuyTower() {
        var index = ToolBox.Instance.FindIndex(towers, t => t.IsEmpty);
        Log.I("index = {0}", index);
        if (index == -1) {
            return;
        }
        var price = getPrice();
        if (MainSceneManager.Instance.coinAmount < price) {
            buyIndex++;
        }
        MainSceneManager.Instance.coinAmount -= price;
        towers[index].Reborn();
    }

    private int getPrice() {
        return 100 + 20 * buyIndex;
    }

    void OnTowerMoveTo(int key, object[] msg) {
        if (msg.Length < 2) {
            return;
        }
        int fromIndex = (int)msg[0];
        int toIndex = (int)msg[1];
        if (towers[fromIndex].IsEmpty) {
            return;
        }
        if (towers[toIndex].IsEmpty) {
            ToolBox.Instance.Swap(towers, fromIndex, toIndex);
            towers[fromIndex].MoveToIndex(fromIndex);
            towers[toIndex].MoveToIndex(toIndex);
            
        } else {
            if (towers[fromIndex].level == towers[toIndex].level) {
                towers[toIndex].LevelUp();
                var tower = towers[fromIndex];
                tower.MoveToIndex(fromIndex); // 滚回原位置
                tower.Destroy();
            } else {
                ToolBox.Instance.Swap(towers, fromIndex, toIndex);
                towers[fromIndex].MoveToIndex(fromIndex);
                towers[toIndex].MoveToIndex(toIndex);
            }
        }
        
    }
}
