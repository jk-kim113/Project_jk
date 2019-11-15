using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerController mPlayerController;

    [SerializeField]
    private GameObject[] mPlayerIconArr;

    private void Start()
    {
        mPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ShowIcon(int id)
    {
        mPlayerIconArr[id].SetActive(true);
        mPlayerIconArr[id].transform.position = Input.mousePosition;
    }
}
