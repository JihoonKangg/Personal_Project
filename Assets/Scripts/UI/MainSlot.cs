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
    private TMP_Text MaxMixCount; //최대 합성 가능한 갯수
    [SerializeField]
    private int HaveNum = 0;
    [SerializeField]
    private int NeedNum = 0;
    [SerializeField]
    private int MaxMixNum = 0;
    [SerializeField]
    GameObject CheckBox_icon;
    [SerializeField]
    GameObject CheckBox_BG;
    [SerializeField]
    private Slider MixSlider;
    [SerializeField]
    GameObject[] PlusMinusIcon;
    [SerializeField]
    private int Count = 0;
    private int UseCount = 0;

    private void Start()
    {
        itemImage.sprite = item.itemImage;
        NeedItemImg.sprite = item.upgradeItemImage;
    }
    private void Update()
    {
        NeedNum = item.MakeItemCount;
        NeedCount.text = NeedNum.ToString();
        if (HaveNum >= NeedNum)
        {
            CheckBox_icon.SetActive(true);
            CheckBox_BG.SetActive(false);
            //MixSlider.value = 1;
        }
        else
        {
            CheckBox_icon.SetActive(false);
            CheckBox_BG.SetActive(true);
        }
        Check();
        CanMixItemCount();
    }

    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        NeedItemImg.sprite = _item.upgradeItemImage;
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
        if (HaveNum == 0) return;
        
        MaxMixNum = HaveNum / NeedNum;
        MixSlider.maxValue = MaxMixNum;
        MaxMixCount.text = MixSlider.value.ToString();
        if (MixSlider.value == 0) PlusMinusIcon[1].SetActive(true);
        else
        {
            PlusMinusIcon[1].SetActive(false);
        }
        if (CheckBox_BG.activeSelf || MixSlider.value == MixSlider.maxValue) PlusMinusIcon[0].SetActive(true);
        else PlusMinusIcon[0].SetActive(false);
    }



    public void Click_Mix()
    {
        Count = (int)Mathf.Round(MixSlider.value);
        inven.AcquireItem(item, Count);
        for (int i = 0; i < inven.slots.Length; i++)
        {
            if (inven.slots[i].item != null)
            {
                if (item.UpgradeItemCode == inven.slots[i].item.itemCode)
                {
                    UseCount = Count * NeedNum;
                    inven.slots[i].Useitem(item, UseCount);
                    return;
                }
                else
                {
                    HaveNum = 0;
                    HaveCount.text = HaveNum.ToString();
                }
            }
        }
    }

    public void Click_Plus()
    {
        MixSlider.value++;
    }
    public void Click_Minus()
    {
        MixSlider.value--;
    }
}
