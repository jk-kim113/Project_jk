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

        mCardDataArr = new CardData[6];

        mCardDataArr[0] = new CardData();
        mCardDataArr[0].ID = 0;
        mCardDataArr[0].Contents = "배틀 상태에 있는 플레이어 전원이 Attack 상태일 경우 전체 공격력 추가 5%";

        mCardDataArr[1] = new CardData();
        mCardDataArr[1].ID = 1;
        mCardDataArr[1].Contents = "필드의 효과를 3턴 동안 무효화";

        mCardDataArr[2] = new CardData();
        mCardDataArr[2].ID = 2;
        mCardDataArr[2].Contents = "몬스터의 방어력을 3턴 동안 5%증가시키고 그 다음 5턴 동안 방어력 -5%";

        mCardDataArr[3] = new CardData();
        mCardDataArr[3].ID = 3;
        mCardDataArr[3].Contents = "Ice Field에 의해 얼려진 플레이어 효과 해제";

        mCardDataArr[4] = new CardData();
        mCardDataArr[4].ID = 4;
        mCardDataArr[4].Contents = "몬스터의 공격력 5% 증가시키고 전체 체력 5% 증가";

        mCardDataArr[5] = new CardData();
        mCardDataArr[5].ID = 5;
        mCardDataArr[5].Contents = "대기중인 플레이어의 체력이 50%이하이면 최대체력의 30%를 회복";
    }

    private void Start()
    {
        mCardElementList = new List<CardElement>();
        mSelectedCardDic = new Dictionary<int, CardElement>();

        for (int i = 0; i < mCardDataArr.Length; i++)
        {
            CardElement cardElement = Instantiate(mCardElementPrefab, mScrollPos1);
            cardElement.Init(mCardDataArr[i].ID, mCardDataArr[i].Contents, AddCard);
            mCardElementList.Add(cardElement);
        }
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
        foreach(int keys in mSelectedCardDic.Keys)
        {
            DataPassing.Instance.CardDataPass(mCardDataArr[keys]);
        }

        SceneManager.LoadScene("Lobby");
    }
}
