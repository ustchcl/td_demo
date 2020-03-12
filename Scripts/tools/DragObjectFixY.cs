using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.EventSystems;

/**
 * 如果直线和平面不平行
 * 已知直线L过点m
 * 且其方向向量为vl
 * 平面P过点n, 且法线方向为vp
 * 则交点o = m + vl * t
 * t = (n-m)·vp / vp·vl
 */
public class DragObjectFixY : MonoBehaviour
{
    private Vector3 offset;

    private Vector3 m; // camera position
    private Vector3 vl;
    private Vector3 n;  // aspect position
    private Vector3 vp;

    private bool draging = false;
    public bool blockByUI = true;


    void Start()
    {
        vp = new Vector3(0f, 1f, 0f);
        m = Camera.main.transform.position;
        n = new Vector3(0f, 0f, 0f);
        vl = Vector3.zero;
    }

    private void OnMouseDown()
    {
        if (blockByUI && IsPointerOverUIObject(Input.mousePosition))
        {
            return;
        }
        draging = true;
        // Camera.main.WorldToScreenPoint
        n.y = gameObject.transform.position.y;
        vl = (GetMouseWorldPos() - m).normalized;
        var t = Vector3.Dot((n - m), vp) / Vector3.Dot(vp, vl);
        var startPos = m + Vector3.Scale(vl, new Vector3(t, t, t));
        offset = gameObject.transform.position - startPos;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = gameObject.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag()
    {
        if (!draging)
        {
            return;
        }
        vl = (GetMouseWorldPos() - m).normalized;
        var t = Vector3.Dot((n - m), vp) / Vector3.Dot(vp, vl);
        transform.position = m + Vector3.Scale(vl, new Vector3(t, t, t)) + offset;
    }

    private void OnMouseExit()
    {
        draging = false;
    }

    private void OnMouseUp()
    {
        draging = false;
    }

    public bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
