using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

/**
 * 如果直线和平面不平行
 * 已知直线L过点m
 * 且其方向向量为vl
 * 平面P过点n, 且法线方向为vp
 * 则交点o = m + vl * t
 * t = (n-m)·vp / vp·vl
 */
public class DragObject : MonoBehaviour
{
    private Vector3 offset;

    private Vector3 m; // camera position
    private Vector3 vl;
    private Vector3 n;  // aspect position
    private Vector3 vp;


    void Start () {
        vp = new Vector3(0f, 0f, 1f);
        m = Camera.main.transform.position;
        n = new Vector3(0f, 0f, 0f);
        vl = Vector3.zero;
    }    

    private void OnMouseDown() {
        n.z = gameObject.transform.position.z;
        vl = (GetMouseWorldPos() - m).normalized;
        var t = Vector3.Dot((n - m), vp) / Vector3.Dot(vp, vl);
        var startPos = m + Vector3.Scale(vl, new Vector3(t, t, t)); 
        offset = gameObject.transform.position - startPos;
    } 

    private Vector3 GetMouseWorldPos() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = n.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag() {
        vl = (GetMouseWorldPos() - m).normalized;
        var t = Vector3.Dot((n - m), vp) / Vector3.Dot(vp, vl);
        transform.position =  m + Vector3.Scale(vl, new Vector3(t, t, t)) + offset;
    }


}
