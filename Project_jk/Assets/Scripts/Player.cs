using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Waiting,
    Battle
}

public enum eBattleType
{
    Attack,
    Defend,
    Heal
}

public class Player : MonoBehaviour
{
    private string mName;
    private int mID;
    private float mATK, mDEF, mHEAL, mHPmax, mHPcurrent;
    private eBattleType mBattleType;

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

    public void Initialize(string name, int id, float atk, float def, float heal, float hpmax, float hpcurrent, eBattleType battletype)
    {
        mName = name;
        mID = id;
        mATK = atk;
        mDEF = def;
        mHEAL = heal;
        mHPmax = hpmax;
        mHPcurrent = hpcurrent;
        mBattleType = battletype;
    }

    public void Renew(float atk, float def, float heal, float hpmax, float hpcurrent)
    {
        mATK = atk;
        mDEF = def;
        mHEAL = heal;
        mHPmax = hpmax;
        mHPcurrent = hpcurrent;
    }

    public ePlayerState GetPlayerState()
    {
        return mPlayerState;
    }

    public void RaySelected()
    {
        UIController.Instance.ShowPlayerInfo(mBattleType, mATK, mDEF, mHEAL);
    }

    public void NextState()
    {
        SubtractPlayerState();

        mBattleType++;
        if ((int)mBattleType > 2)
        {
            mBattleType = 0;
        }

        CurrentBattleState(mBattleType);
    }

    public void CurrentBattleState(eBattleType state)
    {
        switch (state)
        {
            case eBattleType.Attack:
                GameController.Instance.TotalStatus(state, mATK);
                break;
            case eBattleType.Defend:
                GameController.Instance.TotalStatus(state, mDEF);
                break;
            case eBattleType.Heal:
                GameController.Instance.TotalStatus(state, mHEAL);
                break;
            default:
                Debug.LogError("Wrong State : " + state);
                break;
        }
    }

    private void SubtractPlayerState()
    {
        switch (mBattleType)
        {
            case eBattleType.Attack:
                GameController.Instance.SubtractStatus(mBattleType, mATK);
                break;
            case eBattleType.Defend:
                GameController.Instance.SubtractStatus(mBattleType, mDEF);
                break;
            case eBattleType.Heal:
                GameController.Instance.SubtractStatus(mBattleType, mHEAL);
                break;
            default:
                Debug.LogError("Wrong State : " + mBattleType);
                break;
        }
    }

    private void DetectBattleTable()
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

    private void OnMouseDown()
    {
        DetectBattleTable();
    }

    private void OnMouseDrag()
    {
        DetectBattleTable();
    }

    private void OnMouseUp()
    {
        if (mBattleTable != null)
        {
            if (mPlayerState == ePlayerState.Waiting && !mBattleTable.PlayerIsHere())
            {
                mPlayerState = ePlayerState.Battle;
                transform.position = mBattleTable.transform.position + Vector3.up * 0.5f;
                CurrentBattleState(mBattleType);
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

                SubtractPlayerState();
            }
        }
        else
        {
            mPlayerState = ePlayerState.Waiting;
            transform.position = mStartPos;

            SubtractPlayerState();
        }
    }
}
