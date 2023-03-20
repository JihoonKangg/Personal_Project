using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : UIchecker
{
    [SerializeField] private SaveLoad SaveLoad;


    void Update()
    {
        TryOpenMenu();
    }

    private void TryOpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIActivated = !UIActivated;
            if (UIActivated)
            {
                OpenUI();
            }
            else
            {
                if (!go_UiBase.activeSelf) return;
                CloseUI();
            }
            SceneData.Inst.OnUI = UIActivated;
            SceneData.Inst.UIOn();
        }
    }

    public void ClickSave()
    {
        Debug.Log("저장됨");
        SaveLoad.SaveData(); //플레이어 위치가 기록됨.
    }

    public void ClickContinue()
    {
        Debug.Log("계속하기");
        CloseUI();
    }

    public void ClickExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}
