using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : CharacterMovement, IBattle
{
    List<IBattle> myAttackers = new List<IBattle>(); //Player�� �����ϴ� ������Ʈ
    [SerializeField] float Sensitivity = 10.0f;
    [SerializeField] Transform myAttackPoint;
    [SerializeField] Transform myESkillAttackPoint;
    [SerializeField] LayerMask myEnemyMask;
    [SerializeField] GameObject myWeapon;

    Transform _target = null;
    Transform myTarget
    {
        get => _target;
        set
        {
            _target = value;
            _target.GetComponent<IBattle>()?.AddAttacker(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    //������ ���� ���(�񵿱� ���)
    public void BaseAttack()
    {
        Collider[] list = Physics.OverlapSphere(myAttackPoint.position, 0.5f, myEnemyMask);

        foreach(Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(myStat.AP); //������ 30
        }
    }

    public void ESkillAttack() //E ��ų ���
    {
        //��ų ����.
        Collider[] list = Physics.OverlapSphere(myESkillAttackPoint.position, 2.8f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnSkillDamage(myStat.SkillAP); //������ 50
        }
    }

    public void ComboCheck(bool v) //�޺� ���� üũ
    {
        if(v)
        {
            //Start Combo Check
            IsCombable = true;
            clickCount = 0;
        }
        else
        {
            //End Combo Check
            IsCombable = false;
            if(clickCount == 0)
            {
                myAnim.SetTrigger("ComboFail");
            }
        }
    }

    //�������̽�

    public void OnBigDamage(float Bigdmg) //���ѵ����� ���� ��
    {
        myStat.HP -= Bigdmg;

        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
            //Death Ʈ���� �ߵ�
        }
        else
        {
            if (!myAnim.GetBool("IsStun"))
            {
                myAnim.SetTrigger("Big Damage");
                myAnim.SetBool("IsStun", true);
                //Big DamageƮ���Ű� �߻����� �ʰ� �ؾ���.(�������� �Ե���)
                //�÷��̾��� ������ ���ƾ���.
            }
            else
            {
                myAnim.SetBool("IsStun", false);
            }
        }
    }
    public void OnDamage(float dmg) //�Ϲ� ������ ���� ��
    {
        myStat.HP -= dmg;

        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
            //Death Ʈ���� �ߵ�
        }
        else //�ڡڼ����׸�ڡ�
        {
            if (!myAnim.GetBool("IsStun"))
            {
                myAnim.SetTrigger("Big Damage");
                myAnim.SetBool("IsStun", true);
                //Big DamageƮ���Ű� �߻����� �ʰ� �ؾ���.(�������� �Ե���)
                //�÷��̾��� ������ ���ƾ���.
            }
            else
            {
                myAnim.SetBool("IsStun", false);
            }
        }
    }
    public void OnSkillDamage(float SkillDamage) //��ų������ ���� ��
    {

    }
    public bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f); //�������
    }
    public void AddAttacker(IBattle ib)
    {
        myAttackers.Add(ib);
    }
    public void DeadMessage(Transform tr)
    {
        if (tr == myTarget)
        {
            StopAllCoroutines();
        }
    }
    public void RemoveAttacker(IBattle ib)
    {

    }
}
