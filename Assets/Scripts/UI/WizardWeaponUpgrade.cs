using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWeaponUpgrade : WeaponUpgrade
{
    public Wizard wizard;

    void Start()
    {
        WeaponStat();
    }

    void Update()
    {
        if (wizard.W_LEVEL == 10)
        {
            NextLevel.text = "Lv. MAX";
        }
    }

    public void WeaponLevelUpgrade()
    {
        if (wizard.W_LEVEL == 10)
        {
            Debug.Log("∏∏∑æ¿‘¥œ¥Ÿ");
            return;
        }

        wizard.WeaponLevelUP();
        WeaponStat();
    }

    public void WeaponStat()
    {
        Weapon_AP.text = (wizard.orgData.AP[wizard.LEVEL - 1] * wizard.orgWeaponData.WeaponAP[wizard.W_LEVEL - 1]).ToString();
        Weapon_Cri_P.text = wizard.orgWeaponData.CriticalPercent[wizard.W_LEVEL - 1].ToString() + " %";
        Weapon_Cri_AP.text = "°ø " + wizard.orgWeaponData.CriticalAP[wizard.W_LEVEL - 1].ToString();
        NowLevel.text = "Lv. " + wizard.W_LEVEL.ToString();
        NextLevel.text = "Lv. " + (wizard.W_LEVEL + 1).ToString();
    }
}
