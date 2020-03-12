using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using QFramework;

public class TowerComposition : MonoBehaviour
{
    private Vector3[] posArray;

    public Text levelText;

    public GameObject ground;

    public int selfIndex;

    private int _level;

    public int level
    {
        get { return _level; }
        set
        {
            _level = value;
            levelText.text = value.ToString();
        }
    }

    private GameObject model;
    public Attach2DTo3D attach;

    private void Awake() {
        posArray = new Vector3[10];
        for (var i = 0; i < 5; ++i) {
            posArray[i] = new Vector3(-4f + i * 2f, 0.6f, -4.8f);
        }
        for (var i = 5; i < 10; ++i) {
            posArray[i] = new Vector3(-4f + (i - 5) * 2f, 0.6f, -6.9f);
        }
    }

    private void Start() {
        InitDrag();
        attach.gameObject.SetActive(false);
    }


    public void LevelUp() {
        level += 1;
        Destroy(model);
        InitModel(); 
    }

    void InitModel() {
        var path = string.Format("tower_model/tower_{0}", level);
        var prefab = Resources.Load(path, typeof(GameObject)) as GameObject;
        model = Instantiate(prefab, transform);
    }

    public void Destroy() {
        Destroy(model);
        model = null;
        attach.gameObject.SetActive(false);
        Log.I("DESOTROY: {0}", selfIndex);
        IsEmpty = true;
    }

    public void Reborn() {
        level = 1;
        InitModel();
        attach.gameObject.SetActive(true);
        Log.I("ISEMPTY: {0}", selfIndex);
        IsEmpty = false;
    }
    
    public void MoveToIndex(int index) {
        if (ToolBox.Instance.InRange(index, 0, 10)) {        
            transform.position = posArray[index] + ground.transform.position;
            attach.UpdatePos();
            selfIndex = index;
        }
    }
    private void OnMouseUp() {
        draging = false;

        int index = GetIndex();
        transform.position = posArray[index] + ground.transform.position;

        if (index == selfIndex) {
            return;
        }
        QFramework.QEventSystem.Instance.Send(Constants.Instance.TOWER_MOVE_TO, selfIndex, index);
    }

    int GetIndex() {
        Vector3 pos = transform.position;
        int index = 0;
        float min = 9999f;
        Vector3 groundPos = ground.transform.position;
        for (int i = 0; i < posArray.Length; ++i) {
            float distance = (posArray[i] + groundPos - pos).magnitude;
            if (min > distance) {
                min = distance;
                index = i;
            }
        }
        return index;
    }

    #region  drag
    
    private Vector3 offset;
    private Vector3 m; // camera position
    private Vector3 vl;
    private Vector3 n;  // aspect position
    private Vector3 vp;
    private bool draging = false;

    void InitDrag()
    {
        vp = new Vector3(0f, 1f, 0f);
        m = Camera.main.transform.position;
        n = new Vector3(0f, 0f, 0f);
        vl = Vector3.zero;
    }

    private void OnMouseDown()
    {
        if (IsEmpty) {
            Log.I("Empty: {0}", selfIndex);
            return;
        }
        if (IsPointerOverUIObject(Input.mousePosition))
        {   
            Log.I("UIBlock: {0}", selfIndex);
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
        attach.UpdatePos();
    }


    public bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    #endregion

    public bool IsEmpty = true;
}
