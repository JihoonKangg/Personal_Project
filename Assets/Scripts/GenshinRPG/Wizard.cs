using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : CharacterMovement
{
    public Transform myAttackPos;
    


    /*private void FixedUpdate()
    {
        curHP = Mathf.Clamp(curHP, 0.0f, HP);

        HpValue = curHP / HP;
        MyHPRightUI.value = HpValue;
    }*/
    void Update()
    {
        if (!SceneData.Inst.NPC_Talking && !IsDead)
        {
            PlayerMoving();
            PlayerAttack();
            WizardMove();
            AutoAim();
        }
        else myAnim.SetFloat("Speed", 0.0f);

        if (IsCombable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
        }

        if (curHP <= 0) //플레이어가 죽었을 때
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
        if (myAnim.GetBool("IsESkillAttacking") || myAnim.GetBool("IsQSkillAttacking") 
            || myAnim.GetBool("IsComboAttacking") || myAnim.GetBool("IsComboAttacking1"))
        {
            myAnim.SetFloat("Speed", 0.0f);
        }
        transform.parent.Translate(transform.forward * myAnim.GetFloat("Speed") * 10.0f * Time.deltaTime);
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

    public override void AttackTarget(float radius, int a = 0, int b = 0) //a = AttackPoint , b = kind of damage
    {
        base.AttackTarget(radius, a, b);
    }

    public void BaseAttack()
    {
        AttackTarget(orgData.AttackRadius, 0, 0);
        GameObject obj = Instantiate(Resources.Load("Prefabs/Wizard/BaseAttackObj"), myAttackPos) as GameObject;
    }

    public void ESkillAttack()
    {
        AttackTarget(15.0f, 1, 1);
    }
    

    //인터페이스
    public override void OnBigDamage(float Bigdmg) //강한데미지 받을 때
    {
        curHP -= Bigdmg;
        Hpupdate();

        if (HpValue == 0.0f) //죽었을 때
        {
            //Death 트리거 발동
            IsDead = true;
            myAnim.SetTrigger("Die");

        }
        else
        {
            if (!myAnim.GetBool("IsStun") && !myAnim.GetBool("IsESkillAttacking") && !myAnim.GetBool("IsQSkillAttacking"))
            {
                myAnim.SetTrigger("Big Damage");
            }
        }
    }
    public override void OnDamage(float dmg) //일반 데미지 받을 때
    {
        curHP -= dmg;
        Hpupdate();

        if (HpValue == 0.0f) //죽었을 때
        {
            //Death 트리거 발동
            IsDead = true;
            myAnim.SetTrigger("Die");
        }
        else
        {
            if (!myAnim.GetBool("IsStun") && !myAnim.GetBool("IsESkillAttacking") && !myAnim.GetBool("IsQSkillAttacking"))
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
        return (HpValue != 0.0f); //살아있음 , false면 죽었음.
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
