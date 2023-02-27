using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUpgrade : UIchecker
{
    [SerializeField]
    GameObject[] Player;
    [SerializeField]
    GameObject[] UpgradeButton;
    [SerializeField]
    protected Item item;

    [SerializeField]
    protected TMP_Text NowLevel;
    [SerializeField]
    protected TMP_Text NextLevel;
    [SerializeField]
    protected TMP_Text Weapon_AP;
    [SerializeField]
    protected TMP_Text Weapon_Cri_P;
    [SerializeField]
    protected TMP_Text Weapon_Cri_AP;

    void Start()
    {
        ChooseWarrier();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.C))
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
        }
    }

    public void ChooseWarrier()
    {
        Player[0].SetActive(true);
        Player[1].SetActive(false);
        UpgradeButton[0].SetActive(true);
        UpgradeButton[1].SetActive(false);
    }
    public void ChooseWizard()
    {
        Player[0].SetActive(false);
        Player[1].SetActive(true); 
        UpgradeButton[0].SetActive(false);
        UpgradeButton[1].SetActive(true);
    }
}
