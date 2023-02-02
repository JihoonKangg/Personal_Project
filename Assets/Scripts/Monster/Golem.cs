using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Golem : BattleSystem
{
    public Transform myHpBarPos;
    MonsterHP myUI = null;
    GameObject myHpBar = null;
    Vector3 startPos = Vector3.zero;
    [SerializeField] Transform QSkillExpPos;
    bool SkillExp = true;
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
                MonsterAttackTarget(myTarget);
                myHpBar.SetActive(true);
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                foreach(IBattle ib in myAttackers)
                {
                    ib.DeadMessage(transform);
                }
                StartCoroutine(Disapearing(4.0f, 3.0f));
                Destroy(myHpBar);
                GameObject obj = Instantiate(Resources.Load("Prefabs/SkillEffect/QSkillballEffect")) as GameObject;
                obj.transform.position = QSkillExpPos.position;
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
        SkillExp = true;
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
        HpUpdate();

        float value = myStat.HP / myStat.MaxHP;
        if (value <= 0.5) 
        {
            if(SkillExp)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/SkillEffect/QSkillballEffect")) as GameObject;
                obj.transform.position = QSkillExpPos.position;
                SkillExp = false;
            }
        }
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
        if(myState == STATE.Dead) return;
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
    public override void AttackTarget(float radius, int a = 0, int b = 0)
    {
        base.AttackTarget(radius, a, b);
    }

    public void Attacktarget()
    {
        AttackTarget(myStat.AttackRadius, 0, 1);
    }

    //인터페이스

    public override void OnDamage(float dmg) //데미지 입을 때
    {
        myStat.HP -= dmg;
        if(Mathf.Approximately(myStat.HP, 0)) //죽었을 때
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
        if (Mathf.Approximately(myStat.HP, 0)) //죽었을 때
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
        if (Mathf.Approximately(myStat.HP, 0)) //죽었을 때
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
