using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TreantGuard : BattleSystem
{
    Vector3 startPos = Vector3.zero;
    public enum STATE
    {
        Create, Idle, Roaming, Battle, Dead
    }
    public STATE myState = STATE.Create;

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                StartCoroutine(DelayRoaming(2.0f));
                break;
            case STATE.Roaming:
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-10.0f, 10.0f);
                pos.z = Random.Range(-10.0f, 10.0f);
                pos = startPos + pos;
                MoveToPosition(pos, () => ChangeState(STATE.Idle));
                break;
            case STATE.Battle:
                MonsterAttackTarget(myTarget); //AttackRange : 2.0f, AttackDelay : 3.0f
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                foreach (IBattle ib in myAttackers)
                {
                    ib.DeadMessage(transform);
                }
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Roaming:
                break;
            case STATE.Battle:
                break;
            case STATE.Dead:
                break;
        }
    }

    IEnumerator DelayRoaming(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(STATE.Roaming);
    }
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        ChangeState(STATE.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public void FindTarget(Transform target)
    {
        myTarget = target;
        StopAllCoroutines();
        ChangeState(STATE.Battle);
    }

    public void LostTarget()
    {
        myTarget = null;
        StopAllCoroutines();
        myAnim.SetBool("Run Forward", false);
        ChangeState(STATE.Idle);
    }

    public void AttackTarget()
    {
        //������ ���� ���(�񵿱� ���)
        if (myTarget.GetComponent<IBattle>().IsLive())
        {
            myTarget.GetComponent<IBattle>()?.OnDamage(myStat.AP); //�Ϲ� ������ ���� ��
        }
    }





    //�������̽�
    public override void OnDamage(float dmg) //������ ���� ��
    {
        myStat.HP -= dmg;
        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Take Damage");
        }
    }
    public override void OnSkillDamage(float Skilldmg)
    {
        myStat.HP -= Skilldmg;
        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
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