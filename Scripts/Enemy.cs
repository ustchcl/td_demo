using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

public class EnemyPool : Singleton<EnemyPool> {
    public SimpleObjectPool<Enemy> ep; 
    public GameObject prefab;
    private EnemyPool() {
        prefab = (GameObject)Resources.Load("Prefabs/Enemy2", typeof(GameObject));
        ep = new SimpleObjectPool<Enemy>(
            () => {
                var enemy = (GameObject)GameObject.Instantiate(prefab);
                enemy.SetActive(false);
                return enemy.GetComponent<Enemy>();
            }, 
            enemy => {
                enemy.Reset();
            },
            50
        );
    }
}

public class Enemy : MonoBehaviour {
    private float life = 3.0f;
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject cube; 
    // [SerializeField] BoxCollider bc;

    void OnCollisionEnter (Collision other) {
        if (!alive) {
            return;
        }
        TakeDamage();
        if (!alive) {
            QEventSystem.Instance.Send(2, 1);
            StartCoroutine(PlayDie());
        }
    }

    IEnumerator PlayDie() {
        cube.SetActive(false);
        ps.Play();
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
        cube.SetActive(true);
        EnemyPool.Instance.ep.Recycle(this);
    } 

    void TakeDamage() {
        life -= 1.0f;
    }

    public bool alive {
        get { return life > 0; }
    }

    public void Reset() {
        life = 4.0f;
        transform.position = new Vector3(Utils.GetInstance().Rand() * 10f - 5f, 0.5f, 10);
    }

    void Update() {
        if (!alive) {
            return;
        }
        if (GameManager.gameEnd) { 
            life = 0;
            StartCoroutine(PlayDie());
            return ; 
        }
        if (alive) {
            transform.Translate(Vector3.back * 0.05f);
            if (transform.position.z < -10) {
                QFramework.QEventSystem.Instance.Send(1, true, "42");
            }
        }
    }
}
