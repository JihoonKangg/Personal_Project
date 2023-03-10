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

        if (curHP <= 0) //�÷��̾ �׾��� ��
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
    

    //�������̽�
    public override void OnBigDamage(float Bigdmg) //���ѵ����� ���� ��
    {
        curHP -= Bigdmg;
        Hpupdate();

        if (HpValue == 0.0f) //�׾��� ��
        {
            //Death Ʈ���� �ߵ�
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
    public override void OnDamage(float dmg) //�Ϲ� ������ ���� ��
    {
        curHP -= dmg;
        Hpupdate();

        if (HpValue == 0.0f) //�׾��� ��
        {
            //Death Ʈ���� �ߵ�
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
    public override void OnESkillDamage(float ESkillDamage) //��ų������ ���� ��
    {
        //������ ��ų������ �ϴ� ���.
    }
    public override bool IsLive()
    {
        return (HpValue != 0.0f); //������� , false�� �׾���.
    }
    public override void DeadMessage(Transform tr)
    //���Ͱ� �׾��� �� ȣ��ǵ���.
    {
        if (tr == myTarget)
        {
            StopAllCoroutines();
        }
    }
}
