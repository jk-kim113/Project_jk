using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectController : MonoBehaviour
{
    public static CardEffectController Instance;

    private List<CardData> mCardPassDataList;

    private int mCardID;

    private int mCardOrder;

    private bool bID1Card;
    public bool ID1Card { get { return bID1Card; } }
    private int mID1CardCount;

    private bool bID2Card;
    private int mID2CardCount;

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

        mCardPassDataList = new List<CardData>();
        
        mCardID = 0;
        mCardOrder = 0;

        bID1Card = false;
        mID1CardCount = 0;

        bID2Card = false;
        mID2CardCount = 0;
    }

    public void SpawnCardEffect()
    {
        mCardPassDataList = DataPassing.Instance.CardDeckSpawn();

        if (mCardPassDataList.Count == 0)
        {
            UIController.Instance.ShowCardData("카드 없음");
        }
        else
        {
            UIController.Instance.ShowCardData(mCardPassDataList[mCardOrder].Contents);
            mCardID = mCardPassDataList[mCardOrder].ID;

            ApplyCardEffect();
        }

        
    }

    public void NextCardEffect()
    {
        if(mCardOrder >= mCardPassDataList.Count)
        {
            mCardOrder = -1;
        }

        if(mCardID == 0)
        {
            StopCoroutine(ID0CardEffect());
        }
        else if(mCardID == 4)
        {
            MonsterController.Instance.EffectATKbyCard(1);
            PlayerController.Instance.EffectHPmaxByCard(1);
        }

        if(!bID1Card || !bID2Card)
        {
            mCardOrder++;
            mCardID = mCardPassDataList[mCardOrder].ID;
        }
        
        UIController.Instance.ShowCardData(mCardPassDataList[mCardOrder].Contents);
        ApplyCardEffect();
    }

    private void ApplyCardEffect()
    {
        switch(mCardID)
        {
            case 0:

                StartCoroutine(ID0CardEffect());

                break;
            case 1:

                ID1CardEffect();

                break;
            case 2:

                ID2CardEffect();

                break;
            case 3:

                ID3CardEffect();

                break;
            case 4:

                ID4CardEffect();

                break;
            case 5:

                ID5CardEffect();

                break;
            default:

                Debug.LogError("Wrong CardID : " + mCardID);

                break;
        }
    }

    private IEnumerator ID0CardEffect()
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

        bool IsOnEffect = false;

        while(true)
        {
            if(IsOnEffect == false)
            {
                if (PlayerController.Instance.CheckAllBattleState())
                {   
                    IsOnEffect = true;
                    PlayerController.Instance.EffectATKbyCard(PlayerController.Instance.PlayerAtk * 1.05);
                }
            }
            else
            {
                if (!PlayerController.Instance.CheckAllBattleState())
                {
                    IsOnEffect = false;
                    PlayerController.Instance.EffectATKbyCard(PlayerController.Instance.PlayerAtk);
                }
                else
                {
                    PlayerController.Instance.EffectATKbyCard(PlayerController.Instance.PlayerAtk * 1.05);
                }
            }

            yield return fixedUpdate;
        }
    }

    private void ID1CardEffect()
    {
        if(mID1CardCount > 3)
        {
            bID1Card = false;
            NextCardEffect();
            return;
        }

        bID1Card = true;

        mID1CardCount++;
    }

    private void ID2CardEffect()
    {
        bID2Card = true;
        if(mID2CardCount < 3)
        {
            MonsterController.Instance.EffectDEFbyCard(1.05);
        }
        else if(mID2CardCount < 8)
        {
            MonsterController.Instance.EffectDEFbyCard(0.95);
        }
        else
        {
            bID2Card = false;
            MonsterController.Instance.EffectDEFbyCard(1);
            NextCardEffect();
        }
        

        mID2CardCount++;
    }

    private void ID3CardEffect()
    {
        PlayerController.Instance.EffectDeIcebyCard();
    }

    private void ID4CardEffect()
    {
        MonsterController.Instance.EffectATKbyCard(1.05);
        PlayerController.Instance.EffectHPmaxByCard(1.05);
    }

    private void ID5CardEffect()
    {
        PlayerController.Instance.EffectHealByCard();
    }
}
