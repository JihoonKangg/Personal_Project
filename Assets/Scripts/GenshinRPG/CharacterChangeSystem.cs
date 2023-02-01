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
        Warrior, //����
        Wizard  //������
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
                UserUIControl[0].SetBool("WarrierChoose", true);
                UserUIControl[1].SetBool("WizardChoose", false);
                break;
            case ChooseCharacter.Wizard:
                myPlayer[0].SetActive(false);
                myPlayerSkillUI[0].SetActive(false);
                myPlayer[1].SetActive(true);
                myPlayerSkillUI[1].SetActive(true);
                UserUIControl[0].SetBool("WarrierChoose", false);
                UserUIControl[1].SetBool("WizardChoose", true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacter(myCharacter);
        UserUIControl[0].SetBool("WarrierChoose", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCharacter(ChooseCharacter.Warrior);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter(ChooseCharacter.Wizard);
        }
    }
}
