using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    public static CardController Instance;

#pragma warning disable 0649
    [SerializeField]
    private CardElement mCardElementPrefab;
    [SerializeField]
    private Transform mScrollPos1, mScrollPos2;
    [SerializeField]
    private CardElement mSelectedCardPrefab;
#pragma warning restore

    private List<CardElement> mCardElementList;

    private Dictionary<int, CardElement> mSelectedCardDic;

    private CardData[] mCardDataArr;

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
    }

    private void Start()
    {
        mCardElementList = new List<CardElement>();
        mCardElementList.Clear();
        mSelectedCardDic = new Dictionary<int, CardElement>();
        mSelectedCardDic.Clear();

        mCardDataArr = DataPassing.Instance.CardData;

        SpawnCard();
    }

    private void SpawnCard()
    {
        for (int i = 0; i < mCardDataArr.Length; i++)
        {
            CardElement cardElement = Instantiate(mCardElementPrefab, mScrollPos1);
            cardElement.Init(mCardDataArr[i].ID, mCardDataArr[i].Contents, AddCard);
            mCardElementList.Add(cardElement);
        }

        for(int i = 0; i < DataPassing.Instance.CardDataPassingList.Count; i++)
        {
            int id = DataPassing.Instance.CardDataPassingList[i].ID;

            AddCard(id);
        }

        DataPassing.Instance.CardDataPassingList.Clear();
    }

    public void AddCard(int id)
    {
        mCardElementList[id].gameObject.SetActive(false);

        CardElement selectedCard = Instantiate(mSelectedCardPrefab, mScrollPos2);
        selectedCard.Init(mCardDataArr[id].ID, mCardDataArr[id].Contents, RemoveCard);
        mSelectedCardDic.Add(mCardDataArr[id].ID, selectedCard);
    }

    public void RemoveCard(int id)
    {
        mSelectedCardDic[id].gameObject.SetActive(false);
        mSelectedCardDic.Remove(id);

        mCardElementList[id].gameObject.SetActive(true);
    }

    public void LobbyButton()
    {
        DataPassing.Instance.Init();

        foreach(int keys in mSelectedCardDic.Keys)
        {
            DataPassing.Instance.CardDataPass(mCardDataArr[keys]);
        }

        SceneManager.LoadScene("Lobby");
    }
}
