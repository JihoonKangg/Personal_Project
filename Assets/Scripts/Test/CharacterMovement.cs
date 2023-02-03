using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterMovement : BattleSystem //�ൿ�� ���õ� ��ũ��Ʈ(����/�÷��̾�)
{
    [SerializeField] float CharacterRotSpeed = 10.0f;
    Quaternion targetRot = Quaternion.identity;
    public float AttackCount = 0.0f;
    

    float targetSpeed = 0.0f;
    //�÷��̾� Movement
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
            && !myAnim.GetBool("IsQSkillAttacking") && dir != Vector3.zero && !myAnim.GetBool("IsStun")) //����, speed�� ����
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * CharacterRotSpeed);
        }
    }

    protected void WarriorAttack()
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
                        AttackCount += 0.05f;
                        break;
                    case 1: //���ѵ�����
                        col.GetComponent<IBattle>()?.OnBigDamage(myStat.AP);
                        break;
                    case 2: //E��ų������
                        col.GetComponent<IBattle>()?.OnESkillDamage(myStat.ESkillAP);
                        AttackCount += 0.1f;
                        break;
                    case 3: //Q��ų������
                        col.GetComponent<IBattle>()?.OnQSkillDamage(myStat.QSkillAP);
                        AttackCount += 0.02f;
                        break;
                }
            }
        }
    }
}
