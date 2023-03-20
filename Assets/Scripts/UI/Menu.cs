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
        Debug.Log("�����");
        SaveLoad.SaveData(); //�÷��̾� ��ġ�� ��ϵ�.
    }

    public void ClickContinue()
    {
        Debug.Log("����ϱ�");
        CloseUI();
    }

    public void ClickExit()
    {
        Debug.Log("��������");
        Application.Quit();
    }
}
