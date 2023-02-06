using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterMovement : CharacterDATAUSE //행동에 관련된 스크립트(몬스터/플레이어)
{
    [SerializeField] float CharacterRotSpeed = 10.0f;
    Quaternion targetRot = Quaternion.identity;
    protected float AttackCount = 0.0f;
    float targetSpeed = 0.0f;
    //콤보체크 담당
    protected bool IsCombable = false;
    protected int ClickCount = 0;
    public float HpValue = 1.0f;

    protected void PlayerMoving()
    {
        Vector3 dir = Vector2.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        if (!Mathf.Approximately(dir.magnitude, 0))
        {
            targetSpeed = Mathf.Clamp(dir.magnitude, 0.0f, 0.5f);
            
            if (Input.GetKey(KeyCode.LeftShift) && GetComponentInParent<SprintBar>().myStatusSpr != 0.0f)
            {
                targetSpeed = 1.0f;
            }

            float spd = myAnim.GetFloat("Speed");
            spd = Mathf.Lerp(spd, targetSpeed, Time.deltaTime * 10.0f);
            myAnim.SetFloat("Speed", spd);

            dir.Normalize();
            dir = Camera.main.transform.rotation * dir;
            dir.y = 0;
            targetRot = Quaternion.LookRotation(dir);
        }
        else
        {
            float spd = myAnim.GetFloat("Speed");
            spd = Mathf.Lerp(spd, 0.0f, Time.deltaTime * 10.0f);
            myAnim.SetFloat("Speed", spd);
        }
        if (!myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsComboAttacking1") && !myAnim.GetBool("IsESkillAttacking")
            && !myAnim.GetBool("IsQSkillAttacking") && dir != Vector3.zero && !myAnim.GetBool("IsStun")) //방향, speed값 조절
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * CharacterRotSpeed);
        }
    }

    protected void PlayerAttack()
    {
        if (!myAnim.GetBool("IsComboAttacking") && !myAnim.GetBool("IsComboAttacking1") && !myAnim.GetBool("IsESkillAttacking") 
            && !myAnim.GetBool("IsQSkillAttacking") && !myAnim.GetBool("IsStun") && !myAnim.GetBool("IsDamage"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("ComboAttack");
            }
            if (Input.GetKeyDown(KeyCode.E) && GetComponent<SkillCoolTime>().MySkill_IMG[0].fillAmount == 1.0f)
            {
                myAnim.SetTrigger("ESkillAttack");
                GetComponent<SkillCoolTime>().MySkill_IMG[0].fillAmount = 0.0f;
            }
            if (Input.GetKeyDown(KeyCode.Q) && GetComponent<SkillCoolTime>().MySkill_IMG[1].fillAmount == 1.0f)
            {
                myAnim.SetTrigger("QSkillAttack");
                GetComponent<SkillCoolTime>().MySkill_IMG[1].fillAmount = 0.0f;
            }
        }
    }

    //일반공격 연속기 담당
    public void ComboCheck(bool v)
    {
        if (v)
        {
            //Start Combo Check
            IsCombable = true;
            ClickCount = 0;
        }
        else
        {
            //End Combo Check
            IsCombable = false;
            if (ClickCount == 0)
            {
                myAnim.SetTrigger("ComboFail");
            }
        }
    }

    public void AutoAim()
    {
        if (myTarget == null) return;
        if (myAnim.GetBool("IsComboAttacking") || myAnim.GetBool("IsComboAttacking1")
            || myAnim.GetBool("IsESkillAttacking") || myAnim.GetBool("IsQSkillAttacking") && !myAnim.GetBool("IsDamage"))
        {
            Vector3 pos = myTarget.position - transform.position;
            pos.Normalize();
            float delta = RotSpeed * Time.deltaTime;
            float rotDir = 1.0f;
            if (Vector3.Dot(transform.right, pos) < 0)
            {
                rotDir = -rotDir;
            }
            float angle = Vector3.Angle(transform.forward, pos);
            if (angle > 0)
            {
                if (delta > angle)
                {
                    delta = angle;
                }
                angle -= delta;
                transform.Rotate(Vector3.up * rotDir * delta, Space.World);
            }
        }
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
                switch(b)
                {
                    case 0: //일반데미지
                        col.GetComponent<IBattle>()?.OnDamage(AP);
                        AttackCount += 0.05f;
                        break;
                    case 1: //강한데미지
                        col.GetComponent<IBattle>()?.OnBigDamage(AP);
                        break;
                    case 2: //E스킬데미지
                        col.GetComponent<IBattle>()?.OnESkillDamage(ESkillAP);
                        AttackCount += 0.1f;
                        break;
                    case 3: //Q스킬데미지
                        col.GetComponent<IBattle>()?.OnQSkillDamage(QSkillAP);
                        AttackCount += 0.02f;
                        break;
                }
            }
        }
    }
}
