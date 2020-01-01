using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterController : DataLoader
{
    public static MonsterController Instance;

    [SerializeField]
    private Monster[] mMonsterPrefabArr;
    private MonsterData[] mMonsterDataArr;

    private int mMonsterIndex;
    private Monster mMonsterSpawned;

    private int mStageLevel;

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

        mDataDummy = LoadCsvData("CSVFiles/MonsterData");
        mMonsterDataArr = new MonsterData[mDataDummy.Length - 2];
        for(int i = 0; i < mMonsterDataArr.Length; i++)
        {
            string[] splited = mDataDummy[i + 1].Split(',');
            mMonsterDataArr[i] = new MonsterData();
            mMonsterDataArr[i].Name = splited[0];
            mMonsterDataArr[i].ID = int.Parse(splited[1]);
            mMonsterDataArr[i].Attack = double.Parse(splited[2]);
            mMonsterDataArr[i].Defend = double.Parse(splited[3]);
            mMonsterDataArr[i].HPmax = double.Parse(splited[4]);
            mMonsterDataArr[i].HPcurrent = double.Parse(splited[5]);
            mMonsterDataArr[i].AttackWeight = double.Parse(splited[6]);
            mMonsterDataArr[i].DefendWeight = double.Parse(splited[7]);
            mMonsterDataArr[i].HPWeight = double.Parse(splited[8]);
        }
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

        GameController.Instance.ClearStage();
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