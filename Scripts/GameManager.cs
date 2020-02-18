using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using UniRx;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Bullet bulletModel;
    public List<Tower> towers;
    private enum ShotMode {AUTOSHOT = 0, STOP = 1, SHOTTO = 2};
    private ShotMode shotMode = ShotMode.STOP;
    private Utils utils;

    private TimeItem m_RepeatTimeItem;    
    private TimeItem enemy_TimeItem;

    private Vector3 shotTo = new Vector3(0, 0, 10); 
    private Vector3 sourceVec3 = new Vector3(0, 0, 10);

    private double cos15 = Math.Cos(15.0 / 180.0);


    private SimpleObjectPool<Bullet> bp;
    private SimpleObjectPool<Enemy> ep;


    float spd = 1f;

    public static bool gameEnd = true;
    public int _score = 0;

    public GameObject gameover ;
    public GameObject gamestart;
    public Text scoreText;

    public  int score {
        get { return _score; } set { _score = value; scoreText.text = String.Format("Score: {0}", _score); }
    }



    void Awake() {
        QFramework.QEventSystem.Instance.Register(1, OnEvent);
        QFramework.QEventSystem.Instance.Register(2, OnScoreChange);
    }

    void OnDistroy() {
        QEventSystem.Instance.UnRegister(1, OnEvent);
        QFramework.QEventSystem.Instance.UnRegister(2, OnScoreChange); 
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
        // 子弹速度
        m_RepeatTimeItem = Timer.Instance.Post2Really(OnTimeTick, 0.5f, -1);
        // 怪物速度
        enemy_TimeItem = Timer.Instance.Post2Really(GenEnemy, 0.8f, -1);
        shotMode = ShotMode.AUTOSHOT;
        shotTo = sourceVec3;
        bp = new SimpleObjectPool<Bullet>(() => {
            var bullet = Instantiate(bulletModel);
            bullet.gameObject.SetActive(false);
            return bullet;
        }, (Bullet bullet) => {
            bullet.gameObject.SetActive(false);
            var tr = bullet.gameObject.GetComponent<TrailRenderer>() as TrailRenderer;
            tr.Clear();
            bullet.rb.Sleep();
        }, 50);
    }

    private void OnTimeTick(int tick) {
        if (gameEnd) {
            return;
        }
        Shot();
    }

    private void GenEnemy(int tick) {
        if (gameEnd) {
            return;
        }
        Utils.GetInstance().Range(0, 5).ForEach(i => {
            var enemy = EnemyPool.Instance.ep.Allocate();
            enemy.gameObject.SetActive(true);
            enemy.transform.position = new Vector3(4f - i * 2f, 0.3f, 14);
        });
        
    }

    void Update() {
        if (gameEnd) { return ; }
        if (Input.GetKey(KeyCode.Space)) {
            shotTo = Input.mousePosition;
            shotMode = ShotMode.SHOTTO;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            shotTo = sourceVec3;
            shotMode = ShotMode.AUTOSHOT;
        }
    }

    void Shot() {
        if (shotMode == ShotMode.STOP) {
            return;
        } else if (shotMode == ShotMode.AUTOSHOT) {
            towers.ForEach(t => t.AutoShot(bp));
        } else {
            towers.ForEach(t => t.ShotTo(bp, shotTo));
        }
        
    }


    public void StartGame() {
        Log.I("Start Game");
        gameEnd = false;
        score = 0;
        gameover.SetActive(false);
        gamestart.SetActive(false);
    }

    void InitGame() {
        // bp.
    }
}