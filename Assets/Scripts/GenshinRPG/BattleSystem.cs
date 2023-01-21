using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle //다중상속 불가능으로 인터페이스 사용
{
    //추상클래스
    void OnBigDamage(float Bigdmg);
    void OnESkillDamage(float ESkillDamage);
    void OnQSkillDamage(float QSkillDamage);
    void OnDamage(float dmg);
    bool IsLive();
    void AddAttacker(IBattle ib);
    void RemoveAttacker(IBattle ib);
    void DeadMessage(Transform tr); //죽었을 때 알려주는 함수
}

public class BattleSystem : CharacterMovement, IBattle //전투에 관련된 스크립트(몬스터/플레이어)
{
    protected List<IBattle> myAttackers = new List<IBattle>(); //플레이어/몬스터 공격하는 오브젝트
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

    //차이들은 딜리게이트로 할 예정.
    public virtual void OnDamage(float dmg) //데미지 입을 때
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
