using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : CharacterProperty
{
    [SerializeField] Slider mySlider;
    [SerializeField] Slider mySlider_BG;
    [SerializeField] Transform myHpPos;
    [SerializeField] GameObject myHPBar;
    [SerializeField] CharacterProperty myMonster;
    //public CharacterProperty myMonster;

    //HP bar 다시 구현.

    // Start is called before the first frame update
    void Start()
    {
        mySlider.value = 1.0f;
        mySlider_BG.value = mySlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        mySlider.transform.position = Camera.main.WorldToScreenPoint(myHpPos.position);
        mySlider_BG.transform.position = mySlider.transform.position;
        mySlider.value = myMonster.myStat.HP / myMonster.myStat.MaxHP;
        mySlider_BG.value = Mathf.Lerp(mySlider_BG.value, myMonster.myStat.HP / myMonster.myStat.MaxHP, 5.0f * Time.deltaTime);

        if(mySlider.value == 0.0f)
        {
            myHPBar.SetActive(false);
        }
    }
}
