using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterMovement : CharacterProperty //�ൿ�� ���õ� ��ũ��Ʈ(����/�÷��̾�)
{
    [SerializeField] float CharacterRotSpeed = 10.0f;
    Quaternion targetRot = Quaternion.identity;
    public Slider mySprint;

    Coroutine moveCo = null;
    Coroutine rotCo = null;
    protected Coroutine attackCo = null;

    //�÷��̾� Movement
    protected void PlayerMoving()
    {
        Vector3 dir = Vector2.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        if (!Mathf.Approximately(dir.magnitude, 0))
        {
            float spd = Mathf.Clamp(dir.magnitude, 0.0f, 1.0f);
            myAnim.SetFloat("Speed", spd / 2.0f);
            if (Input.GetKey(KeyCode.LeftShift) && mySprint.value != 0.0f)
            {
                myAnim.SetFloat("Speed", spd);
                //����ƮŰ ������ ������ ������ ��ġ�� Ȯ �ٲ�� ������ �����ؾ���.
            }
            dir.Normalize();
            dir = Camera.main.transform.rotation * dir;
            dir.y = 0;
            targetRot = Quaternion.LookRotation(dir);
        }
        if (!myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsComboAttacking1") && !myAnim.GetBool("IsSkillAttacking")
            && dir != Vector3.zero && !myAnim.GetBool("IsStun")) //����, speed�� ����
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * CharacterRotSpeed);
        }
    }

    protected void WarriorAttack()
    {
        if (!myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsComboAttacking1") && !myAnim.GetBool("IsSkillAttacking")
            && !myAnim.GetBool("IsStun") && !myAnim.GetBool("IsDamage"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("ComboAttack");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                myAnim.SetTrigger("ESkillAttack");
            }
        }
    }

    

    //���� Movement
    protected void MonsterAttackTarget(Transform target)
    {
        StopAllCoroutines();
        attackCo = StartCoroutine(AttackingTarget(target, myStat.AttackRange, myStat.AttackDelay));
    }

    //��ġ�� �����ϴ� �Լ�
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
        Vector3 dir = (pos - transform.position).normalized;
        float Angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -rotDir;
        }

        while (Angle > 0.0f)
        {
            if (!myAnim.GetBool("IsAttacking"))
            {
                float delta = myStat.RotSpeed * Time.deltaTime;
                if (delta > Angle)
                {
                    delta = Angle;
                }
                Angle -= delta;
                transform.Rotate(Vector3.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }


    IEnumerator MovingToPostion(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        //�ȱ� ����
        myAnim.SetBool("Walk Forward", true);
        while (dist > 0.0f)
        {
            if (myAnim.GetBool("IsAttacking"))
            {
                myAnim.SetBool(("Walk Forward"), false);
                yield break;
            }


            if (!myAnim.GetBool("IsAttacking"))
            {
                float delta = myStat.WalkSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
        //�ȱ� �� - ����
        myAnim.SetBool(("Walk Forward"), false);
        done?.Invoke();
    }


    IEnumerator AttackingTarget(Transform target, float AttackRange, float AttackDelay) //����� ����ٴϴ� Ÿ��
    {
        float playTime = 0.0f;
        float delta = 0.0f;
        while (target != null)
        {
            if (!myAnim.GetBool("IsAttacking")) playTime += Time.deltaTime;
            //�̵�
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;
            if (dist > AttackRange)
            {
                myAnim.SetBool("Run Forward", true);
                myAnim.SetBool("Walk Forward", true);
                dir.Normalize();
                delta = myStat.RunSpeed * Time.deltaTime;
                //if(myAnim.GetBool("Run Forward")) delta = myStat.RunSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                transform.Translate(dir * delta, Space.World);
            }
            else
            {
                myAnim.SetBool("Run Forward", false);
                myAnim.SetBool("Walk Forward", false);
                if (playTime >= AttackDelay)
                {
                    //����
                    playTime = 0.0f;
                    myAnim.SetTrigger("Punch");
                }
            }
            //ȸ��
            delta = myStat.RotSpeed * Time.deltaTime;
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
            if(!myAnim.GetBool("IsAttacking"))
            {
                transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            }
            yield return null;
        }
        myAnim.SetBool("Run Forward", false);
    }

    public IEnumerator Disapearing(float d, float t) //�׾ ������� �Լ�
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



    //���� ��� �Լ�(����/�÷��̾�)

    public Transform[] myAttackPoint;
    [SerializeField] LayerMask myEnemyMask;

    public virtual void AttackTarget(float radius, int a = 0, int b = 0) //������ ���ϴ� �Լ�
    {
        Collider[] list = Physics.OverlapSphere(myAttackPoint[a].position, radius, myEnemyMask);

        foreach (Collider col in list)
        {
            if (col.GetComponent<IBattle>().IsLive())
            {
                switch(b)
                {
                    case 0: //�Ϲݵ�����
                        col.GetComponent<IBattle>()?.OnDamage(myStat.AP);
                        break;
                    case 1: //���ѵ�����
                        col.GetComponent<IBattle>()?.OnBigDamage(myStat.AP);
                        break;
                    case 2: //E��ų������
                        col.GetComponent<IBattle>()?.OnSkillDamage(myStat.SkillAP);
                        break;
                    case 3: //Q��ų������
                        //col.GetComponent<IBattle>()?.OnSkillDamage(myStat.AP);
                        break;
                }
            }
        }
    }
}
