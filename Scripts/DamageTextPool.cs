using UnityEngine;
using QFramework;
using System.Collections;
public class DamageTextPool : Singleton<DamageTextPool> {
    public SimpleObjectPool<DamageText> ep; 
    public GameObject prefab;
    private DamageTextPool() {
        prefab = (GameObject)Resources.Load("Prefabs/100k", typeof(GameObject));
        ep = new SimpleObjectPool<DamageText>(
            () => {
                var enemy = (GameObject)GameObject.Instantiate(prefab);
                enemy.SetActive(false);
                return enemy.GetComponent<DamageText>();
            }, 
            dt => {
               dt.InitDamage(2000); 
            },
            50
        );
    }
}
