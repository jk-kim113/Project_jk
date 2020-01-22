using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPassing : MonoBehaviour
{
    public static DataPassing Instance;

    private List<CardData> mCardDataPassingList;
    public List<CardData> CardDataPassingList { get { return mCardDataPassingList; } }

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

        mCardDataPassingList = new List<CardData>();
    }

    public void CardDataPass(CardData cardData)
    {
        mCardDataPassingList.Add(cardData);
    }

    public List<CardData> CardDeckSpawn()
    {
        return mCardDataPassingList;
    }
}
