using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeWeaponMainSlot : Slot
{
    [SerializeField]
    private TMP_Text HaveCount;
    [SerializeField]
    private TMP_Text NeedCount;
    [SerializeField]
    private int HaveNum = 0;
    public int NeedNum = 0;
    [SerializeField]
    private GameObject PlayerUI;
    [SerializeField] WarrierWeaponUpgrade Warrier;
    [SerializeField] WizardWeaponUpgrade Wizard;
    private int W_Level;
    public int Itemcode;
    [SerializeField]
    private Inventory inven;
    [SerializeField]
    private GameObject[] ButtonUI;


    public void WarrierUIChoose()
    {
        W_Level = Warrier.warrier.W_LEVEL;
        item = Warrier.item;
        NeedNum = Warrier.warrier.orgWeaponData.LvupNum[W_Level - 1];
        ChangeUI();
    }

    public void WizardUIChoose()
    {
        W_Level = Wizard.wizard.W_LEVEL;
        item = Wizard.item;
        NeedNum = Wizard.wizard.orgWeaponData.LvupNum[W_Level - 1];
        ChangeUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        WarrierUIChoose();
    }

    private void Update()
    {
        if (HaveNum < NeedNum)
        {
            ButtonUI[0].SetActive(true);
            ButtonUI[1].SetActive(false);
        }
        else
        {
            ButtonUI[0].SetActive(false);
            ButtonUI[1].SetActive(true);
        }
    }

    private void ChangeUI()
    {
        NeedCount.text = NeedNum.ToString();
        if (W_Level < 5)
        {
            itemImage.sprite = item.upgradeItemImage;
            Itemcode = item.UpgradeItemCode;
            Check();
        }
        else
        {
            itemImage.sprite = item.itemImage;
            Itemcode = item.itemCode;
            Check();
        }
    }
    private void Check()
    {
        for (int i = 0; i < inven.slots.Length; i++)
        {
            if (inven.slots[i].item != null)
            {
                if (Itemcode == inven.slots[i].item.itemCode)
                {
                    HaveNum = inven.slots[i].itemCount;
                    HaveCount.text = HaveNum.ToString();
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
}
