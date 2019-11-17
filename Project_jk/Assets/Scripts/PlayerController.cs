using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    protected UIController mUIController;

    [SerializeField]
    private LayerMask mPlayerLayer;

    private void Start()
    {
        mUIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, mPlayerLayer))
            {
                mUIController.MoveToBattle(Input.mousePosition);
            }
        }
    }
}
