﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInventory : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private ApplyInvenElement mApplyInven;
    [SerializeField]
    private Transform mApplyInvenPos, mInvenPos;
    [SerializeField]
    private BattleInvenElement mInvenPrefab;
    [SerializeField]
    private Image mEquipPanel;
#pragma warning restore

    private List<Player> mPlayerList;
    private EquipData[] mEquipDataArr;

    private List<BattleInvenElement> mEquiplist;
    private List<ApplyInvenElement> mApplyInvenList;

    private int mSelectedEquipID;

    public void ShowItemInven(List<Player> playerlist, EquipData[] equipdata)
    {
        mPlayerList = new List<Player>();
        mEquiplist = new List<BattleInvenElement>();
        mApplyInvenList = new List<ApplyInvenElement>();

        mEquipDataArr = equipdata;
        mPlayerList = playerlist;

        for (int i = 0; i < SaveLoadData.Instance.SaveData.EquipNum.Length; i++)
        {
            if (SaveLoadData.Instance.SaveData.EquipNum[i] > 0)
            {
                BattleInvenElement inven = Instantiate(mInvenPrefab, mInvenPos);
                inven.Init(
                    null,
                    mEquipDataArr[i].ID,
                    mEquipDataArr[i].Name,
                    SaveLoadData.Instance.SaveData.EquipNum[i],
                    mEquipDataArr[i].Info,
                    mEquipDataArr[i].Value,
                    ArrangePlayer);

                mEquiplist.Add(inven);
            }
        }

        for (int i = 0; i < mPlayerList.Count; i++)
        {
            ApplyInvenElement apply = Instantiate(mApplyInven, mApplyInvenPos);
            apply.Init(
                mPlayerList[i].ID,
                mPlayerList[i].Name,
                mPlayerList[i].HPcurrent,
                mPlayerList[i].HPmax,
                mPlayerList[i].ATK,
                mPlayerList[i].DEF,
                mPlayerList[i].HEAL,
                ApplyItem);

            mApplyInvenList.Add(apply);
        }
    }

    public void OpenPanel()
    {
        mEquipPanel.gameObject.SetActive(true);
    }

    private void ApplyItem(int id)
    {
        InventoryController.Instance.ApplyEquipItem(id, mEquipDataArr[mSelectedEquipID - 21].EquipType, mSelectedEquipID - 21);

        SaveLoadData.Instance.SaveData.EquipNum[mSelectedEquipID - 21]--;

        mApplyInvenList[id].Renew(mPlayerList[id].HPcurrent, mPlayerList[id].HPmax);

        mEquiplist[mSelectedEquipID - 21].Renew(SaveLoadData.Instance.SaveData.EquipNum[mSelectedEquipID - 21]);
    }

    private void ArrangePlayer(int id)
    {
        mSelectedEquipID = id;
    }

    public void ExitButton()
    {
        for(int i = 0; i < mApplyInvenList.Count; i++)
        {
            mApplyInvenList[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
