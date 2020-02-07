using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInvenElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mItemImg;
    [SerializeField]
    private Text mNameText, mNumberText, mInfoText;
    [SerializeField]
    private Button mApplyButton;
#pragma warning restore

    public void Init(Sprite img, int id, string name, int number, string info, double value, StaticValue.OneIntParameter callback)
    {
        mItemImg.sprite = img;

        mNameText.text = name;
        mNumberText.text = "남은 수량 : " + number.ToString();
        mInfoText.text = string.Format(info, value);

        mApplyButton.onClick.AddListener(() => callback(id));
    }

    public void Renew(int number)
    {
        mNumberText.text = "남은 수량 : " + number.ToString();
    }
}
