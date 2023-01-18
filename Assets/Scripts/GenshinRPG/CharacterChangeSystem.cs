using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeSystem : MonoBehaviour
{
    public GameObject[] myPlayer;

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
                myPlayer[1].SetActive(false);
                break;
            case ChooseCharacter.Wizard:
                myPlayer[0].SetActive(false);
                myPlayer[1].SetActive(true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacter(myCharacter);
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
