using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private Player[] mPlayerPrefabArr;
    [SerializeField]
    private Transform[] mPlayerSpawnPosArr;
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

        #region Playerdata
        mPlayerDataArr = new PlayerData[6];

        mPlayerDataArr[0] = new PlayerData();
        mPlayerDataArr[0].Name = "Berserker";
        mPlayerDataArr[0].ID = 0;
        mPlayerDataArr[0].Level = 1;
        mPlayerDataArr[0].EXPcurrent = 0;
        mPlayerDataArr[0].EXPmax = 15;
        mPlayerDataArr[0].Attack = 6;
        mPlayerDataArr[0].Defend = 3;
        mPlayerDataArr[0].Heal = 1;
        mPlayerDataArr[0].AttackBase = 6;
        mPlayerDataArr[0].AttackWeight = 1.2;
        mPlayerDataArr[0].DefendBase = 3;
        mPlayerDataArr[0].DefendWeight = 1.2;
        mPlayerDataArr[0].HealBase = 1;
        mPlayerDataArr[0].HealWeight = 1.2;
        mPlayerDataArr[0].HPmax = 10;
        mPlayerDataArr[0].HPcurrent = 10;
        mPlayerDataArr[0].HPbase = 10;
        mPlayerDataArr[0].HPWeight = 1.2;
        mPlayerDataArr[0].BattleType = eBattleType.Attack;

        mPlayerDataArr[1] = new PlayerData();
        mPlayerDataArr[1].Name = "Paladin";
        mPlayerDataArr[1].ID = 1;
        mPlayerDataArr[1].Level = 1;
        mPlayerDataArr[1].EXPcurrent = 0;
        mPlayerDataArr[1].EXPmax = 15;
        mPlayerDataArr[1].Attack = 6;
        mPlayerDataArr[1].Defend = 1;
        mPlayerDataArr[1].Heal = 3;
        mPlayerDataArr[1].AttackBase = 6;
        mPlayerDataArr[1].AttackWeight = 1.2;
        mPlayerDataArr[1].DefendBase = 1;
        mPlayerDataArr[1].DefendWeight = 1.2;
        mPlayerDataArr[1].HealBase = 3;
        mPlayerDataArr[1].HealWeight = 1.2;
        mPlayerDataArr[1].HPmax = 10;
        mPlayerDataArr[1].HPcurrent = 10;
        mPlayerDataArr[1].HPbase = 10;
        mPlayerDataArr[1].HPWeight = 1.2;
        mPlayerDataArr[1].BattleType = eBattleType.Attack;

        mPlayerDataArr[2] = new PlayerData();
        mPlayerDataArr[2].Name = "Warrior";
        mPlayerDataArr[2].ID = 2;
        mPlayerDataArr[2].Level = 1;
        mPlayerDataArr[2].EXPcurrent = 0;
        mPlayerDataArr[2].EXPmax = 15;
        mPlayerDataArr[2].Attack = 3;
        mPlayerDataArr[2].Defend = 6;
        mPlayerDataArr[2].Heal = 1;
        mPlayerDataArr[2].AttackBase = 3;
        mPlayerDataArr[2].AttackWeight = 1.2;
        mPlayerDataArr[2].DefendBase = 6;
        mPlayerDataArr[2].DefendWeight = 1.2;
        mPlayerDataArr[2].HealBase = 1;
        mPlayerDataArr[2].HealWeight = 1.2;
        mPlayerDataArr[2].HPmax = 10;
        mPlayerDataArr[2].HPcurrent = 10;
        mPlayerDataArr[2].HPbase = 10;
        mPlayerDataArr[2].HPWeight = 1.2;
        mPlayerDataArr[2].BattleType = eBattleType.Defend;

        mPlayerDataArr[3] = new PlayerData();
        mPlayerDataArr[3].Name = "Guardian";
        mPlayerDataArr[3].ID = 3;
        mPlayerDataArr[3].Level = 1;
        mPlayerDataArr[3].EXPcurrent = 0;
        mPlayerDataArr[3].EXPmax = 15;
        mPlayerDataArr[3].Attack = 1;
        mPlayerDataArr[3].Defend = 6;
        mPlayerDataArr[3].Heal = 3;
        mPlayerDataArr[3].AttackBase = 1;
        mPlayerDataArr[3].AttackWeight = 1.2;
        mPlayerDataArr[3].DefendBase = 6;
        mPlayerDataArr[3].DefendWeight = 1.2;
        mPlayerDataArr[3].HealBase = 3;
        mPlayerDataArr[3].HealWeight = 1.2;
        mPlayerDataArr[3].HPmax = 10;
        mPlayerDataArr[3].HPcurrent = 10;
        mPlayerDataArr[3].HPbase = 10;
        mPlayerDataArr[3].HPWeight = 1.2;
        mPlayerDataArr[3].BattleType = eBattleType.Defend;

        mPlayerDataArr[4] = new PlayerData();
        mPlayerDataArr[4].Name = "Magician";
        mPlayerDataArr[4].ID = 4;
        mPlayerDataArr[4].Level = 1;
        mPlayerDataArr[4].EXPcurrent = 0;
        mPlayerDataArr[4].EXPmax = 15;
        mPlayerDataArr[4].Attack = 3;
        mPlayerDataArr[4].Defend = 1;
        mPlayerDataArr[4].Heal = 6;
        mPlayerDataArr[4].AttackBase = 3;
        mPlayerDataArr[4].AttackWeight = 1.2;
        mPlayerDataArr[4].DefendBase = 1;
        mPlayerDataArr[4].DefendWeight = 1.2;
        mPlayerDataArr[4].HealBase = 6;
        mPlayerDataArr[4].HealWeight = 1.2;
        mPlayerDataArr[4].HPmax = 10;
        mPlayerDataArr[4].HPcurrent = 10;
        mPlayerDataArr[4].HPbase = 10;
        mPlayerDataArr[4].HPWeight = 1.2;
        mPlayerDataArr[4].BattleType = eBattleType.Heal;

        mPlayerDataArr[5] = new PlayerData();
        mPlayerDataArr[5].Name = "Healer";
        mPlayerDataArr[5].ID = 5;
        mPlayerDataArr[5].Level = 1;
        mPlayerDataArr[5].EXPcurrent = 0;
        mPlayerDataArr[5].EXPmax = 15;
        mPlayerDataArr[5].Attack = 1;
        mPlayerDataArr[5].Defend = 3;
        mPlayerDataArr[5].Heal = 6;
        mPlayerDataArr[5].AttackBase = 1;
        mPlayerDataArr[5].AttackWeight = 1.2;
        mPlayerDataArr[5].DefendBase = 3;
        mPlayerDataArr[5].DefendWeight = 1.2;
        mPlayerDataArr[5].HealBase = 6;
        mPlayerDataArr[5].HealWeight = 1.2;
        mPlayerDataArr[5].HPmax = 10;
        mPlayerDataArr[5].HPcurrent = 10;
        mPlayerDataArr[5].HPbase = 10;
        mPlayerDataArr[5].HPWeight = 1.2;
        mPlayerDataArr[5].BattleType = eBattleType.Heal;
        #endregion

        mPlayerSpawnedList = new List<Player>();

        mHPmax = 100;
        mHPcurrent = mHPmax;
    }

    private void Start()
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

    public void NextBattleType()
    {
        for (int i = 0; i < mPlayerSpawnedList.Count; i++)
        {
            if (mPlayerSpawnedList[i].GetPlayerState() == ePlayerState.Battle)
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

public class PlayerData
{
    public string Name;
    public int ID;
    public int Level;
    public double EXPcurrent;
    public double EXPmax;
    public double Attack;
    public double Defend;
    public double Heal;
    public double AttackBase;
    public double AttackWeight;
    public double DefendBase;
    public double DefendWeight;
    public double HealBase;
    public double HealWeight;
    public double HPmax;
    public double HPcurrent;
    public double HPbase;
    public double HPWeight;
    public eBattleType BattleType;
}