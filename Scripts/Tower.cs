using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class Tower : MonoBehaviour
{

    private float spd = 20f;
    public int Index = 0;
    private Vector3 sourceVec3 = new Vector3(0, 0, 10);

    private TimeItem item;

    private float shotSpd = 0.1f;
    private double shotDamge = 1;
    // Start is called before the first frame update

    void Start()
    {
        shotSpd = Constants.Instance.shotSpds[Index];
        shotDamge = Constants.Instance.bulletDamage[Index];
        item = Timer.Instance.Post2Really(Shot, 1/shotSpd, -1);
    }

    // Update is called once per frame

    private void Shot(int tick) {
        if (GameManager.gameEnd) {
            return;
        }
        if (GameManager.Instance.shotMode == GameManager.ShotMode.STOP) {
            return;
        } else if (GameManager.Instance.shotMode == GameManager.ShotMode.AUTOSHOT) {
            AutoShot();
        } else {
            ShotTo(GameManager.Instance.shotTo);
        }
    }

    public void ShotTo(Vector3 shotTo) {
        var bp = BulletPool.Instance.bp;
        var bullet = bp.Allocate();
        bullet.gameObject.SetActive(true);
        bullet.Damage = shotDamge;
        var direction = (shotTo - transform.position);
        direction.y = 0;
        direction = direction.normalized;
        var shotDirection = Vector3.zero;
        shotDirection.x = direction.x * spd;
        shotDirection.z = direction.z * spd;
        Log.I(shotDirection);
        bullet.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        bullet.ApplyForce(shotDirection);
    }


    public void AutoShot() {
        var bp = BulletPool.Instance.bp;
        var bullet = bp.Allocate();
        bullet.gameObject.SetActive(true);
        bullet.Damage = shotDamge;
        bullet.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        bullet.ApplyForce(new Vector3((Utils.GetInstance().Rand() - 0.5f) * 2 * spd, 0, spd));
    }
}
