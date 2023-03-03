using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBat : Monster
{
    public void Attacktarget()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/ForestBatAttackObject"), myAttackPoint[0]) as GameObject;
    }

    private void Deaditem()
    {
        int count = Random.Range(0, 3);
        for(int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(Resources.Load("Prefabs/Item/BatItem"), transform) as GameObject;
        }
    }


    //인터페이스

    public override void OnDamage(float dmg) //데미지 입을 때
    {
        curHP -= dmg;
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
        if (tr == myTarget)
        {
            LostTarget();
        }
    }
}
