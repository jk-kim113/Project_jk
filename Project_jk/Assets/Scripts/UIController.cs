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
    }

    public void ShowPlayerInfo(eBattleType state, float atk, float def, float heal)
    {
        mPlayerInfoObj.SetActive(true);

        mPlayerInfoText[0].text = state.ToString();
        mPlayerInfoText[1].text = "ATK ; " + atk.ToString();
        mPlayerInfoText[2].text = "DEF : " + def.ToString();
        mPlayerInfoText[3].text = "HEAL : " + heal.ToString();
    }

    public void OffPlayerInfo()
    {
        mPlayerInfoObj.SetActive(false);
    }

    public void ShowTotalStatus(float atk, float def, float heal)
    {
        mTotalStatusText[0].text = "Total ATK : " + atk.ToString();
        mTotalStatusText[1].text = "Total DEF : " + def.ToString();
        mTotalStatusText[2].text = "Total HEAL : " + heal.ToString();
    }
}
