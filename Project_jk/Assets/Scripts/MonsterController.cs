using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterController : MonoBehaviour
{
    public static MonsterController Instance;

    [SerializeField]
    private Monster[] mMonsterPrefabArr;
    private MonsterData[] mMonsterDataArr;

    private int mMonsterIndex;
    private Monster mMonsterSpawned;

    private int mStageLevel;

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

        #region MonsterData
        mMonsterDataArr = new MonsterData[4];

        mMonsterDataArr[0] = new MonsterData();
        mMonsterDataArr[0].Name = "Troll";
        mMonsterDataArr[0].ID = 0;
        mMonsterDataArr[0].Attack = 8;
        mMonsterDataArr[0].Defend = 2;
        mMonsterDataArr[0].HPmax = 50;
        mMonsterDataArr[0].HPcurrent = 50;
        mMonsterDataArr[0].AttackWeight = 1.212;
        mMonsterDataArr[0].DefendWeight = 1.2;
        mMonsterDataArr[0].HPWeight = 1.2;

        mMonsterDataArr[1] = new MonsterData();
        mMonsterDataArr[1].Name = "HobGoblin";
        mMonsterDataArr[1].ID = 1;
        mMonsterDataArr[1].Attack = 3;
        mMonsterDataArr[1].Defend = 7;
        mMonsterDataArr[1].HPmax = 70;
        mMonsterDataArr[1].HPcurrent = 70;
        mMonsterDataArr[1].AttackWeight = 1.21;
        mMonsterDataArr[1].DefendWeight = 1.205;
        mMonsterDataArr[1].HPWeight = 1.205;

        mMonsterDataArr[2] = new MonsterData();
        mMonsterDataArr[2].Name = "Goblin";
        mMonsterDataArr[2].ID = 2;
        mMonsterDataArr[2].Attack = 5;
        mMonsterDataArr[2].Defend = 5;
        mMonsterDataArr[2].HPmax = 40;
        mMonsterDataArr[2].HPcurrent = 40;
        mMonsterDataArr[2].AttackWeight = 1.3;
        mMonsterDataArr[2].DefendWeight = 1.3;
        mMonsterDataArr[2].HPWeight = 1.1968;

        mMonsterDataArr[3] = new MonsterData();
        mMonsterDataArr[3].Name = "Wolf";
        mMonsterDataArr[3].ID = 3;
        mMonsterDataArr[3].Attack = 9;
        mMonsterDataArr[3].Defend = 1;
        mMonsterDataArr[3].HPmax = 90;
        mMonsterDataArr[3].HPcurrent = 90;
        mMonsterDataArr[3].AttackWeight = 1.215;
        mMonsterDataArr[3].DefendWeight = 1.2;
        mMonsterDataArr[3].HPWeight = 1.208;
        #endregion
    }

    public void MonsterSpawn()
    {
        mMonsterIndex = UnityEngine.Random.Range(0, mMonsterDataArr.Length);

        mMonsterSpawned = Instantiate(mMonsterPrefabArr[mMonsterIndex]);
        mMonsterSpawned.Initialize(
            mMonsterDataArr[mMonsterIndex].Name,
            mMonsterDataArr[mMonsterIndex].ID,
            mMonsterDataArr[mMonsterIndex].Attack,
            mMonsterDataArr[mMonsterIndex].Defend,
            mMonsterDataArr[mMonsterIndex].HPmax,
            mMonsterDataArr[mMonsterIndex].HPcurrent,
            mMonsterDataArr[mMonsterIndex].AttackWeight,
            mMonsterDataArr[mMonsterIndex].DefendWeight,
            mMonsterDataArr[mMonsterIndex].HPWeight);
    }

    public void GetDamage(double damage)
    {
        mMonsterSpawned.GetDamage(damage);
    }

    public double SpawnedMonsterAttack()
    {
        return mMonsterSpawned.ATK;
    }

    public void DeadSpawnedMonster()
    {
        for(int i = 0; i < mMonsterDataArr.Length; i++)
        {
            mMonsterDataArr[i].Attack += Math.Pow(mMonsterDataArr[i].AttackWeight, mStageLevel);
            mMonsterDataArr[i].Defend += Math.Pow(mMonsterDataArr[i].DefendWeight, mStageLevel);
            mMonsterDataArr[i].HPmax += Math.Pow(mMonsterDataArr[i].HPWeight, mStageLevel);
        }

        mStageLevel++;
    }
}

public class MonsterData
{
    public string Name;
    public int ID;
    public double Attack;
    public double Defend;
    public double HPmax;
    public double HPcurrent;
    public double AttackWeight;
    public double DefendWeight;
    public double HPWeight;
}