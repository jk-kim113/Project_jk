using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipItemDragObj : MonoBehaviour, IDragHandler, IBeginDragHandler
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIcon;
#pragma warning restore

    private const int mObjZPos = 25;

    

    public void Init(Sprite icon)
    {
        mIcon.sprite = icon;
    }

    public void DragBegin(Sprite icon)
    {
        mIcon.sprite = icon;

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * mObjZPos);

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("During");
        eventData.pointerEnter = gameObject;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * mObjZPos);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        eventData.pointerEnter = gameObject;
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * mObjZPos);
    }
}
