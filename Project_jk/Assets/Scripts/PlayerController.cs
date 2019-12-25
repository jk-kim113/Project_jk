using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        mPlayerDataArr[0].Attack = 6;
        mPlayerDataArr[0].Defend = 3;
        mPlayerDataArr[0].Heal = 1;
        mPlayerDataArr[0].HPmax = 10;
        mPlayerDataArr[0].HPcurrent = 10;
        mPlayerDataArr[0].BattleType = eBattleType.Attack;

        mPlayerDataArr[1] = new PlayerData();
        mPlayerDataArr[1].Name = "Paladin";
        mPlayerDataArr[1].ID = 1;
        mPlayerDataArr[1].Attack = 6;
        mPlayerDataArr[1].Defend = 1;
        mPlayerDataArr[1].Heal = 3;
        mPlayerDataArr[1].HPmax = 10;
        mPlayerDataArr[1].HPcurrent = 10;
        mPlayerDataArr[1].BattleType = eBattleType.Attack;

        mPlayerDataArr[2] = new PlayerData();
        mPlayerDataArr[2].Name = "Warrior";
        mPlayerDataArr[2].ID = 2;
        mPlayerDataArr[2].Attack = 3;
        mPlayerDataArr[2].Defend = 6;
        mPlayerDataArr[2].Heal = 1;
        mPlayerDataArr[2].HPmax = 10;
        mPlayerDataArr[2].HPcurrent = 10;
        mPlayerDataArr[2].BattleType = eBattleType.Defend;

        mPlayerDataArr[3] = new PlayerData();
        mPlayerDataArr[3].Name = "Guardian";
        mPlayerDataArr[3].ID = 3;
        mPlayerDataArr[3].Attack = 1;
        mPlayerDataArr[3].Defend = 6;
        mPlayerDataArr[3].Heal = 3;
        mPlayerDataArr[3].HPmax = 10;
        mPlayerDataArr[3].HPcurrent = 10;
        mPlayerDataArr[3].BattleType = eBattleType.Defend;

        mPlayerDataArr[4] = new PlayerData();
        mPlayerDataArr[4].Name = "Magician";
        mPlayerDataArr[4].ID = 4;
        mPlayerDataArr[4].Attack = 3;
        mPlayerDataArr[4].Defend = 1;
        mPlayerDataArr[4].Heal = 6;
        mPlayerDataArr[4].HPmax = 10;
        mPlayerDataArr[4].HPcurrent = 10;
        mPlayerDataArr[4].BattleType = eBattleType.Heal;

        mPlayerDataArr[5] = new PlayerData();
        mPlayerDataArr[5].Name = "Healer";
        mPlayerDataArr[5].ID = 5;
        mPlayerDataArr[5].Attack = 1;
        mPlayerDataArr[5].Defend = 3;
        mPlayerDataArr[5].Heal = 6;
        mPlayerDataArr[5].HPmax = 10;
        mPlayerDataArr[5].HPcurrent = 10;
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
                mPlayerDataArr[i].Name,
                mPlayerDataArr[i].ID,
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
}

public class PlayerData
{
    public string Name;
    public int ID;
    public double Attack;
    public double Defend;
    public double Heal;
    public double HPmax;
    public double HPcurrent;
    public eBattleType BattleType;
}