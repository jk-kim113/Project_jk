using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPassing : MonoBehaviour
{
    public static DataPassing Instance;

    private List<CardData> mCardDataPassing;

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

        mCardDataPassing = new List<CardData>();
    }

    public void CardDataPass(CardData cardData)
    {
        mCardDataPassing.Add(cardData);
    }

    public List<CardData> CardDeckSpawn()
    {
        return mCardDataPassing;
    }
}
