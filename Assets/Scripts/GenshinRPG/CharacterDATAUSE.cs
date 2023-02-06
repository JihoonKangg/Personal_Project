using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDATAUSE : BattleSystem
{
    public CharacterData orgData;

    public float HP;
    protected float AP;
    protected float ESkillAP;
    protected float QSkillAP;
    protected float RotSpeed;
    protected float AttackRadius;
    protected float ESkillCoolTime;
    public float curHP;
    public int LEVEL = 1;

    protected void Awake()
    {
        HP = orgData.HP;
        AP = orgData.AP;
        ESkillAP = orgData.ESkillAP;
        QSkillAP = orgData.QSkillAP;
        RotSpeed = orgData.RotSpeed;
        AttackRadius = orgData.AttackRadius;
        ESkillCoolTime = orgData.ESkillCoolTime;

        curHP = HP;
    }

    
}
