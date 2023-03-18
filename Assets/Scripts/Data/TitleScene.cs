using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public static TitleScene inst;
    public SaveLoad theSaveLoad;
    public bool saveLoading = false;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);
    }

    public void ClickStart()
    {
        LoadingSceneController.LoadScene("PlayScene");
    }

    public void ClickLoad()
    {
        Debug.Log("�ε�");
        LoadingSceneController.LoadScene("PlayScene");
        saveLoading = true;
        
    }

    public void ClickContinue()
    {
        Debug.Log("����ϱ�");
    }

    public void ClickExit()
    {
        Debug.Log("��������");
        Application.Quit(); //������ �󿡴� ������� ����.
    }
}
