using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInvenDragVer : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private EquipSlot mEquipSlot;
    [SerializeField]
    private Transform mEquipSlotPos;

    [SerializeField]
    private Image mIcon;
#pragma warning restore

    private const int mSlotNum = 50;

    private Sprite[] mIconsArr;

    private List<EquipSlot> mSlotList;

    private void Awake()
    {
        mIconsArr = Resources.LoadAll<Sprite>("ItemSprites");

        mSlotList = new List<EquipSlot>();
    }

    private void Start()
    {
        for(int i = 0; i < mSlotNum; i++)
        {
            EquipSlot slot = Instantiate(mEquipSlot, mEquipSlotPos);
            slot.Init(mIconsArr[i]);
            mSlotList.Add(slot);
        }
    }
}
