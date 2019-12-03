using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private GameObject mPlayerInfoObj;
    private Text[] mPlayerInfoText;

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

        mPlayerInfoText = mPlayerInfoObj.GetComponentsInChildren<Text>();
    }

    public void ShowPlayerInfo(string state, float atk, float def, float heal)
    {
        mPlayerInfoObj.SetActive(true);

        mPlayerInfoText[0].text = state;
        mPlayerInfoText[1].text = "ATK ; " + atk.ToString();
        mPlayerInfoText[2].text = "DEF : " + def.ToString();
        mPlayerInfoText[3].text = "HEAL : " + heal.ToString();
    }

    public void OffPlayerInfo()
    {
        mPlayerInfoObj.SetActive(false);
    }
}
