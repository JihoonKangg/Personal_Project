using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//public delegate void MyAction();
//함수 저장

public class CharacterMovement : CharacterProperty
{
    Vector2 targetDir = Vector2.zero;
    protected bool IsCombable = false;
    protected int clickCount = 0;

    Coroutine moveCo = null;
    Coroutine rotCo = null;

    bool IsCharacterRot = false;
    [SerializeField] float CharacterRotSpeed = 1.0f;
    [SerializeField] Transform myFollowCam;
    [SerializeField] Slider myStaminaBar;

    protected void MonsterAttackTarget(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(AttackingTarget(target, myStat.AttackRange, myStat.AttackDelay));
    }
    protected void PlayerAttackTarget(Transform target)
    {
        StopAllCoroutines();
    }

    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot = true)
    {
        if (moveCo != null)
        {
            StopCoroutine(moveCo);
            moveCo = null;
        }
        moveCo = StartCoroutine(MovingToPostion(pos, done));
        if(Rot)
        {
            if (rotCo != null)
            {
                StopCoroutine(rotCo);
                rotCo = null;
            }
            rotCo = StartCoroutine(RotatingToPosition(pos));
        }
    }
    protected void PlayerMoving(float Sensitivity)
    {
        targetDir.x = Input.GetAxis("Horizontal"); //x축값(왼/오 무빙 담당)
        targetDir.y = Input.GetAxis("Vertical"); //y축값(앞/뒤 무빙 담당)

        float x = Mathf.Lerp(myAnim.GetFloat("x"), targetDir.x, Time.deltaTime * Sensitivity);
        float y = Mathf.Lerp(myAnim.GetFloat("y"), targetDir.y, Time.deltaTime * Sensitivity);

        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);

        if(y > 0.1f)
        {
            IsCharacterRot = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, myFollowCam.rotation, CharacterRotSpeed * Time.deltaTime);
        }
        else
        {
            IsCharacterRot = false;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !myStaminaBar.value.Equals(0)) myAnim.SetBool("IsFastRun", true);
        else myAnim.SetBool("IsFastRun", false);
    }
    protected void WarriorAttack()
    {
        if(Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsComboAttacking"))
        {
            myAnim.SetTrigger("ComboAttack");
        }
        if (IsCombable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ++clickCount;
            }
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

        //걷기 시작
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
        //걷기 끝 - 도착
        myAnim.SetBool(("Walk Forward"), false);
        done?.Invoke();
    }

    IEnumerator AttackingTarget(Transform target, float AttackRange, float AttackDelay) //대상을 따라다니는 타겟
    {
        float playTime = 0.0f;
        float delta = 0.0f;
        while(target != null)
        {
            if (!myAnim.GetBool("IsAttacking")) playTime += Time.deltaTime;
            //이동
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;
            if (dist > AttackRange)
            {
                myAnim.SetBool("Run Forward", true);
                myAnim.SetBool("Walk Forward", true);
                dir.Normalize();
                delta = myStat.RunSpeed * Time.deltaTime;
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
                    //공격
                    playTime = 0.0f;
                    myAnim.SetTrigger("Punch");
                }
            }
            //회전
            delta = myStat.RotSpeed * Time.deltaTime;
            float Angle = Vector3.Angle(dir, transform.forward);
            float rotDir = 1.0f;
            if(Vector3.Dot(transform.right, dir) < 0.0f)
            {
                rotDir = -rotDir;
            }
            if(delta > Angle)
            {
                delta = Angle;
            }
            transform.Rotate(Vector3.up * delta * rotDir, Space.World);

            yield return null;
        }
        myAnim.SetBool("Run Forward", false);
    }
}
