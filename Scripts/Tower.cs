using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class Tower : MonoBehaviour
{

    private float spd = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShotTo(SimpleObjectPool<Bullet> bp, Vector3 shotTo) {
        var bullet = bp.Allocate();
        bullet.gameObject.SetActive(true);
        Vector2 direction = ((new Vector2(shotTo.x, shotTo.y)) - (new Vector2(Screen.width/2 + (transform.position.x / 5) * Screen.width/2, 0))).normalized;
        var shotDirection = Vector3.zero;
        shotDirection.x = direction.x * spd;
        shotDirection.z = direction.y * spd * 0.95f;
        bullet.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        bullet.ApplyForce(shotDirection);
    }


    public void AutoShot(SimpleObjectPool<Bullet> bp) {
        var bullet = bp.Allocate();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        bullet.ApplyForce(new Vector3((Utils.GetInstance().Rand() - 0.5f) * 2 * spd, 0, spd));
    }
}
