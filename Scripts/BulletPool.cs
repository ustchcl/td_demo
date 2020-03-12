using UnityEngine;
using QFramework;
using System.Collections;

public class BulletPool : Singleton<BulletPool> {
    public SimpleObjectPool<Bullet> bp; 
    public GameObject prefab;
    private BulletPool() {
        prefab = (GameObject)Resources.Load("Prefabs/Bullet", typeof(GameObject));
        bp = new SimpleObjectPool<Bullet>(() => {
            var bullet = ((GameObject)GameObject.Instantiate(prefab)).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            return bullet;
        }, (Bullet bullet) => {
            bullet.gameObject.SetActive(false);
            var tr = bullet.gameObject.GetComponent<TrailRenderer>() as TrailRenderer;
            tr.Clear();
            bullet.rb.Sleep();
        }, 50);
    }
}