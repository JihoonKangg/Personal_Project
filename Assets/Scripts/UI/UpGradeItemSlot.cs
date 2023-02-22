using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UpGradeItemSlot : Slot, IPointerClickHandler
{
    public MainSlot mainSlot;
    [SerializeField]
    private TMP_Text NeedCount;
    [SerializeField]
    private TMP_Text HaveCount;
    [SerializeField]
    Inventory inven;
    [SerializeField]
    private int NeedNum = 0;
    [SerializeField]
    private int HaveNum = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        /*if(eventData.button == PointerEventData.InputButton.Right)
        {
            inven.AcquireItem(item);
        }*/
    }

    private void Start()
    {
        
    }
    void Update()
    {
        //item = mainSlot.item;
        //itemImage.sprite = item.upgradeItemImage;
        //NeedNum = item.MakeItemCount;
        //NeedCount.text = NeedNum.ToString();
       
        //if(Input.GetKeyDown(KeyCode.Y))
        //Check();
    }

    private void Check()
    {
        for(int i = 0; i < inven.slots.Length; i++)
        {
            if (inven.slots[i].item != null)
            {
                if (item.UpgradeItemCode == inven.slots[i].item.itemCode)
                {
                    HaveNum += inven.slots[i].itemCount;
                    HaveCount.text = HaveNum.ToString();
                }
                else
                {
                    HaveNum = 0;
                    HaveCount.text = HaveNum.ToString();
                }
            }
        }
    }
}
