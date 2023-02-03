using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrier : CharacterMovement
{
    [SerializeField] GameObject[] QSkillPrefabs;
    [SerializeField] Slider MyHPRightUI;

    //�޺�üũ ���
    bool IsCombable = false;
    int ClickCount = 0;

    void Start()
    {
        
    }

    void Update()
    {
        MyHPRightUI.value = myStat.HP / myStat.MaxHP;
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

        if(GetComponentInChildren<AIPerception>().myTarget != null)
        {
            myTarget = GetComponentInChildren<AIPerception>().myTarget;
        }

        if(myTarget == null)
        {
            myTarget = this.transform;
        }
    }

    
    public void AutoAim()
    {
        if (myTarget == null) return;
        if (myAnim.GetBool("IsComboAttacking") || myAnim.GetBool("IsComboAttacking1")
            || myAnim.GetBool("IsESkillAttacking") || myAnim.GetBool("IsQSkillAttacking") && !myAnim.GetBool("IsDamage"))
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
    public override void AttackTarget(float radius, int a = 0, int b = 0) //a = AttackPoint , b = kind of damage
    {
        base.AttackTarget(radius, a, b);
    }
    public void Attacktarget()
    {
        AttackTarget(myStat.AttackRadius, 0, 0);
    }
    public void ESkillAttack()
    {
        AttackTarget(5.0f, 1, 2);
    }
    public void QSkillAttack()
    {
        Instantiate(QSkillPrefabs[2], myTarget.transform);
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
            if (!myAnim.GetBool("IsStun") && !myAnim.GetBool("IsESkillAttacking") && !myAnim.GetBool("IsQSkillAttacking"))
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
        return !Mathf.Approximately(myStat.HP, 0.0f); //������� , false�� �׾���.
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
