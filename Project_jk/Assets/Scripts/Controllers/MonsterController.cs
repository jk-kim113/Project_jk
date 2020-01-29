using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterController : DataLoader
{
    public static MonsterController Instance;

#pragma warning disable 0649
    [SerializeField]
    private Transform mMonsterSpawnPos;

    [SerializeField]
    private MonsterPool mMonsterPool;
#pragma warning restore

    private MonsterData[] mMonsterDataArr;

    private int mMonsterIndex;
    private Monster mMonsterSpawned;

    private string[] mDataDummy;

    private bool bIsSpawnFinish;
    public bool IsSpawnFinish { get { return bIsSpawnFinish; } }

    private double[] ATK;
    private double[] DEF;
    private double[] HPMAX;

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

        bIsSpawnFinish = false;
    }

    public void SpawnMonster()
    {
        mMonsterIndex = UnityEngine.Random.Range(0, mMonsterDataArr.Length);

        mMonsterSpawned = mMonsterPool.GetFromPool(mMonsterIndex);

        mMonsterSpawned.transform.position = mMonsterSpawnPos.position;

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

        bIsSpawnFinish = true;

        UIController.Instance.ShowMonsterInfo(
            mMonsterDataArr[mMonsterIndex].Name,
            mMonsterDataArr[mMonsterIndex].Attack,
            mMonsterDataArr[mMonsterIndex].Defend);
    }

    public void SetActiveFalse()
    {
        if(mMonsterSpawned != null)
        {
            mMonsterSpawned.gameObject.SetActive(false);
        }
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
        GameController.Instance.ClearStage();

        for (int i = 0; i < mMonsterDataArr.Length; i++)
        {
            mMonsterDataArr[i].Attack += Math.Pow(mMonsterDataArr[i].AttackWeight, GameController.Instance.StageLevel);
            mMonsterDataArr[i].Defend += Math.Pow(mMonsterDataArr[i].DefendWeight, GameController.Instance.StageLevel);
            mMonsterDataArr[i].HPmax += Math.Pow(mMonsterDataArr[i].HPWeight, GameController.Instance.StageLevel);

            ATK[i] = mMonsterDataArr[i].Attack;
            DEF[i] = mMonsterDataArr[i].Defend;
            HPMAX[i] = mMonsterDataArr[i].HPmax;
        }
    }

    public void EffectDEFbyCard(double value)
    {
        mMonsterSpawned.Renew(
            mMonsterDataArr[mMonsterIndex].Attack,
            mMonsterDataArr[mMonsterIndex].Defend * value
            );
    }

    public void EffectATKbyCard(double value)
    {
        mMonsterSpawned.Renew(
            mMonsterDataArr[mMonsterIndex].Attack * value,
            mMonsterDataArr[mMonsterIndex].Defend
            );
    }

    public void Load(double[] atk, double[] def, double[] hp)
    {
        if (atk[0] == -1)
        {
            for (int i = 0; i < mMonsterDataArr.Length; i++)
            {
                atk[i] = mMonsterDataArr[i].Attack;
            }
        }
        if(def[0] == -1)
        {
            for (int i = 0; i < mMonsterDataArr.Length; i++)
            {
                def[i] = mMonsterDataArr[i].Defend;
            }
        }
        if(hp[0] == -1)
        {
            for (int i = 0; i < mMonsterDataArr.Length; i++)
            {
                hp[i] = mMonsterDataArr[i].HPmax;
            }
        }

        ATK = atk;
        DEF = def;
        HPMAX = hp;

        for (int i = 0; i < mMonsterDataArr.Length; i++)
        {
            mMonsterDataArr[i].Attack = atk[i];
            mMonsterDataArr[i].Defend = def[i];
            mMonsterDataArr[i].HPmax = hp[i];
        }
    }
}