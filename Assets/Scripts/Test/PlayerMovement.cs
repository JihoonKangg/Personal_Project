using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : CharacterProperty
{
    [SerializeField] float CharacterRotSpeed = 10.0f;
    Quaternion targetRot = Quaternion.identity;
    public Slider mySprint;


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
                //쉬프트키 눌렀다 떼었다 했을때 수치가 확 바뀌는 과정을 수정해야함.
            }
            dir.Normalize();
            dir = Camera.main.transform.rotation * dir;
            dir.y = 0;
            targetRot = Quaternion.LookRotation(dir);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * CharacterRotSpeed);
    }

    protected void WarriorAttack()
    {
        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsComboAttacking"))
        {
            myAnim.SetTrigger("ComboAttack");
        }
        /*if (IsCombable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ++clickCount;
            }
        }*/
        if (Input.GetKeyDown(KeyCode.E))
        {
            myAnim.SetTrigger("ESkillAttack");
        }
    }
}
