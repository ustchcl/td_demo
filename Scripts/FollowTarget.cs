using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Vector3 offset;
    public GameObject target;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position =  target.transform.position + offset;       
    }
}
