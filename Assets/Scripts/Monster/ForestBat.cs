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


    //�������̽�

    public override void OnDamage(float dmg) //������ ���� ��
    {
        curHP -= dmg;
        if (curHP <= 0) //�׾��� ��
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
        if (curHP <= 0) //�׾��� ��
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
        if (curHP <= 0) //�׾��� ��
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
        return myState != STATE.Dead; //�������
    }
    public override void DeadMessage(Transform tr)
    {
        if (tr == myTarget)
        {
            LostTarget();
        }
    }
}
