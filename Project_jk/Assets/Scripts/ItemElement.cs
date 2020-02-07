using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mItemImg;
    [SerializeField]
    private Text mNameText, mCostText, mInfoText;
    [SerializeField]
    private Button mBuyButton;
#pragma warning restore

    public void Init(Sprite img, int id, string name, double cost, string info, double value, StaticValue.OneIntParameter callback)
    {
        mItemImg.sprite = img;

        mNameText.text = name;
        mCostText.text = "Cost : " + cost.ToString();
        mInfoText.text = string.Format(info, value);

        mBuyButton.onClick.AddListener(() => callback(id));
    }
}
