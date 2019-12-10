using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private PlayerController[] mPlayerController;

    private float mTotalAtk;
    private float mTotalDef;
    private float mTotalHeal;

    [SerializeField]
    private GameObject[] mWaitingTable;

    [SerializeField]
    private BattleTable[] mBattleTable;

    private Monster mMonster;

    [SerializeField]
    private Button mTurnExitBtn;

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

        for (int i = 0; i < mPlayerController.Length; i++)
        {
            mPlayerController[i].transform.position = mWaitingTable[i].transform.position + Vector3.up * 0.5f;
        }
    }

    void Start()
    {   
        mMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Monster>();

        if (mTurnExitBtn != null)
            mTurnExitBtn.onClick.AddListener(() => {

                StartCoroutine(TurnExchange());

            });
    }

    public void TotalStatus(eBattleState state, float value)
    {
        switch(state)
        {
            case eBattleState.Attack:
                mTotalAtk += value;
                break;
            case eBattleState.Defend:
                mTotalDef += value;
                break;
            case eBattleState.Heal:
                mTotalHeal += value;
                break;
            default:
                break;
        }

        UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);
    }

    public void SubtractStatus(eBattleState state, float value)
    {
        switch (state)
        {
            case eBattleState.Attack:
                mTotalAtk -= value;
                break;
            case eBattleState.Defend:
                mTotalDef -= value;
                break;
            case eBattleState.Heal:
                mTotalHeal -= value;
                break;
            default:
                break;
        }

        UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);
    }

    private IEnumerator TurnExchange()
    {
        mTurnExitBtn.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        
        yield return new WaitForSeconds(1);

        for (int i = 0; i < mPlayerController.Length; i++)
        {
            //if (mPlayerController[i].GetIsBattlePos())
            //{
            //    mPlayerController[i].NextState();
            //}
        }

        mTurnExitBtn.gameObject.SetActive(true);
    }
}