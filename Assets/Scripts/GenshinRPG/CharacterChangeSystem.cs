using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeSystem : MonoBehaviour
{
    public GameObject[] myPlayer;
    public GameObject[] myPlayerSkillUI;
    public Animator[] UserUIControl;
    //0 : Warrior
    //1 : Wizard

    public enum ChooseCharacter
    {
        Warrior, //전사
        Wizard  //마법사
    }
    public ChooseCharacter myCharacter = ChooseCharacter.Warrior;

    void ChangeCharacter(ChooseCharacter s)
    {
        if(myCharacter == s) return;
        myCharacter = s;
        switch(myCharacter)
        {
            case ChooseCharacter.Warrior:
                myPlayer[0].SetActive(true);
                myPlayerSkillUI[0].SetActive(true);
                myPlayer[1].SetActive(false);
                myPlayerSkillUI[1].SetActive(false);
                break;
            case ChooseCharacter.Wizard:
                myPlayer[1].SetActive(true);
                myPlayer[0].SetActive(false);
                myPlayerSkillUI[0].SetActive(false);
                myPlayerSkillUI[1].SetActive(true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacter(myCharacter);

        myPlayer[0].SetActive(true);
        myPlayer[1].SetActive(false);

        UserUIControl[0].SetBool("WarrierChoose", true);
        UserUIControl[1].SetBool("WizardChoose", false);
    }

    // Update is called once per frame
    void Update()
    {
        Animator myAnim = GetComponentInChildren<Animator>();
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && !myAnim.GetBool("IsAttaking") && !myAnim.GetBool("IsComboAttacking")
            && !myAnim.GetBool("IsDamage") && !myAnim.GetBool("IsComboAttacking1") &&
            !myAnim.GetBool("IsESkillAttacking") && !myAnim.GetBool("IsQSkillAttacking"))
        {
            if (SceneData.Inst.warrior.IsDead)
            {
                SceneData.Inst.CantChangeMessage.SetTrigger("CantChange");
                return;
            }
            ChangeCharacter(ChooseCharacter.Warrior);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && !myAnim.GetBool("IsAttaking") && !myAnim.GetBool("IsComboAttacking")
            && !myAnim.GetBool("IsDamage") && !myAnim.GetBool("IsComboAttacking1") &&
            !myAnim.GetBool("IsESkillAttacking") && !myAnim.GetBool("IsQSkillAttacking"))
        {
            if (SceneData.Inst.wizard.IsDead)
            {
                SceneData.Inst.CantChangeMessage.SetTrigger("CantChange");
                return;
            }
            ChangeCharacter(ChooseCharacter.Wizard);
        }
        


        switch(myCharacter) 
        {
            case ChooseCharacter.Warrior:
                myPlayer[1].transform.position = myPlayer[0].transform.position;
                UserUIControl[0].SetBool("WarrierChoose", true);
                UserUIControl[1].SetBool("WizardChoose", false);
                break;
            case ChooseCharacter.Wizard:
                
                myPlayer[0].transform.position = myPlayer[1].transform.position;
                UserUIControl[0].SetBool("WarrierChoose", false);
                UserUIControl[1].SetBool("WizardChoose", true);
                break;
        }
    }
}
