using UnityEngine;
using QFramework;
using System.Collections;
public class DamageText2Pool : Singleton<DamageText2Pool> {
    public SimpleObjectPool<DamageText> dp; 
    public GameObject prefab;
    private DamageText2Pool() {
        prefab = (GameObject)Resources.Load("Prefabs/200k", typeof(GameObject));
        dp = new SimpleObjectPool<DamageText>(
            () => {
                var enemy = (GameObject)GameObject.Instantiate(prefab);
                enemy.SetActive(false);
                return enemy.GetComponent<DamageText>();
            }, 
            dt => {
                dt.InitDamage(100);    
            },
            50
        );
    }
}