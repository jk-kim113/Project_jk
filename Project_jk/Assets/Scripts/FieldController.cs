using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : DataLoader
{
    public static FieldController Instance;

    private readonly int[] mSpawnXcord = { 0, 80, 160, 240 };

#pragma warning disable 0649
    [SerializeField]
    private Field[] mFieldPrefabArr;

    [SerializeField]
    private Transform[] mFieldSpawnPosArr;

    [SerializeField]
    private MoveViewPoint mMoveViewPoint;
#pragma warning restore

    private FieldData[] mFieldDataArr;

    private string[] mDataDummy;

    private int mSpawnedFieldID;

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

        mDataDummy = LoadCsvData("CSVFiles/FieldData");
        mFieldDataArr = new FieldData[mDataDummy.Length - 2];
        for (int i = 0; i < mFieldDataArr.Length; i++)
        {
            string[] splited = mDataDummy[i + 1].Split(',');
            mFieldDataArr[i] = new FieldData();
            mFieldDataArr[i].Name = splited[0];
            mFieldDataArr[i].ID = int.Parse(splited[1]);
            mFieldDataArr[i].Info = splited[2];
            mFieldDataArr[i].Cycle = int.Parse(splited[3]);
            mFieldDataArr[i].CycleValue = double.Parse(splited[4]);
            mFieldDataArr[i].Condition = int.Parse(splited[5]);
            mFieldDataArr[i].ConditionValue = double.Parse(splited[6]);
        }

        for (int i = 0; i < mFieldPrefabArr.Length; i++)
        {
            Field field = Instantiate(mFieldPrefabArr[i]);
            field.transform.position = mFieldSpawnPosArr[i].position;
        }
    }

    public void SpawnField()
    {
        mSpawnedFieldID = Random.Range(0, 4);

        mMoveViewPoint.Move(mSpawnXcord[mSpawnedFieldID]);

        UIController.Instance.ShowFieldStatus(
            mFieldDataArr[mSpawnedFieldID].Name,
            mFieldDataArr[mSpawnedFieldID].Info,
            mFieldDataArr[mSpawnedFieldID].Cycle,
            mFieldDataArr[mSpawnedFieldID].CycleValue,
            mFieldDataArr[mSpawnedFieldID].Condition,
            mFieldDataArr[mSpawnedFieldID].ConditionValue);
    }

    public void FieldEffect()
    {
        PlayerController.Instance.GetFieldEffect(
            (eFieldType)mSpawnedFieldID,
            mFieldDataArr[mSpawnedFieldID].Cycle,
            mFieldDataArr[mSpawnedFieldID].CycleValue,
            mFieldDataArr[mSpawnedFieldID].Condition,
            mFieldDataArr[mSpawnedFieldID].ConditionValue);
    }
}
