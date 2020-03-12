using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainUI : MonoBehaviour
{
    // 下部按钮菜单
    public Button harvestBtn;
    public Button buyTowerBtn;
    public Button efficiencyX5Btn;
    public Button autoMergeBtn;
    public Button startBattle;

    // 顶部显示信息
    public Text coinText;
    public Text efficiencyText;
    public Text levelText;
    public Text diamondText;

    // 右侧展开菜单
    public Button menuBtn;

    // 收获金币的标志
    public Collector collector;
}
