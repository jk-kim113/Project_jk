using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mInvenImg;
    [SerializeField]
    private Text mNameText, mNumberText;
#pragma warning restore

    private int mItemNumber;
    public int ItemNumber { get { return mItemNumber; } }

    private int mID;
    public int ID { get { return mID; } }

    private void Awake()
    {
        mItemNumber = 0;
    }

    public void Init(Sprite img, int id, string name)
    {
        mInvenImg.sprite = img;
        mID = id;
        mNameText.text = name;

        AddNumber();
    }

    public void AddNumber()
    {
        mItemNumber++;
        mNumberText.text = mItemNumber.ToString();
    }
}
