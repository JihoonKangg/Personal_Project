using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Wizard : BattleSystem
{
    public Transform myAttackPos;
    //콤보체크 담당
    bool IsCombable = false;
    int ClickCount = 0;

    void Start()
    {

    }

    void Update()
    {
        WizardMove();
        WarriorAttack();
        AutoAim();
        if (IsCombable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
        }

        if (myStat.HP <= 0) //플레이어가 죽었을 때
        {
            foreach (IBattle ib in myAttackers)
            {
                ib.DeadMessage(transform);
            }
        }

        if (GetComponentInChildren<AIPerception>().myTarget != null)
        {
            myTarget = GetComponentInChildren<AIPerception>().myTarget;
        }
    }

    public void WizardMove()
    {
        PlayerMoving();
        if(myAnim.GetBool("IsSkillAttacking") || myAnim.GetBool("IsComboAttacking") || myAnim.GetBool("IsComboAttacking1"))
        {
            myAnim.SetFloat("Speed", 0.0f);
        }
        transform.parent.Translate(transform.forward * myAnim.GetFloat("Speed") * 20.0f * Time.deltaTime);
    }

    public void AutoAim()
    {
        if (myTarget == null) return;
        if (myAnim.GetBool("IsComboAttacking") || myAnim.GetBool("IsComboAttacking1")
            || myAnim.GetBool("IsSkillAttacking") && !myAnim.GetBool("IsDamage"))
        {
            Vector3 pos = myTarget.position - transform.position;
            pos.Normalize();
            float delta = myStat.RotSpeed * Time.deltaTime;
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

    //AI Perception
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
    public override void AttackTarget(float radius, int a = 0, int b = 0) //a = AttackPoint , b = kind of damage
    {
        base.AttackTarget(radius, a, b);
    }
    public void BaseAttack()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/ForestBatAttackObject01"), myAttackPos) as GameObject;
    }
    public void ESkillAttack()
    {
        AttackTarget(5.0f, 1, 2);
    }


    //인터페이스
    public override void OnBigDamage(float Bigdmg) //강한데미지 받을 때
    {
        myStat.HP -= Bigdmg;

        if (Mathf.Approximately(myStat.HP, 0)) //죽었을 때
        {
            //Death 트리거 발동
            myAnim.SetTrigger("Die");

        }
        else
        {
            if (!myAnim.GetBool("IsStun") && !myAnim.GetBool("IsSkillAttacking"))
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
            if (!myAnim.GetBool("IsStun") && !myAnim.GetBool("IsSkillAttacking"))
            {
                myAnim.SetTrigger("Damage");
            }
        }
    }
    public override void OnESkillDamage(float ESkillDamage) //스킬데미지 받을 때
    {
        //보스가 스킬공격을 하는 경우.
    }
    public override bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f); //살아있음 , false면 죽었음.
    }
    public override void DeadMessage(Transform tr)
    //몬스터가 죽었을 때 호출되도록.
    {
        if (tr == myTarget)
        {
            StopAllCoroutines();
        }
    }
}
