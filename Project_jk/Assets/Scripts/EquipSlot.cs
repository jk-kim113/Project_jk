using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IDragHandler
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIcon;
#pragma warning restore

    

    public void Init(Sprite icon)
    {
        mIcon.sprite = icon;

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void OnMouseDown()
    {   
        mIcon.sprite = null;
    }
}
