using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrier : CharacterMovement
{
    [SerializeField] GameObject[] QSkillPrefabs;
    [SerializeField] Slider MyHPRightUI;

    void Start()
    {

    }
    private void FixedUpdate()
    {
        HP = orgData.CharacterHP(LEVEL);
        curHP = Mathf.Clamp(curHP, 0.0f, HP);

        HpValue = curHP / HP;
        MyHPRightUI.value = HpValue;

        PlayerMoving();
        PlayerAttack();
        AutoAim();
    }
    void Update()
    {
        if (IsCombable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
        }

        if(curHP <= 0) //�÷��̾ �׾��� ��
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
    public void Attacktarget()
    {
        AttackTarget(AttackRadius, 0, 0);
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
        curHP -= Bigdmg;

        if (HpValue == 0.0f) //�׾��� ��
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
        curHP -= dmg;

        if (HpValue == 0.0f) //�׾��� ��
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
