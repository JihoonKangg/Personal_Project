using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class Monster : BattleSystem
{
    protected Coroutine attackCo = null;
    public MonsterData orgData;
    float IsDamage = 1.0f;
    Coroutine moveCo = null;
    Coroutine rotCo = null;

    public Transform myHpBarPos;
    protected MonsterHP myUI = null;
    protected GameObject myHpBar = null;
    protected Vector3 startPos = Vector3.zero;
    [SerializeField] Transform QSkillExpPos;
    bool SkillExp = true;
    public float curHP;

    void Awake()
    {
        
    }

    
    public enum STATE
    {
        Create, Idle, Roaming, Battle, Stiff, Dead
    }
    public STATE myState = STATE.Create;

    protected virtual void ChangeState(STATE s)
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
                MonsterAttackTarget(myTarget);
                myHpBar.SetActive(true);
                break;
            case STATE.Stiff:
                myAnim.SetFloat("AnimSpeed", 0.0f);
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
                GameObject obj = Instantiate(Resources.Load("Prefabs/SkillEffect/QSkillballEffect")) as GameObject;
                obj.transform.position = QSkillExpPos.position;
                break;
        }
    }

    protected virtual void StateProcess()
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
            case STATE.Stiff:
                break;
            case STATE.Dead:
                break;
        }
    }

    protected IEnumerator DelayRoaming(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(STATE.Roaming);
    }

    protected virtual void Start()
    {
        GameObject hpBars = GameObject.Find("MonsterHpBar");
        myHpBar = Instantiate(Resources.Load("Prefabs/UI/MonsterHPBar"), hpBars.transform) as GameObject;
        myUI = myHpBar.GetComponent<MonsterHP>();
        myUI.myTarget = myHpBarPos;
        myHpBar.SetActive(false);

        startPos = transform.position;
        ChangeState(STATE.Idle);
        SkillExp = true;
        curHP = orgData.HP;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        StateProcess();
        HpUpdate();
        curHP = Mathf.Clamp(curHP, 0.0f, orgData.HP);
    }

    //몬스터 Movement
    protected void MonsterAttackTarget(Transform target)
    {
        StopAllCoroutines();
        attackCo = StartCoroutine(AttackingTarget(target, orgData.AttackRange, orgData.AttackDelay));
    }

    
    void HpUpdate()
    {
        if (myState == STATE.Dead) return;
        myUI.myBar.value = curHP / orgData.HP;
        myUI.myBGBar.value = Mathf.Lerp(myUI.myBGBar.value, curHP / orgData.HP, 5.0f * Time.deltaTime);

        if (myState != STATE.Battle && myHpBar.activeSelf)
        {
            myHpBar.SetActive(false);
        }

        //궁극기 경험치
        if(myUI.myBar.value <= 0.5)
        {
            if (SkillExp)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/SkillEffect/QSkillballEffect")) as GameObject;
                obj.transform.position = QSkillExpPos.position;
                SkillExp = false;
            }
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

    





    //위치값 전달하는 함수
    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot = true)
    {
        if (attackCo != null)
        {
            StopCoroutine(attackCo);
            attackCo = null;
        }
        if (moveCo != null)
        {
            StopCoroutine(moveCo);
            moveCo = null;
        }
        moveCo = StartCoroutine(MovingToPostion(pos, done));
        if (Rot)
        {
            if (rotCo != null)
            {
                StopCoroutine(rotCo);
                rotCo = null;
            }
            rotCo = StartCoroutine(RotatingToPosition(pos));
        }
    }

    IEnumerator RotatingToPosition(Vector3 pos)
    {
        if (myAnim.GetBool("IsDamage")) IsDamage = 0.0f;
        else IsDamage = 1.0f;

        Vector3 dir = (pos - transform.position).normalized;
        float Angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -rotDir;
        }

        while (Angle > 0.0f)
        {
            if (myAnim.GetBool("IsDamage")) myAnim.SetFloat("AnimSpeed", 0.0f);
            else myAnim.SetFloat("AnimSpeed", 1.0f);

            if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsDamage"))
            {
                float delta = orgData.RotSpeed * Time.deltaTime;
                if (delta > Angle)
                {
                    delta = Angle;
                }
                Angle -= delta;
                transform.Rotate(Vector3.up * rotDir * IsDamage * delta * myAnim.GetFloat("AnimSpeed"), Space.World);
            }
            yield return null;
        }
    }


    IEnumerator MovingToPostion(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();


        //걷기 시작
        myAnim.SetBool("Walk Forward", true);
        if (myAnim.GetBool("IsDamage")) IsDamage = 0.0f;
        else IsDamage = 1.0f;

        while (dist > 0.0f)
        {
            if (myAnim.GetBool("IsAttacking") && myAnim.GetBool("IsDamage"))
            {
                myAnim.SetBool(("Walk Forward"), false);
                yield break;
            }


            if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsDamage"))
            {
                float delta = orgData.WalkSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta * IsDamage * myAnim.GetFloat("AnimSpeed"), Space.World);
            }
            yield return null;
        }
        //걷기 끝 - 도착
        myAnim.SetBool(("Walk Forward"), false);
        done?.Invoke();
    }


    IEnumerator AttackingTarget(Transform target, float AttackRange, float AttackDelay) //대상을 따라다니는 타겟
    {
        float playTime = 0.0f;
        float delta = 0.0f;

        while (target != null)
        {
            if (myAnim.GetBool("IsDamage")) IsDamage = 0.0f;
            else IsDamage = 1.0f;

            if (!myAnim.GetBool("IsAttacking")) playTime += Time.deltaTime;
            //이동
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;
            if (dist > AttackRange)
            {
                myAnim.SetBool("Run Forward", true);
                myAnim.SetBool("Walk Forward", true);
                dir.Normalize();
                delta = orgData.RunSpeed * Time.deltaTime;
                //if(myAnim.GetBool("Run Forward")) delta = myStat.RunSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }

                transform.Translate(dir * delta * IsDamage * myAnim.GetFloat("AnimSpeed"), Space.World);
            }
            else
            {
                myAnim.SetBool("Run Forward", false);
                myAnim.SetBool("Walk Forward", false);
                if (playTime >= AttackDelay)
                {
                    //공격
                    playTime = 0.0f;
                    myAnim.SetTrigger("Punch");
                }
            }
            //회전
            delta = orgData.RotSpeed * Time.deltaTime;
            float Angle = Vector3.Angle(dir, transform.forward);
            float rotDir = 1.0f;
            if (Vector3.Dot(transform.right, dir) < 0.0f)
            {
                rotDir = -rotDir;
            }
            if (delta > Angle)
            {
                delta = Angle;
            }
            if (!myAnim.GetBool("IsAttacking") || !myAnim.GetBool("IsDamage"))
            {
                transform.Rotate(Vector3.up * delta * rotDir * IsDamage * myAnim.GetFloat("AnimSpeed"), Space.World);
            }
            yield return null;
        }
        myAnim.SetBool("Run Forward", false);
    }

    public IEnumerator Disapearing(float d, float t) //죽어서 사라지는 함수
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        yield return new WaitForSeconds(t);

        float dist = d;
        while (dist > 0.0f)
        {
            float delta = 2.0f * Time.deltaTime;
            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(Vector3.down * delta, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }

    //공용 사용 함수(몬스터/플레이어)

    public Transform[] myAttackPoint;
    [SerializeField] LayerMask myEnemyMask;

    public virtual void AttackTarget(float radius, int a = 0, int b = 0) //데미지 가하는 함수
    {
        Collider[] list = Physics.OverlapSphere(myAttackPoint[a].position, radius, myEnemyMask);


        foreach (Collider col in list)
        {
            if (col.GetComponent<IBattle>().IsLive())
            {
                switch (b)
                {
                    case 0: //일반데미지
                        col.GetComponent<IBattle>()?.OnDamage(orgData.AP);
                        break;
                    case 1: //강한데미지
                        col.GetComponent<IBattle>()?.OnBigDamage(orgData.AP);
                        break;
                }
            }
        }
    }
}
