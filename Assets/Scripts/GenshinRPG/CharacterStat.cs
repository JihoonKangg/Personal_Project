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
    [SerializeField] float Skillap;  //��ų���ݷ�
    [SerializeField] float walkSpeed;  //�ȱ�ӵ�
    [SerializeField] float runSpeed;  //�ٱ�ӵ�
    [SerializeField] float rotSpeed;  //ȸ���ӵ�
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;  //����

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
    }
    public float AP
    {
        get => ap;
    }
    public float SkillAP
    {
        get => Skillap;
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

}
