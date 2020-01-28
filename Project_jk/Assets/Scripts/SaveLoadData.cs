﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoadData : MonoBehaviour
{
    public static SaveLoadData Instance;

    private SaveData mSaveData;
    public SaveData SaveData { get { return mSaveData; } }

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

        Load();
    }


    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        formatter.Serialize(stream, mSaveData);

        string data = Convert.ToBase64String(stream.GetBuffer());

        PlayerPrefs.SetString("SaveData", data);
        stream.Close();
    }


    public void Load()
    {
        string data = PlayerPrefs.GetString("SaveData");

        if(!string.IsNullOrEmpty(data))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));

            mSaveData = (SaveData)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            mSaveData = new SaveData();
            mSaveData.PlayerLevel = new int[StaticValue.PLAYER_LEVEL_LENGTH];

            for(int i = 0; i < mSaveData.PlayerLevel.Length; i++)
            {
                mSaveData.PlayerLevel[i] = 1;
            }

            mSaveData.StageLevel = 1;

            mSaveData.MonsterAttack = new double[StaticValue.MONSTER_LENGTH];
            mSaveData.MonsterDefend = new double[StaticValue.MONSTER_LENGTH];
            mSaveData.MonsterHPmax = new double[StaticValue.MONSTER_LENGTH];

            mSaveData.CardID = new int[StaticValue.CARD_ID_LENGTH];
        }

        FixSaveDataForUpdate();
    }

    private void FixSaveDataForUpdate()
    {
        if(mSaveData.PlayerLevel == null)
        {
            mSaveData.PlayerLevel = new int[StaticValue.PLAYER_LEVEL_LENGTH];
        }
        else if(mSaveData.PlayerLevel.Length < StaticValue.PLAYER_LEVEL_LENGTH)
        {
            int[] temp = new int[StaticValue.PLAYER_LEVEL_LENGTH];
            for(int i = 0; i < mSaveData.PlayerLevel.Length; i++)
            {
                temp[i] = mSaveData.PlayerLevel[i];
            }

            mSaveData.PlayerLevel = temp;
        }

        if (mSaveData.MonsterAttack == null)
        {
            mSaveData.MonsterAttack = new double[StaticValue.MONSTER_LENGTH];
        }
        else if (mSaveData.MonsterAttack.Length < StaticValue.MONSTER_LENGTH)
        {
            double[] temp = new double[StaticValue.MONSTER_LENGTH];
            for (int i = 0; i < mSaveData.MonsterAttack.Length; i++)
            {
                temp[i] = mSaveData.MonsterAttack[i];
            }

            mSaveData.MonsterAttack = temp;
        }

        if (mSaveData.MonsterDefend == null)
        {
            mSaveData.MonsterDefend = new double[StaticValue.MONSTER_LENGTH];
        }
        else if (mSaveData.MonsterDefend.Length < StaticValue.MONSTER_LENGTH)
        {
            double[] temp = new double[StaticValue.MONSTER_LENGTH];
            for (int i = 0; i < mSaveData.MonsterDefend.Length; i++)
            {
                temp[i] = mSaveData.MonsterDefend[i];
            }

            mSaveData.MonsterDefend = temp;
        }

        if (mSaveData.MonsterHPmax == null)
        {
            mSaveData.MonsterHPmax = new double[StaticValue.MONSTER_LENGTH];
        }
        else if (mSaveData.MonsterHPmax.Length < StaticValue.MONSTER_LENGTH)
        {
            double[] temp = new double[StaticValue.MONSTER_LENGTH];
            for (int i = 0; i < mSaveData.MonsterHPmax.Length; i++)
            {
                temp[i] = mSaveData.MonsterHPmax[i];
            }

            mSaveData.MonsterHPmax = temp;
        }

        if (mSaveData.CardID == null)
        {
            mSaveData.CardID = new int[StaticValue.CARD_ID_LENGTH];
        }
        else if (mSaveData.CardID.Length < StaticValue.CARD_ID_LENGTH)
        {
            int[] temp = new int[StaticValue.CARD_ID_LENGTH];
            for (int i = 0; i < mSaveData.CardID.Length; i++)
            {
                temp[i] = mSaveData.CardID[i];
            }

            mSaveData.CardID = temp;
        }
    }

    private void OnApplicationQuit()
    {
        //Save();
    }
}