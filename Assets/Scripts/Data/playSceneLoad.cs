using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playSceneLoad : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //Start보다 먼저 시작함
    {
        if (TitleScene.inst == null) return;

        if(TitleScene.inst.saveLoading)
        {
            TitleScene.inst.theSaveLoad = FindObjectOfType<SaveLoad>();
            TitleScene.inst.theSaveLoad.LoadData();
            SceneData.Inst.LoadSet();
            TitleScene.inst.saveLoading = false;
        }
        Destroy(TitleScene.inst.gameObject);
    }
}
