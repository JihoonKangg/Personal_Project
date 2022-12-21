using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : CharacterMovement, IBattle
{
    List<IBattle> myAttackers = new List<IBattle>(); //Player를 공격하는 오브젝트
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


    //옵저버 패턴 사용(비동기 방식)
    public void BaseAttack()
    {
        Collider[] list = Physics.OverlapSphere(myAttackPoint.position, 0.5f, myEnemyMask);

        foreach(Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(myStat.AP); //데미지 30
        }
    }

    public void SkillAttack()
    {
        //스킬 미정.
        Collider[] list = Physics.OverlapSphere(myAttackPoint.position, 0.5f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnSkillDamage(myStat.SkillAP); //데미지 50
        }
    }

    public void ComboCheck(bool v) //콤보 어택 체크
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




    //인터페이스

    public void OnBigDamage(float Bigdmg) //데미지 받을 때
    {
        if(!myAnim.GetBool("IsStun"))
        {
            myAnim.SetTrigger("Big Damage");
            //Big Damage트리거가 발생하지 않게 해야함.(데미지만 입도록)
            //플레이어의 공격을 막아야함.
        }
    }
    public void OnDamage(float dmg) //데미지 받을 때
    {

    }
    public void OnSkillDamage(float SkillDamage) //데미지 받을 때
    {

    }
    public bool IsLive()
    {
        return !Mathf.Approximately(myStat.HP, 0.0f); //살아있음
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
