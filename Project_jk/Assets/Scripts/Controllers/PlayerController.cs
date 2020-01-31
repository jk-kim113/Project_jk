using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

#pragma warning disable 0649
    [SerializeField]
    private Transform[] mPlayerSpawnPosArr;
    [SerializeField]
    private PlayerPool mPlayerPool;
#pragma warning restore

    private List<Player> mPlayerSpawnedList;

    private PlayerData[] mPlayerDataArr;

    private double mTotalAtk;
    public double TotalAtk
    {
        get
        {
            return mTotalAtk;
        }
        set
        {
            mTotalAtk = value;
            UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);
        }
    }
    private double mTotalDef;
    private double mTotalHeal;

    private double mHPmax;
    private double mHPcurrent;

    private double mOriginalHPmax;

    private double mPlayerAtk;
    public double PlayerAtk { get { return mPlayerAtk; } }

    private bool bIsSpawnFinish;
    public bool IsSpawnFinish { get { return bIsSpawnFinish; } }

    private int[] Levels;

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

        mPlayerDataArr = LoadOriginFiles.Instance.GetDataToPlayerController();

        mPlayerSpawnedList = new List<Player>();

        mHPmax = 100;
        mHPcurrent = mHPmax;

        mOriginalHPmax = mHPmax;
        bIsSpawnFinish = false;
    }

    public void SpawnPlayers()
    {
        mTotalAtk = 0;
        mPlayerAtk = 0;
        mTotalDef = 0;
        mTotalHeal = 0;
        UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);

        mPlayerSpawnedList.Clear();

        for (int i = 0; i < mPlayerDataArr.Length; i++)
        {
            Player player = mPlayerPool.GetFromPool(i);
            player.transform.position = mPlayerSpawnPosArr[i].position;
            player.Initialize(
                mPlayerDataArr[i].ID,
                mPlayerDataArr[i].Level,
                mPlayerDataArr[i].Attack,
                mPlayerDataArr[i].Defend,
                mPlayerDataArr[i].Heal,
                mPlayerDataArr[i].HPmax,
                mPlayerDataArr[i].HPcurrent,
                mPlayerDataArr[i].BattleType);
            mPlayerSpawnedList.Add(player);
        }

        bIsSpawnFinish = true;
        UIController.Instance.ShowPlayerGaugeBar(mHPcurrent, mHPmax);
    }

    public void SetActiveFalse()
    {
        for(int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            mPlayerSpawnedList[i].gameObject.SetActive(false);
        }
    }

    public void GetDamage(double value)
    {
        if(value > mTotalDef)
        {
            mHPcurrent -= value - mTotalDef;
        }

        UIController.Instance.ShowPlayerGaugeBar(mHPcurrent, mHPmax);
    }

    public void GetFieldEffect(eFieldType fieldtype, int cycle, double cycleValue, int condition, double conditionValue)
    {
        for (int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            if (mPlayerSpawnedList[i].PlayerState == ePlayerState.Battle)
            {
                switch (fieldtype)
                {
                    case eFieldType.Normal:
                        break;
                    case eFieldType.Fire:

                        mPlayerSpawnedList[i].FieldCycle++;
                        if(mPlayerSpawnedList[i].FieldCycle >= cycle)
                        {
                            mPlayerSpawnedList[i].ATK *= (100 - cycleValue) / 100;
                            mPlayerSpawnedList[i].FieldCycle = 0;
                            mPlayerSpawnedList[i].FieldCondition++;
                        }
                        if(mPlayerSpawnedList[i].FieldCondition >= condition)
                        {
                            mPlayerSpawnedList[i].GetDamage(mPlayerSpawnedList[i].HPmax * (conditionValue / 100));
                            mPlayerSpawnedList[i].FieldCondition = 0;
                        }
                        
                        break;
                    case eFieldType.Ice:
                        if(!mPlayerSpawnedList[i].IsMove)
                        {
                            if(mPlayerSpawnedList[i].FieldCondition >= 3)
                            {
                                mPlayerSpawnedList[i].MoveChange();
                            }

                            mPlayerSpawnedList[i].FieldCondition++;
                        }

                        mPlayerSpawnedList[i].FieldCycle++;
                        if (mPlayerSpawnedList[i].FieldCycle >= cycle)
                        {
                            mPlayerSpawnedList[i].DEF *= (100 - cycleValue) / 100;
                            mPlayerSpawnedList[i].FieldCycle = 0;
                            mPlayerSpawnedList[i].FieldCondition++;
                        }
                        if (mPlayerSpawnedList[i].FieldCondition >= condition)
                        {
                            mPlayerSpawnedList[i].MoveChange();
                            mPlayerSpawnedList[i].FieldCondition = 0;
                        }

                        break;
                    case eFieldType.Poison:

                        mPlayerSpawnedList[i].FieldCycle++;
                        if (mPlayerSpawnedList[i].FieldCycle >= cycle)
                        {
                            mPlayerSpawnedList[i].GetDamage(cycleValue);
                            mPlayerSpawnedList[i].FieldCycle = 0;
                            mPlayerSpawnedList[i].FieldCondition++;
                        }
                        if (mPlayerSpawnedList[i].FieldCondition >= condition)
                        {
                            mPlayerSpawnedList[i].GetDamage(cycleValue * (1 + conditionValue / 100));
                            mPlayerSpawnedList[i].FieldCondition = 0;
                        }

                        break;
                    default:
                        Debug.LogError("Wrong Field Type : " + fieldtype);
                        break;
                }
            }
        }
    }

    public void NextBattleType()
    {
        for (int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            if (mPlayerSpawnedList[i].PlayerState == ePlayerState.Battle)
            {
                mPlayerSpawnedList[i].NextState();
            }
        }
    }

    public void TotalStatus(eBattleType state, double value)
    {
        value = Math.Round(value);

        switch (state)
        {
            case eBattleType.Attack:
                mPlayerAtk += value;
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

        mTotalAtk = mPlayerAtk;
        UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);
    }

    public void SubtractStatus(eBattleType state, double value)
    {
        value = Math.Round(value);

        switch (state)
        {
            case eBattleType.Attack:
                mPlayerAtk -= value;
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

        mTotalAtk = mPlayerAtk;
        UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);
    }

    public void AddEXP()
    {
        for (int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            mPlayerDataArr[i].EXPcurrent += mPlayerDataArr[i].Level * mPlayerSpawnedList[i].BattleCount;
            mPlayerSpawnedList[i].BattleCount = 0;

            while(mPlayerDataArr[i].EXPcurrent >= mPlayerDataArr[i].EXPmax)
            {
                mPlayerDataArr[i].Level++;
                double overExp = mPlayerDataArr[i].EXPcurrent - mPlayerDataArr[i].EXPmax;
                mPlayerDataArr[i].EXPcurrent = overExp;
                mPlayerDataArr[i].EXPmax *= mPlayerDataArr[i].Level;

                CalculateStatus(i);

                Levels[i] = mPlayerDataArr[i].Level;
            }
        }
    }

    private void CalculateStatus(int i)
    {
        mPlayerDataArr[i].Attack = mPlayerDataArr[i].AttackBase * Math.Pow(mPlayerDataArr[i].AttackWeight, mPlayerDataArr[i].Level - 1);
        mPlayerDataArr[i].Defend = mPlayerDataArr[i].DefendBase * Math.Pow(mPlayerDataArr[i].DefendWeight, mPlayerDataArr[i].Level - 1);
        mPlayerDataArr[i].Heal = mPlayerDataArr[i].HealBase * Math.Pow(mPlayerDataArr[i].HealWeight, mPlayerDataArr[i].Level - 1);
        mPlayerDataArr[i].HPmax = mPlayerDataArr[i].HPbase * Math.Pow(mPlayerDataArr[i].HPWeight, mPlayerDataArr[i].Level - 1);

        mPlayerDataArr[i].Attack = Math.Round(mPlayerDataArr[i].Attack);
        mPlayerDataArr[i].Defend = Math.Round(mPlayerDataArr[i].Defend);
        mPlayerDataArr[i].HPmax = Math.Round(mPlayerDataArr[i].HPmax);
        mPlayerDataArr[i].HPcurrent = mPlayerDataArr[i].HPmax;

        mPlayerSpawnedList[i].Renew(
            mPlayerDataArr[i].Attack,
            mPlayerDataArr[i].Defend,
            mPlayerDataArr[i].Heal,
            mPlayerDataArr[i].HPmax,
            mPlayerDataArr[i].HPcurrent);
    }

    public bool CheckAllBattleState()
    {
        int battlePlayer = 0;
        int attackPlayer = 0;

        for(int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            if(mPlayerSpawnedList[i].PlayerState == ePlayerState.Battle )
            {
                battlePlayer++;
                if(mPlayerSpawnedList[i].BattleType == eBattleType.Attack)
                {
                    attackPlayer++;
                }
            }
        }

        if(battlePlayer == attackPlayer && battlePlayer != 0)
        {
            return true;
        }

        return false;
    }

    public void EffectATKbyCard(double value)
    {
        mTotalAtk = value;
        UIController.Instance.ShowTotalStatus(mTotalAtk, mTotalDef, mTotalHeal);
    }

    public void EffectDeIcebyCard()
    {
        for(int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            if(!mPlayerSpawnedList[i].IsMove)
            {
                mPlayerSpawnedList[i].MoveChange();
            }
        }
    }

    public void EffectHPmaxByCard(double value)
    {
        mHPmax = mOriginalHPmax * value;

        UIController.Instance.ShowPlayerGaugeBar(mHPcurrent, mHPmax);
    }

    public void EffectHealByCard()
    {
        for (int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            if (mPlayerSpawnedList[i].PlayerState == ePlayerState.Waiting)
            {
                if (mPlayerSpawnedList[i].HPcurrent < mPlayerSpawnedList[i].HPmax * 0.5)
                {
                    mPlayerSpawnedList[i].Healing(mPlayerSpawnedList[i].HPmax * 0.3);
                }
            }
        }
    }

    public void Load(int[] levels)
    {
        Levels = levels;

        for(int i = 0; i < mPlayerDataArr.Length; i++)
        {
            mPlayerDataArr[i].Level = levels[i];

            CalculateStatus(i);
        }
    }
}
