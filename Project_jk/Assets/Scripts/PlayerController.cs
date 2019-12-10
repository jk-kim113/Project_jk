using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Waiting,
    Battle
}

public enum eBattleState
{
    Attack,
    Defend,
    Heal
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    [SerializeField]
    private eBattleState mBattleState;
    private ePlayerState mPlayerState;

    private Vector3 mStartPos;

    private BattleTable mBattleTable;

    private void Awake()
    {
        mPlayerState = ePlayerState.Waiting;
    }

    private void Start()
    {
        mStartPos = transform.position;
    }

    public void RaySelected()
    {
        UIController.Instance.ShowPlayerInfo(mBattleState, mATK, mDEF, mHEAL);
    }

    public void NextState()
    {
        mBattleState++;
        if ((int)mBattleState > 2)
        {
            mBattleState = 0;
        }

        CurrentBattleState(mBattleState);
    }

    public void CurrentBattleState(eBattleState state)
    {
        switch (state)
        {
            case eBattleState.Attack:
                GameController.Instance.TotalStatus(state, mATK);
                break;
            case eBattleState.Defend:
                GameController.Instance.TotalStatus(state, mDEF);
                break;
            case eBattleState.Heal:
                GameController.Instance.TotalStatus(state, mHEAL);
                break;
            default:
                Debug.LogError("Wrong State : " + state);
                break;
        }
    }

    private void OnMouseDown()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 70);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("BattleTable"))
            {
                mBattleTable = hit.collider.gameObject.GetComponent<BattleTable>();
                if (mBattleTable.PlayerIsHere() && mPlayerState == ePlayerState.Battle)
                {
                    mBattleTable.StateChange();
                }
            }
            else
            {
                mBattleTable = null;
            }
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 70);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("BattleTable"))
            {
                mBattleTable = hit.collider.gameObject.GetComponent<BattleTable>();
                if(mBattleTable.PlayerIsHere() && mPlayerState == ePlayerState.Battle)
                {
                    mBattleTable.StateChange();
                }
            }
            else
            {
                mBattleTable = null;
            }
        }
    }

    private void OnMouseUp()
    {
        if(mBattleTable != null)
        {
            if (mPlayerState == ePlayerState.Waiting && !mBattleTable.PlayerIsHere())
            {
                mPlayerState = ePlayerState.Battle;
                transform.position = mBattleTable.transform.position + Vector3.up * 0.5f;
                CurrentBattleState(mBattleState);
                mBattleTable.StateChange();
            }
            else
            {
                if (mBattleTable.PlayerIsHere() && mPlayerState == ePlayerState.Battle)
                {
                    mBattleTable.StateChange();
                }
                mPlayerState = ePlayerState.Waiting;
                transform.position = mStartPos;

                switch (mBattleState)
                {
                    case eBattleState.Attack:
                        GameController.Instance.SubtractStatus(mBattleState, mATK);
                        break;
                    case eBattleState.Defend:
                        GameController.Instance.SubtractStatus(mBattleState, mDEF);
                        break;
                    case eBattleState.Heal:
                        GameController.Instance.SubtractStatus(mBattleState, mHEAL);
                        break;
                    default:
                        Debug.LogError("Wrong State : " + mBattleState);
                        break;
                }
            }
        }
        else
        {
            mPlayerState = ePlayerState.Waiting;
            transform.position = mStartPos;

            switch (mBattleState)
            {
                case eBattleState.Attack:
                    GameController.Instance.SubtractStatus(mBattleState, mATK);
                    break;
                case eBattleState.Defend:
                    GameController.Instance.SubtractStatus(mBattleState, mDEF);
                    break;
                case eBattleState.Heal:
                    GameController.Instance.SubtractStatus(mBattleState, mHEAL);
                    break;
                default:
                    Debug.LogError("Wrong State : " + mBattleState);
                    break;
            }
        }
    }
}
