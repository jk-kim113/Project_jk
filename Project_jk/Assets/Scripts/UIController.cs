using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

#pragma warning disable 0649
    [SerializeField]
    private GameObject mPlayerInfoObj;
    [SerializeField]
    private GameObject mTotalStatusObj;
    [SerializeField]
    private GameObject mMonsterInfoObj;
    [SerializeField]
    private GaugeBar mMonsterGaugeBar;
    [SerializeField]
    private GaugeBar mPlayerGaugeBar;
    [SerializeField]
    private Text mFieldNameText;
    [SerializeField]
    private Text mFieldInfoText;
    [SerializeField]
    private Text mCardConstentsText;
#pragma warning restore

    private Text[] mPlayerInfoText;
    private Text[] mTotalStatusText;
    private Text[] mMonsterInfoText;

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
        mMonsterInfoText = mMonsterInfoObj.GetComponentsInChildren<Text>();

        ShowTotalStatus(0, 0, 0);
    }

    public void ShowPlayerInfo(eBattleType state, double atk, double def, double heal, double hpCurrent, double hpMax, int conditionCurrent)
    {
        mPlayerInfoObj.SetActive(true);

        mPlayerInfoText[0].text = state.ToString();
        mPlayerInfoText[1].text = "ATK : " + UnitBuilder.GetUnitStr(atk);
        mPlayerInfoText[2].text = "DEF : " + UnitBuilder.GetUnitStr(def);
        mPlayerInfoText[3].text = "HEAL : " + UnitBuilder.GetUnitStr(heal);
        mPlayerInfoText[4].text = string.Format("HP : {0}/{1}", UnitBuilder.GetUnitStr(hpCurrent), UnitBuilder.GetUnitStr(hpMax));
        mPlayerInfoText[5].text = string.Format("Field : {0}", conditionCurrent);
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

    public void ShowFieldStatus(string name, string Info, int cycle, double cycleValue, int condition, double conditionValue)
    {
        mFieldNameText.text = "Name : " + name;
        mFieldInfoText.text = string.Format(Info, cycle, UnitBuilder.GetUnitStr(cycleValue), condition, UnitBuilder.GetUnitStr(conditionValue));
    }

    public void ShowMonsterInfo(string name, double atk, double def)
    {
        mMonsterInfoText[1].text = name;
        mMonsterInfoText[2].text = "ATK : " + UnitBuilder.GetUnitStr(atk);
        mMonsterInfoText[3].text = "DEF : " + UnitBuilder.GetUnitStr(def);
    }

    public void ShowCardData(string cardContents)
    {
        mCardConstentsText.text = cardContents;
    }
}
