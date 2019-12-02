using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private Image[] mPlayerInfo;

    private Text[] mPlayerInfoText;
    private List<Text[]> mPlayerInfoTexts;

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

        mPlayerInfoTexts = new List<Text[]>();

        for (int i = 0; i < mPlayerInfo.Length; i++)
        {
            mPlayerInfoText = mPlayerInfo[i].GetComponentsInChildren<Text>();
            mPlayerInfoTexts.Add(mPlayerInfoText); 
        }
    }

    public void ShowPlayerStat(int id, string state, string atk, string def, string heal)
    {
        mPlayerInfoTexts[id][0].text = state;
        mPlayerInfoTexts[id][1].text = "ATK : " + atk;
        mPlayerInfoTexts[id][2].text = "DEF : " + def;
        mPlayerInfoTexts[id][3].text = "HEAL : " + heal;
    }
}
