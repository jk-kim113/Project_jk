using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mGaugeBar;
    [SerializeField]
    private Text mGaugeText;
#pragma warning restore

    public void ShowHPGauge(float progress, string gaugetext)
    {
        mGaugeBar.fillAmount = progress;
        mGaugeText.text = gaugetext;
    }
}
