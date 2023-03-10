using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : UIchecker
{
    //슬롯들
    public InventorySlot[] slots;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField] UpgradeWeaponMainSlot U_W_MainSlot;
    [SerializeField] MainSlot M_MainSlot;

    public InventorySlot[] GetInvenSlots() { return slots; } //세이브 데이터에 넣기위한 반환값

    [SerializeField] private Item[] items;
    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++) //아이템의 갯수만큼 포문을 돌림
            if (items[i].itemName == _itemName) //아이템 이름과 일치할 경우
                slots[_arrayNum].Additem(items[i], _itemNum); //아이템s의 i번째에 아이템 갯수를 넣는 과정
    }

    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIActivated = !UIActivated;
            if (UIActivated)
            {
                OpenUI();
            }
            else
            {
                CloseUI();
            }
            SceneData.Inst.OnUI = UIActivated;
            SceneData.Inst.UIOn();
        }
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].Additem(_item, _count);
                return;
            }
        }
    }

    public void UpgradeButtonClick()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return;
            }
            if (U_W_MainSlot.Itemcode == slots[i].item.itemCode)
            {
                slots[i].Useitem(slots[i].item, U_W_MainSlot.NeedNum);
            }
        }
        U_W_MainSlot.WarrierUIChoose();
        U_W_MainSlot.WizardUIChoose();
    }

    public void Click_Mix()
    {
        int Count = (int)M_MainSlot.MixSlider.value;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return;
            }
            if (M_MainSlot.item.UpgradeItemCode == slots[i].item.itemCode)
            {
                slots[i].Useitem(slots[i].item, M_MainSlot.NeedNum * Count);
                AcquireItem(M_MainSlot.item, Count);
            }
        }
        M_MainSlot.Check();
    }
}
