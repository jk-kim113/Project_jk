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
    }

    void Start()
    {   
        mMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Monster>();

        for(int i = 0; i < mPlayerController.Length; i++)
        {
            mPlayerController[i].transform.position = mWaitingTable[i].transform.position + Vector3.up * mWaitingTable[i].GetComponent<Collider>().transform.localScale.y;
        }
        
        if (mTurnExitBtn != null)
            mTurnExitBtn.onClick.AddListener(() => {

                StartCoroutine(TurnExchange());

            });
    }

    public void TotalStatus(eCurrentState state, float value)
    {
        if(state == eCurrentState.Attack)
        {
            mTotalAtk += value;
        }
        else if(state == eCurrentState.Defend)
        {
            mTotalDef += value;
        }
        else if(state == eCurrentState.Heal)
        {
            mTotalHeal += value;
        }
    }

    private IEnumerator TurnExchange()
    {
        mTurnExitBtn.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        
        yield return new WaitForSeconds(1);

        for (int i = 0; i < mPlayerController.Length; i++)
        {
            if (mPlayerController[i].GetIsBattlePos())
            {
                mPlayerController[i].NextState();
            }
        }

        mTurnExitBtn.gameObject.SetActive(true);
    }
}