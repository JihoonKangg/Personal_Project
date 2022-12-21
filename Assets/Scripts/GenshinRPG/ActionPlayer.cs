using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : CharacterMovement, IBattle
{
    List<IBattle> myAttackers = new List<IBattle>(); //Player�� �����ϴ� ������Ʈ
    [SerializeField] float Sensitivity = 10.0f;
    [SerializeField] Transform myAttackPoint;
    [SerializeField] LayerMask myEnemyMask;

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
        PlayerMoving(Sensitivity);
        WarriorAttack();
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

    public void SkillAttack()
    {
        //��ų ����.
        Collider[] list = Physics.OverlapSphere(myAttackPoint.position, 0.5f, myEnemyMask);

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

    public void OnBigDamage(float Bigdmg) //������ ���� ��
    {
        if(!myAnim.GetBool("IsStun"))
        {
            myAnim.SetTrigger("Big Damage");
            //Big DamageƮ���Ű� �߻����� �ʰ� �ؾ���.(�������� �Ե���)
            //�÷��̾��� ������ ���ƾ���.
        }
    }
    public void OnDamage(float dmg) //������ ���� ��
    {

    }
    public void OnSkillDamage(float SkillDamage) //������ ���� ��
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
