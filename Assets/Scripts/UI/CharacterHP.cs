using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHP : CharacterProperty
{
    public Slider HpBar;
    public Warrier myCharacter;
    public TMPro.TMP_Text MyHp;
    public TMPro.TMP_Text MaxHp;

    float MaxHP;
    float MyHP;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = myCharacter.myStat.MaxHP;
        MyHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        MyHP = myCharacter.myStat.HP;
        HpBar.value = MyHP / MaxHP;

        MyHp.text = MyHP.ToString();
        MaxHp.text = MaxHP.ToString();
    }
}