using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //�ν����Ϳ��� �Լ��� ���ε��� �� ���.

public class AnimEvent : MonoBehaviour
{
    public UnityEvent Attack = default;
    public UnityEvent ESkill= default;
    public UnityEvent<bool> ComboCheck = default; //���׸� Ÿ�� ��������Ʈ
    public UnityEvent Weaponset = default;
   

    public void LeftFootEvent()
    {

    }
    public void RightFootEvent()
    {

    }
    public void OnESkill()
    {
        ESkill?.Invoke();
    }
    public void OnAttack()
    {
        Attack?.Invoke(); //���ε� �� �Լ��� ������� ����.
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
}
