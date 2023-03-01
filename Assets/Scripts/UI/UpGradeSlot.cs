using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpGradeSlot : Slot
{
    public MainSlot MainSlot;
    
    public void OnClick()
    {
        MainSlot.item = item;
        MainSlot.itemImage.sprite = item.itemImage;
        MainSlot.NeedItemImg.sprite = item.upgradeItemImage;

        MainSlot.Check();
    }
}
