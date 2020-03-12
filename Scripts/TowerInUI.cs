using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInUI : MonoBehaviour
{
    private Vector2[] posArray;
    private GameObject ground;

    public Text lvText;
    private int _level;
    public int selfIndex;
    public int level {
        get { return _level; } set { _level = value; lvText.text = string.Format("Lv.{0}", value); }
    }
    
    private void Awake() {
        ground = Ground.Instance.gameObject;
        posArray = new Vector2[11];
        for (int i = 0; i < 5; ++i) {
            posArray[i * 2] = vec2(-200 + i*100, -310);
            posArray[i * 2 + 1] = vec2(-200 + i*100, -415);
        }
        posArray[10] = vec2(0, -14);
    }

    // Start is called before the first frame updateoi
    void Start()
    {
        InitDrag();        
    }

    // Update is called once per frame

    private void OnMouseUp() {
        Vector2 groundPos = ground.transform.position;
        int index = GetIndex();
        transform.position = new Vector3(posArray[index].x + groundPos.x, posArray[index].y + groundPos.y, transform.position.z + 10);

        if (index == selfIndex) {
            return;
        }        

        QFramework.QEventSystem.Instance.Send(Constants.Instance.TOWER_MOVE_TO, selfIndex, index);
    }

    int GetIndex() {
        Vector2 pos = transform.position;
        int index = 0;
        float min = 9999f;
        Vector2 groundPos = ground.transform.position;
        for (int i = 0; i < posArray.Length; ++i) {
            float distance = (posArray[i] + groundPos - pos).magnitude;
            if (min > distance) {
                min = distance;
                index = i;
            }
        }
        return index;
    }

    public void LevelUp() {
        level += 1;
    }

    public void MoveToIndex(int index) {
        if (ToolBox.Instance.InRange(index, 0, 10)) {        
            Vector2 groundPos = ground.transform.position;
            transform.position = new Vector3(posArray[index].x + groundPos.x, posArray[index].y + groundPos.y, transform.position.z);
        }
    }

    public void InitIndex(int index) {
        selfIndex = index;
        MoveToIndex(index);
    }


    #region  Drag
    private Vector3 offset;
    private Vector3 m; // camera position
    private Vector3 vl;
    private Vector3 n;  // aspect position
    private Vector3 vp;

    void InitDrag () {
        vp = new Vector3(0f, 0f, 1f);
        m = Camera.main.transform.position;
        n = new Vector3(0f, 0f, 0f);
        vl = Vector3.zero;
    }    

    private void OnMouseDown() {
        gameObject.transform.Translate(Vector3.forward * -10);
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
        var index = GetIndex();
        QFramework.QEventSystem.Instance.Send(Constants.Instance.HIGH_LIGHT, index);
    }
    #endregion
    
    Vector2 vec2(float x, float y) {
        return new Vector2(x, y);
    } 
}
