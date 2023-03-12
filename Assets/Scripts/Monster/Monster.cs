using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


public class Monster : BattleSystem
{
    protected Coroutine attackCo = null;
    public MonsterData orgData;
    float IsDamage = 1.0f;
    Coroutine moveCo = null;
    Coroutine rotCo = null;

    public Transform myHpBarPos;
    protected SkinnedMeshRenderer myMesh;
    public Material[] myMaterial;
    protected float HpValue = 1.0f;
    public float curHP;
    protected Material myMat;
    protected Color orgColor;
    protected MonsterHP myUI = null;
    protected GameObject myHpBar = null;
    protected Vector3 startPos = Vector3.zero;
    [SerializeField] Transform QSkillExpPos;
    [SerializeField] protected Transform itemSpawn;
    bool SkillExp = true;



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
                StartCoroutine(Disapearing(6.0f, 20.0f));
                Destroy(myHpBar);
                GameObject obj = Instantiate(Resources.Load("Prefabs/SkillEffect/QSkillballEffect")) as GameObject;
                SceneData.Inst.PlayerLevel.EXP += orgData.EXP;
                SceneData.Inst.ExpSlider.GetComponent<Animator>().SetTrigger("Show");
                HuntMonster();
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

        myMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        myMesh.material = myMaterial[0];
    }
    protected virtual void Update()
    {
        StateProcess();
        HpUpdate();
        curHP = Mathf.Clamp(curHP, 0.0f, orgData.HP);
        HpValue = curHP / orgData.HP;
    }

    //몬스터 Movement
    protected void MonsterAttackTarget(Transform target)
    {
        if (myState == STATE.Dead) return;
        StopAllCoroutines();
        attackCo = StartCoroutine(AttackingTarget(target, orgData.AttackRange, orgData.AttackDelay));
    }

    
    void HpUpdate()
    {
        if (myState == STATE.Dead) return;

        myUI.myBar.value = HpValue;
        myUI.myBGBar.value = Mathf.Lerp(myUI.myBGBar.value, HpValue, 5.0f * Time.deltaTime);

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

    public void HuntMonster()
    {
        QuestController qc = SceneData.Inst.myquest;
        for (int i = 0; i < qc.slots.Length; i++)  //반복문을 돌려 슬롯 길이 체크
        {
            if (qc.slots[i].quest != null)  //슬롯에 있는 퀘스트가 널이 아닐 때
            {
                for(int j = 0; j < qc.slots[i].quest.NeedMonsterCode.Length; j++)
                {
                    if (qc.slots[i].quest.NeedMonsterCode[j] == orgData.MonsterCode)
                    //몬스터의 코드와 슬롯에 있는 몬스터코드가 맞을 때 전송 
                    {
                        if(qc.slots[i].neednum == qc.slots[i].quest.NeedCount)
                        {
                            Debug.Log(qc.slots[i].neednum);

                            return;
                        }
                        qc.slots[i].neednum++; //몬스터 확킬했다고 표시.
                        qc.slots[i].QuestClick();
                    }
                }
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
        pos.y = transform.position.y;
        Vector3 dir = (pos - transform.position).normalized; //몬스터 떨림을 고치는 수정 코드
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
            dir.y = 0.0f;  //몬스터 떨림을 고치는 수정 코드
            dir.Normalize(); //몬스터 떨림을 고치는 수정 코드
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

    IEnumerator Disapearing(float d, float t) //죽어서 사라지는 함수
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

    float time = 0.0f;

    public void UseQSkill()
    {
        StartCoroutine(MonsterStiff());
        curHP -= (orgData.HP * 0.2f);
    }

    IEnumerator MonsterStiff() //궁극시 사용 시 몬스터 Stiff
    {
        while (time <= orgData.QSkillStiffTime)
        {
            time += Time.deltaTime;
            myAnim.SetFloat("AnimSpeed", 0.0f);
            Debug.Log(time);
            myMesh.material = myMaterial[1];
            yield return null;
        }
        myMesh.material = myMaterial[0];
        time = 0.0f; 
        myAnim.SetFloat("AnimSpeed", 1.0f);
    }


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
