using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOriginFiles : DataLoader
{
    public static LoadOriginFiles Instance;

    private PlayerData[] mPlayerDataArr;
    private MonsterData[] mMonsterDataArr;
    private CardData[] mCardDataArr;

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

        DontDestroyOnLoad(this);

        LoadPlayerOriginFile();
        LoadMonsterOriginFile();
        LoadCardOriginFile();
    }

    public PlayerData[] GetDataToPlayerController()
    {
        return mPlayerDataArr;
    }

    public MonsterData[] GetDataToMonsterController()
    {
        return mMonsterDataArr;
    }

    public CardData[] GetDataToCardController()
    {
        return mCardDataArr;
    }

    private void LoadPlayerOriginFile()
    {
        string[] mDataDummy = LoadCsvData("CSVFiles/PlayerData");
        mPlayerDataArr = new PlayerData[mDataDummy.Length - 2];
        for (int i = 0; i < mPlayerDataArr.Length; i++)
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
    }

    private void LoadMonsterOriginFile()
    {
        string[] mDataDummy = LoadCsvData("CSVFiles/MonsterData");
        mMonsterDataArr = new MonsterData[mDataDummy.Length - 2];
        for (int i = 0; i < mMonsterDataArr.Length; i++)
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

    private void LoadCardOriginFile()
    {
        mCardDataArr = new CardData[6];

        mCardDataArr[0] = new CardData();
        mCardDataArr[0].ID = 0;
        mCardDataArr[0].Contents = "배틀 상태에 있는 플레이어 전원이 Attack 상태일 경우 전체 공격력 추가 5%";

        mCardDataArr[1] = new CardData();
        mCardDataArr[1].ID = 1;
        mCardDataArr[1].Contents = "필드의 효과를 3턴 동안 무효화";

        mCardDataArr[2] = new CardData();
        mCardDataArr[2].ID = 2;
        mCardDataArr[2].Contents = "몬스터의 방어력을 3턴 동안 5%증가시키고 그 다음 5턴 동안 방어력 -5%";

        mCardDataArr[3] = new CardData();
        mCardDataArr[3].ID = 3;
        mCardDataArr[3].Contents = "Ice Field에 의해 얼려진 플레이어 효과 해제";

        mCardDataArr[4] = new CardData();
        mCardDataArr[4].ID = 4;
        mCardDataArr[4].Contents = "몬스터의 공격력 5% 증가시키고 전체 체력 5% 증가";

        mCardDataArr[5] = new CardData();
        mCardDataArr[5].ID = 5;
        mCardDataArr[5].Contents = "대기중인 플레이어의 체력이 50%이하이면 최대체력의 30%를 회복";
    }
}
