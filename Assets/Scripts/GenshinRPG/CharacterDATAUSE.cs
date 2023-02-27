using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.DocumentationSortingAttribute;

public class CharacterDATAUSE : BattleSystem
{
    public CharacterData orgData;
    public WeaponLevelData orgWeaponData;
    protected ExpSystemData expData;

    public float HP;
    public float curHP;
    public int LEVEL;
    public int W_LEVEL;
    protected float AP; //AP : 캐릭터 공격력
    protected float ESkillAP;
    protected float QSkillAP;
    protected float RotSpeed;
    protected float AttackRadius;
    protected float ESkillCoolTime;
    protected float W_AP; //무기 공격력
    protected float CriticalAP; //CriticalAP : 무기 공격력
    protected float Critical_P; //CriticalPercent : 크리티컬 확률4
    protected float ChaAP; //캐릭터가 공격하는 최종 공격력
    protected float CurEXP; //캐릭터가 가지고 있는 경험치

    protected void Awake()
    {
        LEVEL = 0;
        W_LEVEL = 0;
        WeaponLevelUP();
        CharacterLevelUP();
    }

    public void WeaponLevelUP() //무기레벨업
    {
        W_LEVEL++;
        W_AP = orgWeaponData.WeaponAP[W_LEVEL - 1];
        Critical_P = orgWeaponData.CriticalPercent[W_LEVEL - 1];
        CriticalAP = orgWeaponData.CriticalAP[W_LEVEL - 1];
        ESkillAP = orgData.ESkillAP[W_LEVEL - 1];
        QSkillAP = orgData.QSkillAP[W_LEVEL - 1];
        //무기레벨업의 경우 캐릭터와 관련된 스탯이 올라가게 해야함.
    }

    public void CharacterLevelUP()
    {
        if (LEVEL == 10) return;
        LEVEL++;

        HP = orgData.HP[LEVEL - 1]; //캐릭터 레벨 HP
        AP = orgData.AP[LEVEL - 1]; //캐릭터 레벨 AP
        curHP = HP;
    }

    public void GetEXP(float exp)
    {
        CurEXP -= exp;

        if(CurEXP <= 0)
        {
            CharacterLevelUP();
        }
    }
}
