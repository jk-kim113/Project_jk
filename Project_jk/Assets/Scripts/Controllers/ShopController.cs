using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

#pragma warning disable 0649
    [SerializeField]
    private ItemElement mItemElementPrefab;
    [SerializeField]
    private InvenElement mInvenElemntPrefab;
    [SerializeField]
    private Transform mScrollPos, mInvenScrollPos;
    [SerializeField]
    private Text mGoldText;
#pragma warning restore

    private ItemData[] mItemDataArr;

    private EquipData[] mEquipDataArr;

    private List<InvenElement> mBoughtItemIDList;

    private List<InvenElement> mBoughtEquipIDList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mBoughtItemIDList = new List<InvenElement>();
        mBoughtEquipIDList = new List<InvenElement>();
    }

    private void Start()
    {
        mItemDataArr = LoadOriginFiles.Instance.LoadItemDataArr();
        mEquipDataArr = LoadOriginFiles.Instance.LoadEquipDataArr();

        for (int i = 0; i < mItemDataArr.Length; i++)
        {
            ItemElement item = Instantiate(mItemElementPrefab, mScrollPos);
            item.Init(
                null,
                mItemDataArr[i].ID,
                mItemDataArr[i].Name,
                mItemDataArr[i].Cost,
                mItemDataArr[i].Info,
                mItemDataArr[i].Value,
                BuyItemOrEquip);
        }

        for (int i = 0; i < mEquipDataArr.Length; i++)
        {
            ItemElement equip = Instantiate(mItemElementPrefab, mScrollPos);
            equip.Init(
                null,
                mEquipDataArr[i].ID,
                mEquipDataArr[i].Name,
                mEquipDataArr[i].Cost,
                mEquipDataArr[i].Info,
                mEquipDataArr[i].Value,
                BuyItemOrEquip);
        }

        for (int i = 0; i < SaveLoadData.Instance.SaveData.ItemNum.Length; i++)
        {
            if(SaveLoadData.Instance.SaveData.ItemNum[i] > 0)
            {
                InvenElement inven = Instantiate(mInvenElemntPrefab, mInvenScrollPos);

                for (int j = 0; j < SaveLoadData.Instance.SaveData.ItemNum[i]; j++)
                {
                    inven.Init(
                        null,
                        mItemDataArr[i].ID,
                        mItemDataArr[i].Name);
                }

                mBoughtItemIDList.Add(inven);
            }
            else
            {
                InvenElement inven = Instantiate(mInvenElemntPrefab, mInvenScrollPos);
                inven.gameObject.SetActive(false);
                mBoughtItemIDList.Add(inven);
            }
        }

        for (int i = 0; i < SaveLoadData.Instance.SaveData.EquipNum.Length; i++)
        {
            if (SaveLoadData.Instance.SaveData.EquipNum[i] > 0)
            {
                InvenElement inven = Instantiate(mInvenElemntPrefab, mInvenScrollPos);

                for (int j = 0; j < SaveLoadData.Instance.SaveData.EquipNum[i]; j++)
                {
                    inven.Init(
                        null,
                        mEquipDataArr[i].ID,
                        mEquipDataArr[i].Name);
                }

                mBoughtEquipIDList.Add(inven);
            }
            else
            {
                InvenElement inven = Instantiate(mInvenElemntPrefab, mInvenScrollPos);
                inven.gameObject.SetActive(false);
                mBoughtEquipIDList.Add(inven);
            }
        }

        mGoldText.text = "Gold : " + UnitBuilder.GetUnitStr(DataPassing.Instance.Gold);
    }

    public void BuyItemOrEquip(int id)
    {
        DataPassing.Instance.mGoldConsumeCallBack = () => { AddInventory(id); };

        eShopItemType type = TransformIDtoEnum(id);

        switch(type)
        {
            case eShopItemType.Consume:

                DataPassing.Instance.Gold -= mItemDataArr[id - 11].Cost;

                break;
            case eShopItemType.Equip:

                DataPassing.Instance.Gold -= mEquipDataArr[id - 21].Cost;

                break;
            default:

                Debug.LogError("Wrong Item or Equip type : " + type);

                break;
        }

        mGoldText.text = "Gold : " + UnitBuilder.GetUnitStr(DataPassing.Instance.Gold);
    }

    private void AddInventory(int id)
    {
        eShopItemType type = TransformIDtoEnum(id);

        switch (type)
        {
            case eShopItemType.Consume:

                for (int i = 0; i < mBoughtItemIDList.Count; i++)
                {
                    if (mBoughtItemIDList[i].ID == mItemDataArr[id - 11].ID && mBoughtItemIDList[i].gameObject.activeInHierarchy)
                    {
                        mBoughtItemIDList[i].AddNumber();

                        SaveLoadData.Instance.SaveData.ItemNum[i] = mBoughtItemIDList[i].ItemNumber;
                        return;
                    }
                }

                mBoughtItemIDList[id - 11].gameObject.SetActive(true);

                mBoughtItemIDList[id - 11].Init(
                    null,
                    mItemDataArr[id - 11].ID,
                    mItemDataArr[id - 11].Name);

                SaveLoadData.Instance.SaveData.ItemNum[id - 11] = mBoughtItemIDList[id - 11].ItemNumber;

                break;
            case eShopItemType.Equip:

                for (int i = 0; i < mBoughtEquipIDList.Count; i++)
                {
                    if (mBoughtEquipIDList[i].ID == mEquipDataArr[id - 21].ID && mBoughtEquipIDList[i].gameObject.activeInHierarchy)
                    {
                        mBoughtEquipIDList[i].AddNumber();

                        SaveLoadData.Instance.SaveData.EquipNum[i] = mBoughtEquipIDList[i].ItemNumber;
                        return;
                    }
                }

                mBoughtEquipIDList[id - 21].gameObject.SetActive(true);

                mBoughtEquipIDList[id - 21].Init(
                    null,
                    mEquipDataArr[id - 21].ID,
                    mEquipDataArr[id - 21].Name);

                SaveLoadData.Instance.SaveData.EquipNum[id - 21] = mBoughtEquipIDList[id - 21].ItemNumber;

                break;
            default:
                Debug.LogError("Wrong Item or Equip type : " + type);
                break;
        }
    }

    private eShopItemType TransformIDtoEnum(int id)
    {
        if(id > 0 && id < 20)
        {
            return eShopItemType.Consume;
        }

        if(id < 30)
        {
            return eShopItemType.Equip;
        }

        else
        {
            Debug.LogError("Wrong Item or Equip ID : " + id);
            return (eShopItemType)(-1);
        }
        
    }

    public void LobbyButton()
    {
        SaveLoadData.Instance.Save();
        SceneManager.LoadScene("Lobby");
    }
}
