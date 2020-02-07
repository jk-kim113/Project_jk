using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOriginFiles : DataLoader
{
    public static LoadOriginFiles Instance;

    private PlayerData[] mPlayerDataArr;
    private MonsterData[] mMonsterDataArr;
    private CardData[] mCardDataArr;
    private ItemData[] mItemDataArr;
    private EquipData[] mEquipDataArr;

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
        LoadItemOriginData();
        LoadEquipOriginData();
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

    public ItemData[] LoadItemDataArr()
    {
        return mItemDataArr;
    }

    public EquipData[] LoadEquipDataArr()
    {
        return mEquipDataArr;
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

    private void LoadItemOriginData()
    {
        mItemDataArr = new ItemData[3];

        mItemDataArr[0] = new ItemData();
        mItemDataArr[0].Name = "체력 회복(소)";
        mItemDataArr[0].ID = 11;
        mItemDataArr[0].Info = "플레이어의 체력을 {0}만큼 회복";
        mItemDataArr[0].Value = 10;
        mItemDataArr[0].Cost = 50;

        mItemDataArr[1] = new ItemData();
        mItemDataArr[1].Name = "체력 회복(중)";
        mItemDataArr[1].ID = 12;
        mItemDataArr[1].Info = "플레이어의 체력을 {0}만큼 회복";
        mItemDataArr[1].Value = 50;
        mItemDataArr[1].Cost = 250;

        mItemDataArr[2] = new ItemData();
        mItemDataArr[2].Name = "체력 회복(대)";
        mItemDataArr[2].ID = 13;
        mItemDataArr[2].Info = "플레이어의 체력을 {0}만큼 회복";
        mItemDataArr[2].Value = 100;
        mItemDataArr[2].Cost = 500;
    }

    private void LoadEquipOriginData()
    {
        mEquipDataArr = new EquipData[3];

        mEquipDataArr[0] = new EquipData();
        mEquipDataArr[0].Name = "낡은 장검";
        mEquipDataArr[0].ID = 21;
        mEquipDataArr[0].Info = "장착 시 공격력이 {0}상승";
        mEquipDataArr[0].Value = 3;
        mEquipDataArr[0].Cost = 500;
        mEquipDataArr[0].EquipType = eEquipType.Attack;

        mEquipDataArr[1] = new EquipData();
        mEquipDataArr[1].Name = "낡은 가죽";
        mEquipDataArr[1].ID = 22;
        mEquipDataArr[1].Info = "장착 시 방어력이 {0}상승";
        mEquipDataArr[1].Value = 3;
        mEquipDataArr[1].Cost = 500;
        mEquipDataArr[1].EquipType = eEquipType.Defend;

        mEquipDataArr[2] = new EquipData();
        mEquipDataArr[2].Name = "낡은 지팡이";
        mEquipDataArr[2].ID = 23;
        mEquipDataArr[2].Info = "장착 시 힐량이 {0}상승";
        mEquipDataArr[2].Value = 3;
        mEquipDataArr[2].Cost = 500;
        mEquipDataArr[2].EquipType = eEquipType.Heal;
    }
}
