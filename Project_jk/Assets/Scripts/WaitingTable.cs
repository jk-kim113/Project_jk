using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingTable : MonoBehaviour
{
    private bool IsWaiting;

    private void Awake()
    {
        IsWaiting = true;
    }

    public bool WaitingPossible()
    {
        return IsWaiting;
    }

    public void ChangeWaiting()
    {
        IsWaiting = !IsWaiting;
    }

    private void OnMouseUp()
    {
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
