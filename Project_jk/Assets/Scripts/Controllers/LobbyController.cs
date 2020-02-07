using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public void BattleButton()
    {
        SceneManager.LoadScene("Battle");
    }

    public void CardButton()
    {
        SceneManager.LoadScene("Card");
    }

    public void ShopButton()
    {
        SceneManager.LoadScene("Shop");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Title");
    }
}
