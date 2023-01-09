using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //인스펙터에서 함수를 바인딩할 때 사용.

public class AnimEvent : MonoBehaviour
{
    public UnityEvent Attack = default;
    public UnityEvent Skill= default;
    public UnityEvent<bool> ComboCheck = default; //제네릭 타입 딜리게이트
    public UnityEvent Weaponset = default;
    public Transform ESkillAttackPoint;
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
        Attack?.Invoke(); //바인딩 된 함수가 있을경우 실행.
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
        Instantiate(ESkillEffect, ESkillAttackPoint.position, Quaternion.identity);
    }
}
