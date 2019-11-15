using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    protected UIController mUIController;

    private void Start()
    {
        mUIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        mUIController.ShowIcon(0);
    }
    private void OnMouseUp()
    {
        
    }
}
