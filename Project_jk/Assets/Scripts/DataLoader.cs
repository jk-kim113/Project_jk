using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    protected string[] LoadCsvData(string dataPath)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(dataPath);

        if (textAsset != null)
        {
            string data = textAsset.text;

            if(!string.IsNullOrEmpty(data))
            {
                string[] output = data.Split('\n');
                return output;
            }
            else
            {
                Debug.LogWarning("File is empty : " + dataPath);
            }
        }
        else
        {
            Debug.LogWarning("Wrong location or file name : " + dataPath);
        }

        return null;
    }
}
