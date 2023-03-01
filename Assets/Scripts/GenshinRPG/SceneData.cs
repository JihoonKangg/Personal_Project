using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData Inst = null;
    public GameObject Player;
    public TMP_Text actionText;
    public Inventory myinven;
    public GameObject[] ActionUI;
    public GameObject Synthesis;
    public PlayerLevel PlayerLevel;
    public int WorldLevel;
    public GameObject ExpSlider;

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
        if (OnUI) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }
}
