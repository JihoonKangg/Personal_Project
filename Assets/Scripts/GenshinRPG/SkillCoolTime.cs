using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class SkillCoolTime : CharacterProperty
{
    public Image[] MySkill_IMG;
    //0번 E스킬, 1번 Q스킬, 2번 스킬 쿨타임 표시
    [SerializeField] GameObject mySkillEffect;
    [SerializeField] TMPro.TMP_Text Cooltime;
    //0번 Q스킬 완료 이펙트
    //1번 쿨타임 이미지
    [SerializeField] float myCoolTime = 0.0f;

    bool isCooling = false;
    IEnumerator cooldown = null;
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        MySkill_IMG[0].fillAmount = 1.0f; //E스킬 활성화
        MySkill_IMG[1].fillAmount = 1.0f; //Q스킬 비활성화
        MySkill_IMG[2].gameObject.SetActive(false); //E스킬 쿨타임 표시 비활성화
        mySkillEffect.SetActive(false); //Q스킬이펙트 비활성화
        myCoolTime = myStat.ESkillCoolTime;
    }

    // Update is called once per frame
    void Update()
    {
        UseSkill();
        //Cooltime.text = myCoolTime.ToString();
        Cooltime.text = string.Format("{0:0.0}", myCoolTime);

        if(isCooling) 
        {
            cooldown.MoveNext();
        }

        if(MySkill_IMG[1].fillAmount == 1.0f)
        {
            mySkillEffect.SetActive(true);
        }
    }

    Coroutine coCool = null;
    public void UseSkill()
    {
        if(MySkill_IMG[0].fillAmount == 0.0f) //E스킬 사용했을 때
        {
            MySkill_IMG[2].gameObject.SetActive(true);
            isCooling = true;
            cooldown = UseCoolTime();
        }
        else //E스킬이 사용 가능한 상태일 때
        {
            MySkill_IMG[2].gameObject.SetActive(false);
        }
    }

    IEnumerator UseCoolTime()
    {
        while (myCoolTime > 0.0f)
        {
            myCoolTime -= Time.deltaTime;
            MySkill_IMG[2].fillAmount = myCoolTime / myStat.ESkillCoolTime;
            yield return null;
        }
        MySkill_IMG[0].fillAmount = 1.0f;
        myCoolTime = myStat.ESkillCoolTime;
        coCool = null;
        isCooling = false;
    }
}
