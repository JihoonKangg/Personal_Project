using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class Golem : Monster
{
    int rnd;
    public override void AttackTarget(float radius, int a = 0, int b = 0)
    {
        int num = 0;
        rnd = Random.Range(0, 100);
        if (rnd < 60) num = 0;
        else num = 1;
        base.AttackTarget(radius, 0, num);
    }

    public void Attacktarget()
    {
        AttackTarget(orgData.AttackRadius);
    }

    private void Deaditem()
    {
        int count = Random.Range(0, 3);
        for (int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(Resources.Load("Prefabs/Item/GolemItem"), transform) as GameObject;
        }
    }


    //인터페이스

    public override void OnDamage(float dmg) //데미지 입을 때
    {
        curHP -= dmg;
        if(curHP <= 0) //죽었을 때
        {
            Deaditem();
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Take Damage");
        }
    }
    public override void OnESkillDamage(float ESkilldmg)
    {
        curHP -= ESkilldmg;
        if (curHP <= 0) //죽었을 때
        {
            Deaditem();
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Take Damage");
        }
    }
    public override void OnQSkillDamage(float QSkilldmg)
    {
        curHP -= QSkilldmg;
        if (curHP <= 0) //죽었을 때
        {
            Deaditem();
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Take Damage");
        }
    }
    public override bool IsLive()
    {
        return myState != STATE.Dead; //살아있음
    }
    public override void DeadMessage(Transform tr)
    {
        if(tr == myTarget)
        {
            LostTarget();
        }
    }
}
