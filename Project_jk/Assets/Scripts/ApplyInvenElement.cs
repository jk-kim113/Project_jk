using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyInvenElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text mNameText, mHPText, mAtkText, mDefText, mHealText;
    [SerializeField]
    private Button mApplyButton;
#pragma warning restore

    public void Init(int id, string name, double hpcurrent, double hpmax, double atk, double def, double heal, StaticValue.OneIntParameter callback)
    {
        mNameText.text = name;
        mHPText.text = "HP : " + string.Format("{0}/{1}", UnitBuilder.GetUnitStr(hpcurrent), UnitBuilder.GetUnitStr(hpmax));
        mAtkText.text = "Atk : " + UnitBuilder.GetUnitStr(atk);
        mDefText.text = "Def : " + UnitBuilder.GetUnitStr(def);
        mHealText.text = "Heal : " + UnitBuilder.GetUnitStr(heal);

        mApplyButton.onClick.AddListener(() => { callback(id); });
    }

    public void Renew(double hpcurrent, double hpmax)
    {
        mHPText.text = "HP : " + string.Format("{0}/{1}", UnitBuilder.GetUnitStr(hpcurrent), UnitBuilder.GetUnitStr(hpmax));
    }
}
