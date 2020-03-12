using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour {
    public TextMesh text;
    public Animation ani;

    public void Play() {
        ani.Play();
    }

    public void InitDamage(double damage, int multi = 1) {
        var start = (multi == 1) ? "-" : "暴-";
        text.text = string.Format(start + "<b>{0}</b>", ToolBox.Instance.FormatNum(damage * multi));
    }

}