﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private int mID, mLevel;
    private string mName;
    private double mATK, mDEF, mHEAL, mHPmax, mHPcurrent;
    public int ID { get { return mID; } }
    public string Name { get { return mName; } }
    public double ATK { get { return mATK;} set { mATK = value;}}
    public double DEF { get { return mDEF;} set { mDEF = value;}}
    public double HEAL { get { return mHEAL; } }
    public double HPmax { get { return mHPmax;}}
    public double HPcurrent { get { return mHPcurrent;}}

    private eBattleType mBattleType;
    public eBattleType BattleType { get { return mBattleType;}}

    private int mFieldCondition;
    public int FieldCondition { get { return mFieldCondition;} set { mFieldCondition = value;}}

    private int mFieldCycle;
    public int FieldCycle { get { return mFieldCycle;} set { mFieldCycle = value;}}

    private ePlayerState mPlayerState;
    public ePlayerState PlayerState { get { return mPlayerState;}}

    private Vector3 mStartPos;

    private BattleTable mBattleTable;

    private int mBattleCount;
    public int BattleCount { get { return mBattleCount;} set { mBattleCount = value;}}

    private bool bIsMove;
    public bool IsMove { get { return bIsMove; } }

    private void OnEnable()
    {
        mPlayerState = ePlayerState.Waiting;
        mFieldCycle = 0;
        bIsMove = true;
    }

    private void Start()
    {
        mStartPos = transform.position;
    }

    public void Initialize(int id, int level, string name, double atk, double def, double heal, double hpmax, double hpcurrent, eBattleType battletype)
    {   
        mID = id;
        mLevel = level;
        mName = name;
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

    public void GetDamage(double value)
    {
        if(value < 0)
        {
            Debug.LogError("Wrong Damage Value : " + value);
        }

        mHPcurrent -= value;

        if(mHPcurrent <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Healing(double value)
    {
        if(value < 0)
        {
            Debug.LogError("Wrong Damage Value : " + value);
        }

        mHPcurrent += value;

        if(mHPcurrent >= mHPmax)
        {
            mHPcurrent = mHPmax;
        }
    }

    public void WearEquipment(eEquipType equiptype, double value)
    {
        switch(equiptype)
        {
            case eEquipType.Attack:

                mATK += value;

                break;
            case eEquipType.Defend:

                mDEF += value;

                break;
            case eEquipType.Heal:

                mHEAL += value;

                break;
            default:
                Debug.LogError("Wrong EquipType : " + equiptype);
                break;
        }
    }

    public void MoveChange()
    {
        bIsMove = !bIsMove;
    }

    public void RaySelected()
    {
        UIController.Instance.ShowPlayerInfo(mBattleType, mATK, mDEF, mHEAL, mHPcurrent, mHPmax, mFieldCondition);
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
        if(!bIsMove)
        {
            return;
        }

        mBattleTable = RayController.Instance.DetectBattleTable(this);
    }

    private void OnMouseDrag()
    {
        if (!bIsMove)
        {
            return;
        }

        mBattleTable = RayController.Instance.DetectBattleTable(this);
    }

    private void OnMouseUp()
    {
        if (!bIsMove)
        {
            return;
        }

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
