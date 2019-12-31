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
    private int mID, mLevel;
    private double mATK, mDEF, mHEAL, mHPmax, mHPcurrent;
    private eBattleType mBattleType;

    private ePlayerState mPlayerState;

    private Vector3 mStartPos;

    private BattleTable mBattleTable;

    private int mBattleCount;
    public int BattleCount
    {
        get
        {
            return mBattleCount;
        }
        set
        {
            mBattleCount = value;
        }
    }

    private void Awake()
    {
        mPlayerState = ePlayerState.Waiting;
    }

    private void Start()
    {
        mStartPos = transform.position;
    }

    public void Initialize(int id, int level, double atk, double def, double heal, double hpmax, double hpcurrent, eBattleType battletype)
    {   
        mID = id;
        mLevel = level;
        Renew(atk, def, heal, hpmax, hpcurrent);
        mBattleType = battletype;
    }

    public void Renew(double atk, double def, double heal, double hpmax, double hpcurrent)
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
        mBattleCount++;
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
                PlayerController.Instance.TotalStatus(state, mATK);
                break;
            case eBattleType.Defend:
                PlayerController.Instance.TotalStatus(state, mDEF);
                break;
            case eBattleType.Heal:
                PlayerController.Instance.TotalStatus(state, mHEAL);
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
                PlayerController.Instance.SubtractStatus(mBattleType, mATK);
                break;
            case eBattleType.Defend:
                PlayerController.Instance.SubtractStatus(mBattleType, mDEF);
                break;
            case eBattleType.Heal:
                PlayerController.Instance.SubtractStatus(mBattleType, mHEAL);
                break;
            default:
                Debug.LogError("Wrong State : " + mBattleType);
                break;
        }
    }

    private void OnMouseDown()
    {
        mBattleTable = RayController.Instance.DetectBattleTable(this);
    }

    private void OnMouseDrag()
    {   
        mBattleTable = RayController.Instance.DetectBattleTable(this);
    }

    private void OnMouseUp()
    {
        if (mBattleTable != null)
        {
            if(!mBattleTable.PlayerIsHere())
            {
                switch(mPlayerState)
                {
                    case ePlayerState.Waiting:

                        transform.position = mBattleTable.transform.position + Vector3.up * 0.5f;
                        mPlayerState = ePlayerState.Battle;
                        CurrentBattleState(mBattleType);
                        mBattleTable.StateChange();

                        break;
                    case ePlayerState.Battle:

                        transform.position = mStartPos;
                        SubtractPlayerState();
                        mPlayerState = ePlayerState.Waiting;

                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (mPlayerState)
                {
                    case ePlayerState.Waiting:

                        transform.position = mStartPos;
                        mPlayerState = ePlayerState.Waiting;

                        break;
                    case ePlayerState.Battle:

                        transform.position = mStartPos;
                        SubtractPlayerState();
                        mPlayerState = ePlayerState.Waiting;

                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            if(mPlayerState == ePlayerState.Battle)
            {
                SubtractPlayerState();
            }

            mPlayerState = ePlayerState.Waiting;
            transform.position = mStartPos;
        }
    }
}
