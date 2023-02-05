using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class SkillCoolTime : MonoBehaviour
{
    public CharacterData myPlayer;
    public Image[] MySkill_IMG;
    //0�� E��ų, 1�� Q��ų, 2�� ��ų ��Ÿ�� ǥ��
    [SerializeField] GameObject mySkillEffect;
    [SerializeField] TMPro.TMP_Text Cooltime;
    //0�� Q��ų �Ϸ� ����Ʈ
    //1�� ��Ÿ�� �̹���
    [SerializeField] float myCoolTime = 0.0f;

    bool isCooling = false;
    IEnumerator cooldown = null;
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        MySkill_IMG[0].fillAmount = 1.0f; //E��ų Ȱ��ȭ
        MySkill_IMG[1].fillAmount = 1.0f; //Q��ų ��Ȱ��ȭ
        MySkill_IMG[2].gameObject.SetActive(false); //E��ų ��Ÿ�� ǥ�� ��Ȱ��ȭ
        mySkillEffect.SetActive(false); //Q��ų����Ʈ ��Ȱ��ȭ
        myCoolTime = myPlayer.ESkillCoolTime;
    }

    // Update is called once per frame
    void Update()
    {
        UseSkill();
        //Cooltime.text = myCoolTime.ToString();
        Cooltime.text = string.Format("{0:0.0}", myCoolTime);

        if(isCooling) cooldown.MoveNext();

        if(MySkill_IMG[1].fillAmount == 1.0f) mySkillEffect.SetActive(true);
        else mySkillEffect.SetActive(false);
    }

    Coroutine coCool = null;
    public void UseSkill()
    {
        if(MySkill_IMG[0].fillAmount == 0.0f) //E��ų ������� ��
        {
            MySkill_IMG[2].gameObject.SetActive(true);
            isCooling = true;
            cooldown = UseCoolTime();
        }
        else MySkill_IMG[2].gameObject.SetActive(false); //E��ų�� ��� ������ ������ ��
    }

    IEnumerator UseCoolTime()
    {
        while (myCoolTime > 0.0f)
        {
            myCoolTime -= Time.deltaTime;
            MySkill_IMG[2].fillAmount = myCoolTime / myPlayer.ESkillCoolTime;
            yield return null;
        }
        MySkill_IMG[0].fillAmount = 1.0f;
        myCoolTime = myPlayer.ESkillCoolTime;
        coCool = null;
        isCooling = false;
    }
}
