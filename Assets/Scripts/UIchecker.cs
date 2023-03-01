using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class UIchecker : MonoBehaviour
{
    //필요한 컴포넌트
    public GameObject go_UiBase;
    public static bool UIActivated = false;

    protected void OpenUI()
    {
        go_UiBase.SetActive(true);
    }

    public void CloseUI()
    {
        go_UiBase.SetActive(false);
        SceneData.Inst.OnUI = false;
        SceneData.Inst.UIOn();
        if (!UIActivated) return;
        UIActivated = !UIActivated;
    }
}
