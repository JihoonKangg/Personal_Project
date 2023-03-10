using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    [SerializeField] Slider HpBar;
    [SerializeField] TMPro.TMP_Text MyHp;
    [SerializeField] TMPro.TMP_Text MaxHp;
    float MaxHP;
    float MyHP;


    // Update is called once per frame
    void Update()
    {
        MyHP = GetComponentInChildren<CharacterMovement>().curHP;
        MaxHP = GetComponentInChildren<CharacterMovement>().HP;

        HpBar.value = MyHP / MaxHP;
        MyHp.text = MyHP.ToString();
        MaxHp.text = MaxHP.ToString();
    }
}
