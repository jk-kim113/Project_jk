using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPassing : MonoBehaviour
{
    public static DataPassing Instance;

    private List<CardData> mCardDataPassingList;
    public List<CardData> CardDataPassingList { get { return mCardDataPassingList; } }

    private int[] mSavedCardID;

    private CardData[] mCardData;
    public CardData[] CardData { get { return mCardData; } }

    private int mCardSaveCount;

    public StaticValue.VoidCallBack mGoldConsumeCallBack;

    public double Gold
    {
        get { return SaveLoadData.Instance.SaveData.Gold; }
        set
        {
            if (value >= 0)
            {
                if (SaveLoadData.Instance.SaveData.Gold > value)
                {
                    mGoldConsumeCallBack?.Invoke();
                    mGoldConsumeCallBack = null;
                }

                SaveLoadData.Instance.SaveData.Gold = value;
            }
            else
            {
                Debug.Log("Not enough Money");
            }
        }
    }

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

        DontDestroyOnLoad(this);

        mCardSaveCount = 0;
        mCardDataPassingList = new List<CardData>();

        mCardData = LoadOriginFiles.Instance.GetDataToCardController();
        mSavedCardID = SaveLoadData.Instance.SaveData.CardID;

        if (mSavedCardID[0] >= 0)
        {
            for (int i = 0; i < mSavedCardID.Length; i++)
            {
                if(mSavedCardID[i] >= 0)
                {
                    mCardDataPassingList.Add(mCardData[mSavedCardID[i]]);
                }
            }
        }
    }

    public void Init()
    {
        mCardDataPassingList.Clear();
        mCardSaveCount = 0;
        for(int i = 0; i < mSavedCardID.Length; i++)
        {
            mSavedCardID[i] = -1;
        }
    }

    public void CardDataPass(CardData cardData)
    {
        mCardDataPassingList.Add(cardData);

        mSavedCardID[mCardSaveCount] = cardData.ID;
        mCardSaveCount++;
    }

    public List<CardData> CardDeckSpawn()
    {
        return mCardDataPassingList;
    }
}
