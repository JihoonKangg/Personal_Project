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
    void Update()
    {
        HP = orgData.CharacterHP(LEVEL);
        Mathf.Clamp(curHP, 0, HP);

        MyHPRightUI.value = curHP / HP;
        PlayerMoving();
        PlayerAttack();
        AutoAim();
        if (IsCombable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
        }

        if(curHP <= 0) //플레이어가 죽었을 때
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


    //인터페이스
    public override void OnBigDamage(float Bigdmg) //강한데미지 받을 때
    {
        curHP -= Bigdmg;

        if (Mathf.Approximately(curHP, 0)) //죽었을 때
        {
            //Death 트리거 발동
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

        if (Mathf.Approximately(curHP, 0.0f)) //죽었을 때
        {
            //Death 트리거 발동
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
        return !Mathf.Approximately(curHP, 0.0f); //살아있음 , false면 죽었음.
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
