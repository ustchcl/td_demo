using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using UniRx;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {
    #region UI
    public Bullet bulletModel;
    public List<Tower> towers;
    public GameObject ejectorPrefab;
    #endregion
    public enum ShotMode {AUTOSHOT = 0, STOP = 1, SHOTTO = 2};
    public ShotMode shotMode = ShotMode.STOP;
    private Utils utils;

    private TimeItem m_RepeatTimeItem;    
    private TimeItem enemy_TimeItem;

    public Vector3 shotTo = new Vector3(0, 0, 10); 
    private Vector3 sourceVec3 = new Vector3(0, 0, 10);

    private double cos15 = Math.Cos(15.0 / 180.0);


    private SimpleObjectPool<Bullet> bp;
    private SimpleObjectPool<Monster> ep;


    float spd = 1f;

    public static bool gameEnd = true;
    public int _score = 0;

    public GameObject gameover ;
    public GameObject gamestart;
    public Text scoreText;

    public  int score {
        get { return _score; } set { _score = value; scoreText.text = String.Format("Score: {0}", _score); }
    }

    public float shotRate = 0.3f;
    public float enemyGenRate = 15f;

    public static GameManager Instance;

    private Ejector[] ejectors;
    

    void Awake() {
        QEventSystem.Instance.Register(1, OnEvent);
        QEventSystem.Instance.Register(2, OnScoreChange);
        InitBattle();
        GameManager.Instance = this;
    }
    void InitBattle() {
        CustomsInfo info = Constants.Instance.LevelInfo;
        Log.I("本关为id={1}, name={2}, fc={0}, desc={3}", info.Fc, info.id, info.name, info.description);
        ejectors = info.launch.Select(l => {
            GameObject go = Instantiate(ejectorPrefab);
            var ejector = go.GetComponent<Ejector>();
            ejector.Init(l.id, l.value, (float)l.initialPosition, (float)l.delay);
            return ejector;
        }).ToArray();
    }

    void OnDistroy() {
        QEventSystem.Instance.UnRegister(1, OnEvent);
        QEventSystem.Instance.UnRegister(2, OnScoreChange); 
    }

    void OnEvent(int key, object[] msg) {
       // Log.I("Key: {0}, msg: {1}", key, gameOverMsg[0]); 
       gameEnd = msg[0] == null ? false : (bool)msg[0];
       if (gameEnd) {
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
            gameover.SetActive(true);
       }
    }   

    void OnScoreChange(int key, object[] msg) {
        score = score + 1;
    }

    void Start() {
        utils = Utils.GetInstance();
        // 子弹速度E:\code\unity\td_demo\Assets\Scripts\Enemy.cs
        // m_RepeatTimeItem = Timer.Instance.Post2Really(OnTimeTick, shotRate, -1);
        // 怪物速度
        // enemy_TimeItem = Timer.Instance.Post2Really(GenEnemy, 1.1f, -1);
        shotMode = ShotMode.AUTOSHOT;
        shotTo = sourceVec3;
        // bp = new SimpleObjectPool<Bullet>(() => {
        //     var bullet = Instantiate(bulletModel);
        //     bullet.gameObject.SetActive(false);
        //     return bullet;
        // }, (Bullet bullet) => {
        //     bullet.gameObject.SetActive(false);
        //     var tr = bullet.gameObject.GetComponent<TrailRenderer>() as TrailRenderer;
        //     tr.Clear();
        //     bullet.rb.Sleep();
        // }, 50);

        // 初始化发射


        
    }

    // private void OnTimeTick(int tick) {
    //     if (gameEnd) {
    //         return;
    //     }
    //     Shot();
    // }

    private void GenEnemy(int tick) {
        if (gameEnd) {
            return;
        }
        Utils.GetInstance().Range(0, 8).ForEach(i => {
            if (utils.Rand() < 0.33) {
                return;
            }
            var enemy = EnemyPool.Instance.ep.Allocate();
            enemy.gameObject.SetActive(true);
            enemy.transform.position = new Vector3(4f - i * 1.2f, 0.3f, 14);
        });
        
    }

    void Update() {
        if (gameEnd) { return ; }
        if (Input.GetKey(KeyCode.Mouse0)) {
            shotTo = ToolBox.Instance.ScrrenToGroundPoint(0.2f);
            shotMode = ShotMode.SHOTTO;
        } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            shotTo = sourceVec3;
            shotMode = ShotMode.AUTOSHOT;
           
        }
    }


    // void Shot() {
    //     if (shotMode == ShotMode.STOP) {
    //         return;
    //     } else if (shotMode == ShotMode.AUTOSHOT) {
    //         towers.ForEach(t => t.AutoShot(bp));
    //     } else {
    //         towers.ForEach(t => t.ShotTo(bp, shotTo));
    //     }
        
    // }


    public void StartGame() {
        Log.I("Start Game");
        ejectors.ForEach(e => e.Reset());
        gameEnd = false;
        score = 0;
        gameover.SetActive(false);
        gamestart.SetActive(false);
    }

    void InitGame() {
        // bp
    }
}