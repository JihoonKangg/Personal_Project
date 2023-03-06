using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData : MonoBehaviour
{
    public static SceneData Inst = null;
    public Warrier warreir;
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
        if (OnUI) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }
}
