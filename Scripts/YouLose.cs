using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToGame() {
        SceneManager.LoadScene("Battle");
        QFramework.QEventSystem.Instance.Send(1, false, "42");
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
