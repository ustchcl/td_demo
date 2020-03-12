using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class EnemyPool : Singleton<EnemyPool> {
    public SimpleObjectPool<Monster> ep; 
    public GameObject prefab;
    private EnemyPool() {
        prefab = (GameObject)Resources.Load("Prefabs/Zombie", typeof(GameObject));
        ep = new SimpleObjectPool<Monster>(
            () => {
                var go = (GameObject)GameObject.Instantiate(prefab);
                go.SetActive(false);
                var enemy = go.GetComponent<Monster>();
                enemy.Reset();
                return enemy;
            }, 
            enemy => {
                enemy.Reset();
            },
            50
        );
    }
}

public class Monster : MonoBehaviour {
    private double life = 5.0;
    private double maxLife = 5.0;
    [SerializeField] ParticleSystem ps;
    [SerializeField] public GameObject zombieModel; 
    [SerializeField] Scrollbar lifeBar;

    public float spd = 0.02f;
    public Vector3 spdDirection = Vector3.down;

    private int _showNum = 0;

    private ShakeAnimation sa;

    // public Renderer _renderer;

    private int id = 1;

    #region CONSTANTS
    float Sin20 = Mathf.Sin(20f / 180f);
    float Cos20 = Mathf.Cos(20f / 180f);
    #endregion

    public int showNum {
        get { return _showNum; } set { _showNum = Mathf.Max(0, value); lifeBar.gameObject.SetActive(_showNum > 0); }
    }


    void Start () {
        showNum = 0;
        sa = GetComponent<ShakeAnimation>();
    }

    // [SerializeField] BoxCollider bc;

    void OnCollisionEnter (Collision other) {
        if (!alive) {
            return;
        }
        Bullet b = other.gameObject.GetComponent<Bullet>();
        if (b == null)
        {
            return;
        }
        TakeDamage(b.Damage);
        if (!alive) {
            QEventSystem.Instance.Send(2, 1);
            StartCoroutine(PlayDie());
        }
    }

    IEnumerator PlayDie() {
        zombieModel.SetActive(false);
        ps.Play();
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
        zombieModel.SetActive(true);
        EnemyPool.Instance.ep.Recycle(this);
    } 

    void TakeDamage(double damage) {
        StartCoroutine(ChangeColor());
        StartCoroutine(ShowLifeBar());
        if (Utils.GetInstance().Rand() < 0.2) {
            StartCoroutine(PlayDamage2(damage));
            life -= 2.0f * damage;
            StartCoroutine(sa.Shake(0.15f, 0.08f));
        } else {
            life -= (float)damage;
            StartCoroutine(PlayDamage(damage));
        }
        lifeBar.size = (float)(life / maxLife);
    }

    private IEnumerator ChangeColor() {
        // _renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        // _renderer.material.color = Color.green;
    }

    private IEnumerator ShowLifeBar() {
        showNum += 1;
        yield return new WaitForSeconds(2.0f);
        showNum -= 1; 
    }

    private IEnumerator PlayDamage(double damage) {
        lifeBar.gameObject.SetActive(true);
        var dt = DamageTextPool.Instance.ep.Allocate();
        dt.transform.parent = transform;
        dt.InitDamage(damage);
        dt.gameObject.SetActive(true);
        dt.Play();
        yield return new WaitForSeconds(0.25f);
        dt.gameObject.SetActive(false);
        DamageTextPool.Instance.ep.Recycle(dt); 
    }
    private IEnumerator PlayDamage2(double damage) {
        var dt = DamageText2Pool.Instance.dp.Allocate();
        dt.transform.SetParent(transform);
        dt.InitDamage(damage, 2);
        dt.gameObject.SetActive(true);
        dt.Play();
        yield return new WaitForSeconds(0.25f);
        dt.gameObject.SetActive(false);
        DamageText2Pool.Instance.dp.Recycle(dt); 
    }

    public bool alive {
        get { return life > 0; }
    }

    public void Reset() {
        transform.position = new Vector3(Utils.GetInstance().Rand() * 10f - 5f, 0.5f, 10);
        lifeBar.size = 1;
        showNum = 0;
        // InitSpd();
        InitLife();
    }

    public void Init(int id, float spdX, float spdY) {
        this.id = id;
        spdDirection.x = spdX;
        spdDirection.y = 0;
        spdDirection.z = - spdY;
        InitLife();
    }

    public void InitLife() {
        var mbMonster = ToolBox.Instance.FirstWhere(MonsterInfoFactory.Instance.MonsterInfoArray, mi => mi.id == id);
        mbMonster.Fold(m =>
        {
            life = maxLife = ((double)UnityEngine.Random.Range(0.8f, 1.2f)) * m.Hp;
        }, () => { });
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
            transform.Translate(spdDirection);
            if (Mathf.Abs(transform.position.x) > 3.5 && transform.position.x * spdDirection.x > 0) {
                spdDirection.x *= -1;
            }

            if (transform.position.z < -10) {
                QEventSystem.Instance.Send(1, true, "42");
            }
        }
    }

    
}
