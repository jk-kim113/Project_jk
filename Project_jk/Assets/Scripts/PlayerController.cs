using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : DataLoader
{
    public static PlayerController Instance;

#pragma warning disable 0649
    [SerializeField]
    private Player[] mPlayerPrefabArr;
    [SerializeField]
    private Transform[] mPlayerSpawnPosArr;
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
    }
    private double mTotalDef;
    private double mTotalHeal;

    private double mHPmax;
    private double mHPcurrent;

    private string[] mDataDummy;

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

        mDataDummy = LoadCsvData("CSVFiles/PlayerData");
        mPlayerDataArr = new PlayerData[mDataDummy.Length - 2];
        for(int i = 0; i < mPlayerDataArr.Length; i++)
        {
            string[] splited = mDataDummy[i + 1].Split(',');
            mPlayerDataArr[i] = new PlayerData();
            mPlayerDataArr[i].Name = splited[0];
            mPlayerDataArr[i].ID = int.Parse(splited[1]);
            mPlayerDataArr[i].Level = int.Parse(splited[2]);
            mPlayerDataArr[i].EXPcurrent = double.Parse(splited[3]);
            mPlayerDataArr[i].EXPmax = double.Parse(splited[4]);
            mPlayerDataArr[i].Attack = double.Parse(splited[5]);
            mPlayerDataArr[i].Defend = double.Parse(splited[6]);
            mPlayerDataArr[i].Heal = double.Parse(splited[7]);
            mPlayerDataArr[i].AttackBase = double.Parse(splited[8]);
            mPlayerDataArr[i].AttackWeight = double.Parse(splited[9]);
            mPlayerDataArr[i].DefendBase = double.Parse(splited[10]);
            mPlayerDataArr[i].DefendWeight = double.Parse(splited[11]);
            mPlayerDataArr[i].HealBase = double.Parse(splited[12]);
            mPlayerDataArr[i].HealWeight = double.Parse(splited[13]);
            mPlayerDataArr[i].HPmax = double.Parse(splited[14]);
            mPlayerDataArr[i].HPcurrent = double.Parse(splited[15]);
            mPlayerDataArr[i].HPbase = double.Parse(splited[16]);
            mPlayerDataArr[i].HPWeight = double.Parse(splited[17]);
            mPlayerDataArr[i].BattleType = (eBattleType)int.Parse(splited[18]);
        }

        mPlayerSpawnedList = new List<Player>();

        mHPmax = 100;
        mHPcurrent = mHPmax;
    }

    public void SpawnPlayers()
    {
        for (int i = 0; i < mPlayerPrefabArr.Length; i++)
        {
            Player player = Instantiate(mPlayerPrefabArr[i]);
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

        UIController.Instance.ShowPlayerGaugeBar(mHPcurrent, mHPmax);
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
                            mPlayerDataArr[i].HPcurrent -= cycleValue;
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
        switch (state)
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

    public void SubtractStatus(eBattleType state, double value)
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

                mPlayerDataArr[i].Attack = mPlayerDataArr[i].AttackBase * Math.Pow(mPlayerDataArr[i].AttackWeight, mPlayerDataArr[i].Level - 1);
                mPlayerDataArr[i].Defend = mPlayerDataArr[i].DefendBase * Math.Pow(mPlayerDataArr[i].DefendWeight, mPlayerDataArr[i].Level - 1);
                mPlayerDataArr[i].Heal = mPlayerDataArr[i].HealBase * Math.Pow(mPlayerDataArr[i].HealWeight, mPlayerDataArr[i].Level - 1);

                mPlayerDataArr[i].HPmax = mPlayerDataArr[i].HPbase * Math.Pow(mPlayerDataArr[i].HPWeight, mPlayerDataArr[i].Level - 1);
                mPlayerDataArr[i].HPcurrent = mPlayerDataArr[i].HPmax;

                mPlayerSpawnedList[i].Renew(
                    mPlayerDataArr[i].Attack,
                    mPlayerDataArr[i].Defend,
                    mPlayerDataArr[i].Heal,
                    mPlayerDataArr[i].HPmax,
                    mPlayerDataArr[i].HPcurrent);
            }
        }
    }
}