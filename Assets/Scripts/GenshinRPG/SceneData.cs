using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData Inst = null;
    public Warrier warrior;
    public Wizard wizard;
    public GameObject Player;
    public TMP_Text actionText;
    public Inventory myinven;
    public QuestController myquest;
    public GameObject[] ActionUI;
    public GameObject Synthesis;
    public PlayerLevel PlayerLevel;
    public int WorldLevel;
    public GameObject ExpSlider;
    public Transform[] warpPoint;
    public Animator CantChangeMessage;
    public Map MapUI;
    public GameObject QuestUI;
    public GameObject GameOverUI;
    public Quest1 quest1;
    public Quest2 quest2;

    public float SavewarcurHP;
    public float SavewizcurHP;
    public int Saveexp;
    public int Savewarlevel;
    public int Savewizlevel;

    public bool NPC_Talking = false;
    public bool OnUI;

    private void Awake()
    {
        Inst = this;
    }
    public void UIOn()
    {
        ActionUI[0].SetActive(!OnUI);
        ActionUI[1].SetActive(!OnUI);
        ActionUI[2].SetActive(!OnUI);
        ActionUI[3].SetActive(!OnUI); 
        ActionUI[4].SetActive(!OnUI);
        if (OnUI)
        {
            Time.timeScale = 0.0f;
        }
        else 
        {
            Time.timeScale = 1.0f;
        }
    }

    public void LoadSet()
    {
        //���巹�� ���忡 �°� ������Ʈ
        warrior.CharacterLevelUP();
        wizard.CharacterLevelUP();

        //����� hp ������Ʈ
        warrior.curHP = SavewarcurHP;
        wizard.curHP = SavewizcurHP;
        warrior.Hpupdate();
        wizard.Hpupdate();

        //����ġ, �������̽� ������Ʈ
        PlayerLevel.LevelSet();
        PlayerLevel.EXP = Saveexp;
        PlayerLevel.ExpUpdate();
        ExpSlider.GetComponent<Animator>().SetTrigger("Show");

        //����� ���ⷹ�� ������Ʈ
        warrior.W_LEVEL = Savewarlevel;
        warrior.WeaponLevelSet();
        wizard.W_LEVEL = Savewizlevel;
        wizard.WeaponLevelSet();
    }
}
