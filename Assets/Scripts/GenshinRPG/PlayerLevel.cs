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
    [SerializeField] TMP_Text[] Level_Text;
    [SerializeField] Slider ExpSlider;
    [SerializeField] TMP_Text[] Exp_Text;
    //0: EXP   1: MaxEXP

    public int EXP;
    bool LevelUpEvent;


    private void Awake() //Start에 있는경우 저장된 데이터를 덮어씌우는 현상 발생.
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

    public void LevelSet()
    {
        Level_Text[0].text = "Lv. " + SceneData.Inst.WorldLevel.ToString();
        Level_Text[1].text = Level_Text[0].text;
        LevelUpEvent = false;
        EXP = 0;
        Exp_Text[1].text = expData.EXP[SceneData.Inst.WorldLevel - 1].ToString();
    }

    public void ExpUpdate()
    {
        Exp_Text[0].text = EXP.ToString() + "  /";
        ExpSlider.value = (float)EXP / (float)expData.EXP[SceneData.Inst.WorldLevel - 1];
    }
}
