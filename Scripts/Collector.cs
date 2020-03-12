using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class Collector : MonoBehaviour
{
    #region UI
    public Building[] buildings;
    public RectTransform self;
    public RectTransform[] coins;
    #endregion

    // Start is called before the first frame update

 

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            for (int i = 0; i < coins.Length; ++i)
            {
                if (coins[i].Overlaps(self))
                {
                    buildings[i].CollectCoin();
                }
            }
        }
    }
}
