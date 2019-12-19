using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{   
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<Player>().RaySelected();
            }
            else
            {
                UIController.Instance.OffPlayerInfo();
            }
        }
    }
}
