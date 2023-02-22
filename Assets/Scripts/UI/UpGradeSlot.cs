using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpGradeSlot : Slot, IPointerClickHandler
{
    public MainSlot MainSlot;
    public Image UpgradeItemImage; //업그레이드할 아이템 이미지
    public Image UpImage; //아이템 이미지와 동일하게 될 이미지.
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if(MainSlot.item != null)
        {
            MainSlot.AddItem(item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
