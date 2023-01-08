using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrier : BattleSystem
{
    [SerializeField] Transform myAttackPoint;
    [SerializeField] Transform myESkillAttackPoint;
    [SerializeField] LayerMask myEnemyMask;

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

        if(IsCombable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
        }
    }

    

    //������ ���� ���(�񵿱� ���)
    public void BaseAttack() //�⺻����
    {
        Collider[] list = Physics.OverlapSphere(myAttackPoint.position, 1.0f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(myStat.AP); //������ 30
        }
    }

    public void ESkillAttack() //E ��ų ����
    {
        //��ų ����.
        Collider[] list = Physics.OverlapSphere(myESkillAttackPoint.position, 2.8f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnSkillDamage(myStat.SkillAP); //������ 50
        }
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


    //�������̽�
    public override void OnBigDamage(float Bigdmg) //���ѵ����� ���� ��
    {
        myStat.HP -= Bigdmg;

        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
            //Death Ʈ���� �ߵ�
            myAnim.SetTrigger("Die");
            //�׾ ���Ͱ� ��� ����
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
            myAnim.SetTrigger("Damage");
        }
    }
    public override void OnSkillDamage(float SkillDamage) //��ų������ ���� ��
    {
        //������ ��ų������ �ϴ� ���.
    }
    public override bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f); //�������
    }
    public override void DeadMessage(Transform tr)
    {
        if (tr == myTarget)
        {
            StopAllCoroutines();
        }
    }
}
