using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float mATK, mDEF, mHEAL;

    protected UIController mUIController;

    private Vector3 mOffSet;

    private float mZcoord;

    public Vector3 mCurrentPos;

    private void Start()
    {
        mUIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        mCurrentPos = transform.position;
    }

    private void Update()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void OnMouseDown()
    {
        mUIController.ShowIcon(0);

        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffSet = gameObject.transform.position - GetMouseWorldPos();

        
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZcoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffSet;
    }

    private void OnMouseUp()
    {
        
    }
}
