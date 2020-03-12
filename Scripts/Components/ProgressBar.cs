using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Range(0, 100)]
    public int value = 0;

    [SerializeField] public Image bar;
    
    private float _progress = 0;
    public float progress {
        get {
            return _progress;
        }
        set {
            _progress = Mathf.Min(1, Mathf.Max(0, value));
            bar.fillAmount = _progress;
        }
    }

    private void Awake() {
        progress = value / 100f;
    }
}
