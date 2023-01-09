using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Warrier : BattleSystem
{
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
        AutoAim();
        if (IsCombable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
        }
    }
    
    public void AutoAim()
    {
        if (myTarget == null) return;
        if (myTarget.GetComponent<IBattle>().IsLive())
        {
            if (myAnim.GetBool("IsComboAttacking"))
            {
                Vector3 pos = myTarget.position - transform.position;
                pos.Normalize();
                float delta = 360.0f * Time.deltaTime;
                float rotDir = 1.0f;
                if (Vector3.Dot(transform.right, pos) < 0)
                {
                    rotDir = -rotDir;
                }
                float angle = Vector3.Angle(transform.forward, pos);
                if (angle > 0)
                {
                    if (delta > angle)
                    {
                        delta = angle;
                    }
                    angle -= delta;
                    transform.Rotate(Vector3.up * rotDir * delta, Space.World);
                }
            }
        }
        else myTarget = null;
    }

    public void FindTarget(Transform target)
    {
        myTarget = target;
    }

    public void LostTarget()
    {
        myTarget = null;
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
    public override void AttackTarget(float radius, int a = 0, int b = 0)
    {
        if(myAnim.GetBool(""))//E스킬 사용할 때
        {
            a = 1;
            b = 2;
        }
        base.AttackTarget(radius, a, b);
    }
    public void Attacktarget()
    {
        AttackTarget(myStat.AttackRadius);
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
