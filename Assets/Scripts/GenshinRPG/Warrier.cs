using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrier : BattleSystem
{
    [SerializeField] Transform myAttackPoint;
    [SerializeField] Transform myESkillAttackPoint;
    [SerializeField] LayerMask myEnemyMask;

    //콤보체크 담당
    bool IsCombable = false;
    int ClickCount = 0;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerMoving();
        WarriorAttack();

        if(IsCombable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
        }
    }

    

    //옵저버 패턴 사용(비동기 방식)
    public void BaseAttack() //기본공격
    {
        Collider[] list = Physics.OverlapSphere(myAttackPoint.position, 1.0f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(myStat.AP); //데미지 30
        }
    }

    public void ESkillAttack() //E 스킬 공격
    {
        //스킬 미정.
        Collider[] list = Physics.OverlapSphere(myESkillAttackPoint.position, 2.8f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnSkillDamage(myStat.SkillAP); //데미지 50
        }
    }

    //일반공격 연속기 담당
    public void ComboCheck(bool v)
    {
        if (v)
        {
            //Start Combo Check
            IsCombable = true;
            ClickCount = 0;
        }
        else
        {
            //End Combo Check
            IsCombable = false;
            if (ClickCount == 0)
            {
                myAnim.SetTrigger("ComboFail");
            }
        }
    }


    //인터페이스
    public override void OnBigDamage(float Bigdmg) //강한데미지 받을 때
    {
        myStat.HP -= Bigdmg;

        if (Mathf.Approximately(myStat.HP, 0)) //죽었을 때
        {
            //Death 트리거 발동
            myAnim.SetTrigger("Die");
            //죽어도 몬스터가 계속 때림
        }
        else
        {
            if (!myAnim.GetBool("IsStun"))
            {
                myAnim.SetTrigger("Big Damage");
            }
        }
    }
    public override void OnDamage(float dmg) //일반 데미지 받을 때
    {
        myStat.HP -= dmg;

        if (Mathf.Approximately(myStat.HP, 0)) //죽었을 때
        {
            //Death 트리거 발동
            myAnim.SetTrigger("Die");
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }
    public override void OnSkillDamage(float SkillDamage) //스킬데미지 받을 때
    {
        //보스가 스킬공격을 하는 경우.
    }
    public override bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f); //살아있음
    }
    public override void DeadMessage(Transform tr)
    {
        if (tr == myTarget)
        {
            StopAllCoroutines();
        }
    }
}
