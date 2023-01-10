using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Warrier : BattleSystem
{
    //�޺�üũ ���
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

        if(myStat.HP <= 0) //�÷��̾ �׾��� ��
        {
            foreach (IBattle ib in myAttackers)
            {
                ib.DeadMessage(transform);
            }
        }
    }
    
    public void AutoAim()
    {
        if (myTarget == null) return;
        if (myTarget.GetComponent<IBattle>().IsLive())
        {
            if (myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsDamage"))
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
        else
        {
            LostTarget();
            myTarget = null;
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

    //�Ϲݰ��� ���ӱ� ���
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
        if(myAnim.GetBool(""))//E��ų ����� ��
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


    //�������̽�
    public override void OnBigDamage(float Bigdmg) //���ѵ����� ���� ��
    {
        myStat.HP -= Bigdmg;

        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
            //Death Ʈ���� �ߵ�
            myAnim.SetTrigger("Die");
            
        }
        else
        {
            if (!myAnim.GetBool("IsStun"))
            {
                myAnim.SetTrigger("Big Damage");
            }
        }
    }
    public override void OnDamage(float dmg) //�Ϲ� ������ ���� ��
    {
        myStat.HP -= dmg;

        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
            //Death Ʈ���� �ߵ�
            myAnim.SetTrigger("Die");
        }
        else
        {
            if (!myAnim.GetBool("IsStun"))
            {
                myAnim.SetTrigger("Damage");
            }
        }
    }
    public override void OnSkillDamage(float SkillDamage) //��ų������ ���� ��
    {
        //������ ��ų������ �ϴ� ���.
    }
    public override bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f); //������� , false�� �׾���.
    }
    public override void DeadMessage(Transform tr)
        //���Ͱ� �׾��� �� ȣ��ǵ���.
    {
        if (tr == myTarget)
        {
            myTarget = null;
            StopAllCoroutines();
        }
    }
}
