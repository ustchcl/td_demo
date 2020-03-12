using UnityEngine;
using QFramework;
using System.Collections;
public class TowerInUIPool : Singleton<TowerInUIPool> {
    public SimpleObjectPool<TowerInUI> ep; 
    public GameObject prefab;
    private TowerInUIPool() {
        prefab = (GameObject)Resources.Load("Prefabs/TowerInUI", typeof(GameObject));
        ep = new SimpleObjectPool<TowerInUI>(
            () => {
                var go = (GameObject)GameObject.Instantiate(prefab);
                go.SetActive(false);
                var tower = go.GetComponent<TowerInUI>();
                tower.level = 1;
                return tower;
            }, 
            tower => {
                tower.level = 1;
            },
            50
        );
    }
}

