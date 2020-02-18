using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

public class Bullet : MonoBehaviour, IPoolable
{
    public Rigidbody rb;
    public float life = 99f;

    void OnCollisionEnter (Collision other) {
        life -= 1;
    }
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.gameEnd) {
            SafeObjectPool<Bullet>.Instance.Recycle(this);
            return;
        }
        if (NeedRecycle()) {
            SafeObjectPool<Bullet>.Instance.Recycle(this);
        }
    }

    bool NeedRecycle() {
        return life <= 0 || Math.Abs(transform.position.z) >= 15;
    }

    public void ApplyForce(Vector3 force) {
        rb.AddForce(force, ForceMode.Impulse);
    }

    #region  pool
    public void OnRecycled() {
        gameObject.SetActive(false);
    }

    private bool isrecyscled;
    public bool IsRecycled { get { return isrecyscled; } set { isrecyscled = value; } }
    #endregion
}