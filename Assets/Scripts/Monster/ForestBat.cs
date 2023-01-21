using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBat : BattleSystem
{
    public Transform myHpBarPos;
    public Transform myAttackPos;
    MonsterHP myUI = null;
    GameObject myHpBar = null;
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
                myHpBar.SetActive(true);
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                foreach (IBattle ib in myAttackers)
                {
                    ib.DeadMessage(transform);
                }
                StartCoroutine(Disapearing(4.0f, 3.0f));
                Destroy(myHpBar);
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
        GameObject hpBars = GameObject.Find("MonsterHpBar");
        myHpBar = Instantiate(Resources.Load("Prefabs/UI/MonsterHPBar"), hpBars.transform) as GameObject;
        myUI = myHpBar.GetComponent<MonsterHP>();
        myUI.myTarget = myHpBarPos;
        myHpBar.SetActive(false);

        startPos = transform.position;
        ChangeState(STATE.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        HpUpdate();
    }

    void HpUpdate()
    {
        if (myState == STATE.Dead) return;
        myUI.myBar.value = myStat.HP / myStat.MaxHP;
        myUI.myBGBar.value = Mathf.Lerp(myUI.myBGBar.value, myStat.HP / myStat.MaxHP, 5.0f * Time.deltaTime);
        if (myState != STATE.Battle && myHpBar.activeSelf)
        {
            myHpBar.SetActive(false);
        }
    }

    public void FindTarget(Transform target)
    {
        if (myState == STATE.Dead) return;
        myTarget = target;
        StopAllCoroutines();
        ChangeState(STATE.Battle);
    }

    public void LostTarget()
    {
        if (myState == STATE.Dead) return;
        myTarget = null;
        StopAllCoroutines();
        myAnim.SetBool("Run Forward", false);
        ChangeState(STATE.Idle);
    }
    /*public override void AttackTarget(float radius, int a = 0, int b = 0)
    {
        base.AttackTarget(radius, a, b);
    }*/

    public void Attacktarget()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/ForestBatAttackObject"), myAttackPos) as GameObject;
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
    public override void OnESkillDamage(float ESkilldmg)
    {
        myStat.HP -= ESkilldmg;
        if (Mathf.Approximately(myStat.HP, 0)) //�׾��� ��
        {
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Take Damage");
        }
    }
    public override void OnQSkillDamage(float QSkilldmg)
    {
        myStat.HP -= QSkilldmg;
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