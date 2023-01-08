using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Golem : CharacterMovement, IBattle
{
    List<IBattle> myAttackers = new List<IBattle>(); //Golem을 공격하는 오브젝트
    Transform _target = null;
    Transform myTarget
    {
        get => _target;
        set
        {
            _target = value;
            if(_target != null) _target.GetComponent<IBattle>()?.AddAttacker(this);

        }
    }
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
        switch(myState)
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
                foreach(IBattle ib in myAttackers)
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
        //옵저버 패턴 사용(비동기 방식)
        if(myTarget.GetComponent<IBattle>().IsLive())
        {
            myTarget.GetComponent<IBattle>()?.OnBigDamage(myStat.AP); //데미지 30
            //myTarget.GetComponent<IBattle>()?.OnSkillDamage(myStat.SkillAP); //데미지 50
        }
    }





    //인터페이스

    public void OnBigDamage(float Bigdmg)
    {

    }
    public void OnDamage(float dmg) //데미지 입을 때
    {
        myStat.HP -= dmg;
        myAnim.SetTrigger("Take Damage");
        if(Mathf.Approximately(myStat.HP, 0)) //죽었을 때
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Take Damage");
        }
    }
    public void OnSkillDamage(float Skilldmg)
    {
        myStat.HP -= Skilldmg;
        myAnim.SetTrigger("Take Damage");
        if (Mathf.Approximately(myStat.HP, 0)) //죽었을 때
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Take Damage");
        }
    }
    public bool IsLive()
    {
        return myState != STATE.Dead; //살아있음
    }
    public void AddAttacker(IBattle ib)
    {
        myAttackers.Add(ib);
    }
    public void DeadMessage(Transform tr)
    {
        if(tr == myTarget)
        {
            LostTarget();
        }
    }
    public void RemoveAttacker(IBattle ib)
    {

    }
}
