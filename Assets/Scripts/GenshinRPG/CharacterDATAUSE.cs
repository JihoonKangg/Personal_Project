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
    protected float AP; //AP : ĳ���� ���ݷ�
    protected float ESkillAP;
    protected float QSkillAP;
    protected float RotSpeed;
    protected float AttackRadius;
    protected float ESkillCoolTime;
    protected float W_AP; //���� ���ݷ�
    protected float CriticalAP; //CriticalAP : ���� ���ݷ�
    protected float Critical_P; //CriticalPercent : ũ��Ƽ�� Ȯ��4
    protected float ChaAP; //ĳ���Ͱ� �����ϴ� ���� ���ݷ�
    protected float CurEXP; //ĳ���Ͱ� ������ �ִ� ����ġ

    protected void Awake()
    {
        LEVEL = 0;
        W_LEVEL = 0;
        WeaponLevelUP();
        CharacterLevelUP();
    }

    public void WeaponLevelUP() //���ⷹ����
    {
        W_LEVEL++;
        W_AP = orgWeaponData.WeaponAP[W_LEVEL - 1];
        Critical_P = orgWeaponData.CriticalPercent[W_LEVEL - 1];
        CriticalAP = orgWeaponData.CriticalAP[W_LEVEL - 1];
        ESkillAP = orgData.ESkillAP[W_LEVEL - 1];
        QSkillAP = orgData.QSkillAP[W_LEVEL - 1];
        //���ⷹ������ ��� ĳ���Ϳ� ���õ� ������ �ö󰡰� �ؾ���.
    }

    public void CharacterLevelUP()
    {
        if (LEVEL == 10) return;
        LEVEL++;

        HP = orgData.HP[LEVEL - 1]; //ĳ���� ���� HP
        AP = orgData.AP[LEVEL - 1]; //ĳ���� ���� AP
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
