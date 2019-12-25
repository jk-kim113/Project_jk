using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private GameObject mPlayerInfoObj;
    private Text[] mPlayerInfoText;

    [SerializeField]
    private GameObject mTotalStatusObj;
    private Text[] mTotalStatusText;

    [SerializeField]
    private GaugeBar mMonsterGaugeBar;
    [SerializeField]
    private GaugeBar mPlayerGaugeBar;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mPlayerInfoText = mPlayerInfoObj.GetComponentsInChildren<Text>();
        mTotalStatusText = mTotalStatusObj.GetComponentsInChildren<Text>();

        ShowTotalStatus(0, 0, 0);
    }

    public void ShowPlayerInfo(eBattleType state, double atk, double def, double heal)
    {
        mPlayerInfoObj.SetActive(true);

        mPlayerInfoText[0].text = state.ToString();
        mPlayerInfoText[1].text = "ATK : " + UnitBuilder.GetUnitStr(atk);
        mPlayerInfoText[2].text = "DEF : " + UnitBuilder.GetUnitStr(def);
        mPlayerInfoText[3].text = "HEAL : " + UnitBuilder.GetUnitStr(heal);
    }

    public void OffPlayerInfo()
    {
        mPlayerInfoObj.SetActive(false);
    }

    public void ShowTotalStatus(double atk, double def, double heal)
    {
        mTotalStatusText[0].text = "Total ATK : " + UnitBuilder.GetUnitStr(atk);
        mTotalStatusText[1].text = "Total DEF : " + UnitBuilder.GetUnitStr(def);
        mTotalStatusText[2].text = "Total HEAL : " + UnitBuilder.GetUnitStr(heal);
    }

    public void ShowMonsterGaugeBar(double current, double max)
    {
        float progress = (float)(current / max);
        string gaugetext = string.Format("{0} / {1}", UnitBuilder.GetUnitStr(current), UnitBuilder.GetUnitStr(max));
        mMonsterGaugeBar.ShowHPGauge(progress, gaugetext);
    }

    public void ShowPlayerGaugeBar(double current, double max)
    {
        float progress = (float)(current / max);
        string gaugetext = string.Format("{0} / {1}", UnitBuilder.GetUnitStr(current), UnitBuilder.GetUnitStr(max));
        mPlayerGaugeBar.ShowHPGauge(progress, gaugetext);
    }
}
