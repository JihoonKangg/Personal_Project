using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] //직렬화
public class CharacterStat //구조체
{
    [SerializeField] float hp;  //체력
    [SerializeField] float maxHp; //최대체력
    [SerializeField] float ap;  //공격력
    [SerializeField] float Skillap;  //스킬공격력
    [SerializeField] float walkSpeed;  //걷기속도
    [SerializeField] float runSpeed;  //뛰기속도
    [SerializeField] float rotSpeed;  //회전속도
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;  //공속

    public float HP
    {
        get => hp;
        set //hp를 바꾸기 위한 세팅
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
