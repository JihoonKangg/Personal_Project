using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSlot : Slot
{
    [SerializeField]
    private Image NeedItemImg;
    [SerializeField]
    Inventory inven;
    [SerializeField]
    private TMP_Text HaveCount;
    [SerializeField]
    private TMP_Text NeedCount;
    [SerializeField]
    private int HaveNum = 0;
    [SerializeField]
    private int NeedNum = 0;
    [SerializeField]
    GameObject CheckBox_icon;
    [SerializeField]
    GameObject CheckBox_BG;

    private void Start()
    {
        itemImage.sprite = item.itemImage;
        NeedItemImg.sprite = item.upgradeItemImage;
    }
    private void Update()
    {
        if(HaveNum >= NeedNum)
        {
            CheckBox_icon.SetActive(true);
            CheckBox_BG.SetActive(false);
        }
        else
        {
            CheckBox_icon.SetActive(false);
            CheckBox_BG.SetActive(true);
        }
    }

    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        NeedItemImg.sprite = _item.upgradeItemImage;
        NeedNum = _item.MakeItemCount;
        NeedCount.text = NeedNum.ToString();
        Check();
    }

    private int Check()
    {
        for (int i = 0; i < inven.slots.Length; i++)
        {
            if (inven.slots[i].item != null)
            {
                if (item.UpgradeItemCode == inven.slots[i].item.itemCode)
                {
                    HaveNum = inven.slots[i].itemCount;
                    HaveCount.text = HaveNum.ToString();
                    return HaveNum;
                }
                else
                {
                    HaveNum = 0;
                    HaveCount.text = HaveNum.ToString();
                }
            }
        }
        return 0;
    }

    private void CanMixItemCount()
    {
        int Num = 0;

    }
}
