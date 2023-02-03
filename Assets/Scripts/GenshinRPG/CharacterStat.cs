using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] //����ȭ
public class CharacterStat //����ü
{
    [SerializeField] float hp;  //ü��
    [SerializeField] float maxHp; //�ִ�ü��
    [SerializeField] float ap;  //���ݷ�
    [SerializeField] float ESkillap;  //E��ų���ݷ�
    [SerializeField] float QSkillap;  //Q��ų���ݷ�
    [SerializeField] float walkSpeed;  //�ȱ�ӵ�
    [SerializeField] float runSpeed;  //�ٱ�ӵ�
    [SerializeField] float rotSpeed;  //ȸ���ӵ�
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;  //����
    [SerializeField] float attackRadius;  //���ݹ���
    [SerializeField] float ESkillcooltime; //E��ų ��Ÿ��

    public float HP
    {
        get => hp;
        set //hp�� �ٲٱ� ���� ����
        {
            hp = Mathf.Clamp(value, 0.0f, maxHp);
        }
    }
    public float MaxHP
    {
        get => maxHp;
        set => maxHp = value;
    }
    public float AP
    {
        get => ap;
    }
    public float ESkillAP
    {
        get => ESkillap;
    }
    public float QSkillAP
    {
        get => QSkillap;
    }
    public float WalkSpeed
    {
        get => walkSpeed;
    }
    public float RunSpeed
    {
        get => runSpeed;
    }
    public float RotSpeed
    {
        get => rotSpeed;
    }
    public float AttackRange
    {
        get => attackRange;
    }
    public float AttackDelay
    {
        get => attackDelay;
    }
    public float AttackRadius
    {
        get => attackRadius;
    }
    public float ESkillCoolTime
    {
        get => ESkillcooltime;
    }
}
