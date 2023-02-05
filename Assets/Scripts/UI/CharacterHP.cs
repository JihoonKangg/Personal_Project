using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    public Slider HpBar;
    public TMPro.TMP_Text MyHp;
    public TMPro.TMP_Text MaxHp;
    float MaxHP;
    float MyHP;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = GetComponentInChildren<CharacterMovement>().orgData.HP;
        MyHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        MaxHP = GetComponentInChildren<CharacterMovement>().orgData.HP;
        MyHP = GetComponentInChildren<CharacterMovement>().curHP;

        HpBar.value = MyHP / MaxHP;
        MyHp.text = MyHP.ToString();
        MaxHp.text = MaxHP.ToString();
    }
}
