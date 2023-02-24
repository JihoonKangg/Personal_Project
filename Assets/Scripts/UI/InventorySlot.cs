using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : Slot
{
    public int itemCount; //획득한 아이템 개수

    [SerializeField]
    private TMP_Text text_Count;


    //아이템 획득.
    public void Additem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = _item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            text_Count.gameObject.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = " ";
            text_Count.gameObject.SetActive(false);
        }

        SetColor(1);
    }
    public void Useitem(Item _item, int _count)
    {
        item = _item;
        itemCount -= _count;
        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    //아이템 개수 조정.
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    //슬롯 초기화.
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        text_Count.gameObject.SetActive(false);
    }

    //이미지의 투명도 조절.
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
