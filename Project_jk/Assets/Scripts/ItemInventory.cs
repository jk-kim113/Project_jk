using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private ApplyInvenElement mApplyInven;
    [SerializeField]
    private Transform mApplyInvenPos;
    [SerializeField]
    private BattleInvenElePool mItemElePool;
#pragma warning restore

    private List<Player> mPlayerList;

    private int mSelectedItemID;

    private List<BattleInvenElement> mBattleInvenList;

    private List<ApplyInvenElement> mApplyInvenList;

    private ItemData[] mItemData;

    public void ShowItemInven(List<Player> playerlist, ItemData[] itemdata)
    {
        mPlayerList = new List<Player>();
        mApplyInvenList = new List<ApplyInvenElement>();
        mBattleInvenList = new List<BattleInvenElement>();

        mItemData = itemdata;
        mPlayerList = playerlist;

        for (int i = 0; i < SaveLoadData.Instance.SaveData.ItemNum.Length; i++)
        {
            if (SaveLoadData.Instance.SaveData.ItemNum[i] > 0)
            {
                BattleInvenElement inven = mItemElePool.GetFromPool(0);
                inven.Init(
                    null,
                    mItemData[i].ID,
                    mItemData[i].Name,
                    SaveLoadData.Instance.SaveData.ItemNum[i],
                    mItemData[i].Info,
                    mItemData[i].Value,
                    ArrangePlayer);

                mBattleInvenList.Add(inven);
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

            mApplyInvenList[i].gameObject.SetActive(false);
        }
    }

    private void ApplyItem(int id)
    {
        InventoryController.Instance.ApplyConsumeItem(id, mSelectedItemID - 11);
        SaveLoadData.Instance.SaveData.ItemNum[mSelectedItemID - 11]--;

        mApplyInvenList[id].Renew(mPlayerList[id].HPcurrent, mPlayerList[id].HPmax);

        mBattleInvenList[mSelectedItemID - 11].Renew(SaveLoadData.Instance.SaveData.ItemNum[mSelectedItemID - 11]);
    }

    private void ArrangePlayer(int id)
    {
        mSelectedItemID = id;

        for(int i = 0; i < mApplyInvenList.Count; i++)
        {
            mApplyInvenList[i].gameObject.SetActive(true);
        }
    }

    public void ExitButton()
    {
        gameObject.SetActive(false);
    }
}
