using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIchecker : MonoBehaviour
{
    //필요한 컴포넌트
    public GameObject go_UiBase;
    public static bool UIActivated = false;

    protected void OpenUI()
    {
        go_UiBase.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void CloseUI()
    {
        go_UiBase.SetActive(false);
        Time.timeScale = 1.0f;
        if (!UIActivated) return;
        UIActivated = !UIActivated;
    }
}
