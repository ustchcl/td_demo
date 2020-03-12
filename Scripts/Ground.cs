using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public static Ground Instance;
    // Start is called before the first frame update
    void Awake()
    {  
        Debug.Log(this);
        Instance = this;
    }

    // Update is called once per frame
}
