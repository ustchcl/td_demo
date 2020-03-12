using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class OperationPopUp : MonoBehaviour
{
    #region UI
    public Button lvUpBtn;
    public Button detailBtn;
    public Button lvUpNowBtn;
    public GameObject lvMax;
    public Text diamondCostText;
    #endregion

    Building currentBuilding;

    // Start is called before the first frame update
    void Start()
    {
        lvUpBtn.onClick.AsObservable().Subscribe(_ =>
        {
            if (currentBuilding != null)
            {
                currentBuilding.Upgrade();
            }
        });
        detailBtn.onClick.AsObservable().Subscribe(_ =>
        {

        });
        lvUpNowBtn.onClick.AsObservable().Subscribe(_ =>
        {
            if (currentBuilding != null)
            {
                currentBuilding.UpgradeNow();
            }
        });
    }

    public void Init(Building building)
    {
        currentBuilding = building;
    }

    private void Update()
    {
        lvMax.SetActive(currentBuilding.IsMaxLevel);
        lvUpBtn.gameObject.SetActive(!currentBuilding.IsMaxLevel && !currentBuilding.IsUpgrading);
        lvUpNowBtn.gameObject.SetActive(currentBuilding.IsUpgrading);
        diamondCostText.text = string.Format("x{0}", currentBuilding.UpgradeNowCost());
    }
}
