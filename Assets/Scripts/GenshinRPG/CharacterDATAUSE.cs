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
    public int W_LEVEL; //웨폰레벨
    protected float AP; //AP : 캐릭터 공격력
    protected float ESkillAP;
    protected float QSkillAP;
    protected float ESkillCoolTime;
    protected float W_AP; //무기 공격력
    protected float CriticalAP; //CriticalAP : 무기 공격력
    protected float Critical_P; //CriticalPercent : 크리티컬 확률4
    protected float ChaAP; //캐릭터가 공격하는 최종 공격력
    public float ChaEAP; //캐릭터가 공격하는 최종 스킬
    protected float CurEXP; //캐릭터가 가지고 있는 경험치

    protected void Awake()
    {
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
        ESkillAP = orgData.AP[W_LEVEL - 1] * AP * 1.5f;
        QSkillAP = orgData.AP[W_LEVEL - 1] * AP * 1.5f; //수정요망
        //무기레벨업의 경우 캐릭터와 관련된 스탯이 올라가게 해야함.
    }

    public void CharacterLevelUP()
    {
        HP = orgData.HP[SceneData.Inst.WorldLevel - 1]; //캐릭터 레벨 HP
        AP = orgData.AP[SceneData.Inst.WorldLevel - 1]; //캐릭터 레벨 AP
        curHP = HP;
    }
}
