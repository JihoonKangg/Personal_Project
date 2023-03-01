using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarrierWeaponUpgrade : WeaponUpgrade
{
    public Warrier warrier;

    void Start()
    {
        WeaponStat();
    }

    void Update()
    {
        if(warrier.W_LEVEL == 10)
        {
            NextLevel.text = "Lv. MAX";
        }
    }

    public void WeaponLevelUpgrade()
    {
        if (warrier.W_LEVEL == 10)
        {
            Debug.Log("∏∏∑æ¿‘¥œ¥Ÿ");
            return;
        }

        warrier.WeaponLevelUP();
        WeaponStat();
    }

    public void WeaponStat()
    {
        Weapon_AP.text = (warrier.orgData.AP[SceneData.Inst.WorldLevel - 1] * warrier.orgWeaponData.WeaponAP[warrier.W_LEVEL - 1]).ToString();
        Weapon_Cri_P.text = warrier.orgWeaponData.CriticalPercent[warrier.W_LEVEL - 1].ToString() + " %";
        Weapon_Cri_AP.text = "°ø " + warrier.orgWeaponData.CriticalAP[warrier.W_LEVEL - 1].ToString();
        NowLevel.text = "Lv. " + warrier.W_LEVEL.ToString();
        NextLevel.text = "Lv. " + (warrier.W_LEVEL + 1).ToString();
    }
}
