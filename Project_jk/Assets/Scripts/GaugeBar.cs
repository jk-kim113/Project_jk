using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    private Image mGaugeBar;
    [SerializeField]
    private Text mGaugeText;

    public void ShowHPGauge(float progress, string gaugetext)
    {
        mGaugeBar.fillAmount = progress;
        mGaugeText.text = gaugetext;
    }
}
