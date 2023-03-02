using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterDATAUSE : BattleSystem
{
    public CharacterData orgData;
    public WeaponLevelData orgWeaponData;

    public float HP;
    public float curHP;
    public int W_LEVEL; //��������
    protected float AP; //AP : ĳ���� ���ݷ�
    protected float ESkillAP;
    protected float QSkillAP;
    protected float ESkillCoolTime;
    protected float W_AP; //���� ���ݷ�
    protected float CriticalAP; //CriticalAP : ���� ���ݷ�
    protected float Critical_P; //CriticalPercent : ũ��Ƽ�� Ȯ��4
    protected float ChaAP; //ĳ���Ͱ� �����ϴ� ���� ���ݷ�
    public float ChaEAP; //ĳ���Ͱ� �����ϴ� ���� ��ų
    protected float CurEXP; //ĳ���Ͱ� ������ �ִ� ����ġ

    protected void Awake()
    {
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
        ESkillAP = orgData.AP[W_LEVEL - 1] * AP * 1.5f;
        QSkillAP = orgData.AP[W_LEVEL - 1] * AP * 1.5f; //�������
        //���ⷹ������ ��� ĳ���Ϳ� ���õ� ������ �ö󰡰� �ؾ���.
    }

    public void CharacterLevelUP()
    {
        HP = orgData.HP[SceneData.Inst.WorldLevel - 1]; //ĳ���� ���� HP
        AP = orgData.AP[SceneData.Inst.WorldLevel - 1]; //ĳ���� ���� AP
        curHP = HP;
    }
}
