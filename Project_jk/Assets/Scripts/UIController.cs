using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mPlayerBattleInfo;

    private Text[] mInfoTxt;
    private List<Text[]> mInfoTxts;

    private void Awake()
    {
        mInfoTxt = new Text[4];
        mInfoTxts = new List<Text[]>();
    }

    private void Start()
    {
        for(int i = 0; i < mPlayerBattleInfo.Length; i++)
        {
            mPlayerBattleInfo[i].GetComponentsInChildren<Text>();
        }
    }
}
