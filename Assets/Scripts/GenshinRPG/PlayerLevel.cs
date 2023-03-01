using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] ExpSystemData expData;
    [SerializeField] Warrier warrier;
    [SerializeField] Wizard wizard;
    [SerializeField] TMP_Text Level_Text;
    [SerializeField] Slider ExpSlider;
    [SerializeField] TMP_Text[] Exp_Text;
    //0: EXP   1: MaxEXP

    public int EXP;
    bool LevelUpEvent;

    void Start()
    {
        LevelSet();
        ExpUpdate();
    }

    void Update()
    {
        if(EXP >= expData.EXP[SceneData.Inst.WorldLevel - 1]) //레벨 업 조건 달성
        {
            LevelUpEvent = true;
            LevelUp();
        }
        ExpUpdate();
    }

    public void LevelUp() //월드레벨업
    {
        if (SceneData.Inst.WorldLevel == 10) return;

        Debug.Log("레벨업");
        SceneData.Inst.WorldLevel++;

        LevelSet();
        warrier.CharacterLevelUP();
        wizard.CharacterLevelUP();
    }

    private void LevelSet()
    {
        Level_Text.text = "Lv. " + SceneData.Inst.WorldLevel.ToString();
        LevelUpEvent = false;
        EXP = 0;
        Exp_Text[1].text = expData.EXP[SceneData.Inst.WorldLevel - 1].ToString();
    }

    private void ExpUpdate()
    {
        Exp_Text[0].text = EXP.ToString() + "  /";
        ExpSlider.value = (float)EXP / (float)expData.EXP[SceneData.Inst.WorldLevel - 1];
    }

    public void LevelUPtest()
    {
        SceneData.Inst.ExpSlider.GetComponent<Animator>().SetTrigger("Show");
        EXP += 30;
    }
}
