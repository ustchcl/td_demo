using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] public Text version;
    // Start is called before the first frame update
    void Start()
    {
        version.text = "TD Demo v0.1"; 
    }

    public void StartGame() {
        SceneManager.LoadScene("Battle");
    }
}
