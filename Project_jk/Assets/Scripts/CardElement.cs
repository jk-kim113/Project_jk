using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text mIDtext, mContentsText;
    [SerializeField]
    private Button mAddOrRemoveButton;
#pragma warning restore

    public void Init(int id, string contents, StaticValue.OneIntParameter callBack)
    {
        mIDtext.text = id.ToString();
        mContentsText.text = contents;

        mAddOrRemoveButton.onClick.AddListener(() => { callBack(id); });
    }
}
