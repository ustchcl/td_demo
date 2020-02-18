using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Utils {
    private static Utils _instance = null;

    private Utils() {
        Random.InitState(9527);
    }

    
    public static Utils GetInstance() {
        if (_instance == null) {
            _instance = new Utils();
        }
        return _instance;
    }
    
    public float Rand() {
        return Random.Range(0, 1f);
    }


    public string MouseX { get { return "Mouse X"; } }
    public  string MouseY  {
        get { return "Mouse Y"; }
    }

    public string Vertical { get { return "Vertical"; } }

    public string Horizontal { get { return "Horizontal"; }}

    public List<int> Range(int from, int to) {
        List<int> result = new List<int>();
        for (var i = from; i < to; ++i) {
            result.Add(i);
        }
        return result;
    }
}

