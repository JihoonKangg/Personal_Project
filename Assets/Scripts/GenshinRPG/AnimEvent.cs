using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent Attack = default;
    public UnityEvent Skill= default;
    public UnityEvent<bool> ComboCheck = default; //제네릭 타입 딜리게이트
    public UnityEvent Weaponset = default;
    public Transform PlayerSword;
    public GameObject ESkillEffect;

    public void LeftFootEvent()
    {

    }
    public void RightFootEvent()
    {

    }
    public void OnSkill()
    {
        Skill?.Invoke();
    }
    public void OnAttack()
    {
        Attack?.Invoke();
    }
    public void ComboCheckStart()
    {
        ComboCheck?.Invoke(true);
    }
    public void ComboCheckEnd()
    {
        ComboCheck?.Invoke(false);
    }
    public void WeaponSet()
    {
        Weaponset?.Invoke();
    }

    public void ESkillAttackEffect()
    {
        Instantiate(ESkillEffect, PlayerSword.position, Quaternion.identity);
    }
}
