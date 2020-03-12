using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class Drag2D : MonoBehaviour
{

    Vector3 offset = Vector2.zero;

    public void StartDrag()
    {
        offset = gameObject.transform.position - Input.mousePosition;
    }

    public void Draging()
    {
        gameObject.transform.position = offset + Input.mousePosition;
    }

    public void EndDarg()
    {
        gameObject.SetActive(false);
    }
}
