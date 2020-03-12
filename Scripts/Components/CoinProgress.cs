using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CoinProgress : MonoBehaviour
{
    [Range(0, 100)]
    public int value = 0;

    [SerializeField] public Image barImg;
    public Image completeBg;
    public Image normalBg;

    public Text coinText;

    private float _progress = 0;
    public float progress
    {
        get
        {
            return _progress;
        }
        set
        {
            _progress = ToolBox.Instance.Clamp(0f, 1f, value);
            var notComplete = _progress < 1f;
            completeBg.gameObject.SetActive(!notComplete);
            normalBg.gameObject.SetActive(notComplete);
            barImg.gameObject.SetActive(notComplete);
            barImg.fillAmount = _progress;
        }
    }

    void Start()
    {
        progress = value / 100f;
    }
}
