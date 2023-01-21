using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle //���߻�� �Ұ������� �������̽� ���
{
    //�߻�Ŭ����
    void OnBigDamage(float Bigdmg);
    void OnESkillDamage(float ESkillDamage);
    void OnQSkillDamage(float QSkillDamage);
    void OnDamage(float dmg);
    bool IsLive();
    void AddAttacker(IBattle ib);
    void RemoveAttacker(IBattle ib);
    void DeadMessage(Transform tr); //�׾��� �� �˷��ִ� �Լ�
}

public class BattleSystem : CharacterMovement, IBattle //������ ���õ� ��ũ��Ʈ(����/�÷��̾�)
{
    protected List<IBattle> myAttackers = new List<IBattle>(); //�÷��̾�/���� �����ϴ� ������Ʈ
    Transform _target = null;
    protected Transform myTarget
    {
        get => _target;
        set
        {
            _target = value;
            if (_target != null) _target.GetComponent<IBattle>()?.AddAttacker(this);
        }
    }

    //���̵��� ��������Ʈ�� �� ����.
    public virtual void OnDamage(float dmg) //������ ���� ��
    {

    }
    public virtual void OnBigDamage(float Bigdmg)
    {

    }
    public virtual void OnESkillDamage(float ESkilldmg)
    {

    }
    public virtual void OnQSkillDamage(float QSkilldmg)
    {

    }
    public virtual bool IsLive()
    {
        return true;
    }

    public virtual void AddAttacker(IBattle ib)
    {
        myAttackers.Add(ib);
    }

    public virtual void DeadMessage(Transform tr)
    {

    }
    public virtual void RemoveAttacker(IBattle ib)
    {
        for (int i = 0; i < myAttackers.Count;)
        {
            if (ib == myAttackers[i])
            {
                myAttackers.RemoveAt(i);
                break;
            }
            ++i;
        }
    }
}
