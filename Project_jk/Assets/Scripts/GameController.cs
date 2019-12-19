using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private float mTotalAtk;
    private float mTotalDef;
    private float mTotalHeal;

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
    }

    void Start()
    {   
        if (mTurnExitBtn != null)
            mTurnExitBtn.onClick.AddListener(() => {

                StartCoroutine(TurnExchange());

            });
    }

    public void TotalStatus(eBattleType state, float value)
    {
        switch(state)
        {
            case eBattleType.Attack:
                mTotalAtk += value;
                break;
            case eBattleType.Defend:
                mTotalDef += value;
                break;
            case eBattleType.Heal:
                mTotalHeal += value;
                break;
            default:
                break;
        }

        UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);
    }

    public void SubtractStatus(eBattleType state, float value)
    {
        switch (state)
        {
            case eBattleType.Attack:
                mTotalAtk -= value;
                break;
            case eBattleType.Defend:
                mTotalDef -= value;
                break;
            case eBattleType.Heal:
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

        PlayerController.Instance.NextBattleType();

        mTurnExitBtn.gameObject.SetActive(true);
    }
}