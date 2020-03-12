using UnityEngine;
using UniRx;
using QFramework;

/**
 * 将canvas上的UI， 附到3D物体上
 */
public class Attach2DTo3D : MonoBehaviour
{
    public GameObject taregeObject3D;
    public GameObject ground;
    public Vector3 offset;

    public RectTransform self;
    private readonly float minDistance = 300f;
    private readonly float maxDistance = 900f;
    private readonly float scaleByDistance = 0.5f;

    private void Start()
    {
        ground.transform.ObserveEveryValueChanged(x => x.position).Subscribe(_ => UpdatePos());
    }

    public void UpdatePos()
    {
        var pos = taregeObject3D.transform.position;
        var pos2 = Camera.main.WorldToScreenPoint(pos);
        var distancePos = new Vector3(pos.x, pos2.y, pos2.z);
        var distance = (pos - distancePos).magnitude;
        var scale = (distance - minDistance) * scaleByDistance / (maxDistance - minDistance);
        self.localScale = new Vector2(1 - scale, 1 - scale);
        self.Position(pos2 + offset);
    }
}
