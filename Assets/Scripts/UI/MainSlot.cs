using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MainSlot : Slot
{
    public Image NeedItemImg;
    [SerializeField]
    Inventory inven;
    [SerializeField]
    private TMP_Text HaveCount;
    [SerializeField]
    private TMP_Text NeedCount;
    [SerializeField]
    private TMP_Text MixNum; //합성 가능한 갯수
    [SerializeField]
    GameObject CheckBox_icon;
    [SerializeField]
    GameObject CheckBox_BG;
    [SerializeField]
    GameObject[] PlusMinusIcon_BG;
    public Slider MixSlider;
    [SerializeField]
    private int HaveNum = 0;
    public int NeedNum = 0;
    private int MaxMixNum = 0;
    private int Count;
    
    private void Start()
    {
        itemImage.sprite = item.itemImage;
        NeedItemImg.sprite = item.upgradeItemImage;
    }
    void Update()
    {
        CheckBoxActive();
    }

    public void Check()
    {
        MixSlider.value = 0;

        for (int i = 0; i < inven.slots.Length; i++)
        {
            if (inven.slots[i].item != null)
            {
                if (item.UpgradeItemCode == inven.slots[i].item.itemCode)
                {
                    HaveNum = inven.slots[i].itemCount;
                    HaveCount.text = HaveNum.ToString();
                    NeedNum = item.MakeItemCount;
                    NeedCount.text = NeedNum.ToString();
                    MaxMixNum = HaveNum / NeedNum;
                    MixSlider.maxValue = MaxMixNum;
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
        MixSlider.value ++;
    }
    public void Click_Minus()
    {
        MixSlider.value--;
    }

    private void CheckBoxActive()
    {
        Count = (int)MixSlider.value;
        MixNum.text = MixSlider.value.ToString();

        if (Count == 0)
        {
            CheckBox_icon.SetActive(false);
            CheckBox_BG.SetActive(true);
            return;
        }
        if (HaveNum >= NeedNum * Count)
        {
            CheckBox_icon.SetActive(true);
            CheckBox_BG.SetActive(false);
        }
        else
        {
            CheckBox_icon.SetActive(false);
            CheckBox_BG.SetActive(true);
        }

        if (MaxMixNum < 1)
        {
            PlusMinusIcon_BG[0].SetActive(true);
            PlusMinusIcon_BG[1].SetActive(true);
            return;
        }
    }
}
