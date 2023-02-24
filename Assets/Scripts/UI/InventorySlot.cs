using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : Slot
{
    public int itemCount; //ȹ���� ������ ����

    [SerializeField]
    private TMP_Text text_Count;


    //������ ȹ��.
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

    //������ ���� ����.
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    //���� �ʱ�ȭ.
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        text_Count.gameObject.SetActive(false);
    }

    //�̹����� ���� ����.
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
