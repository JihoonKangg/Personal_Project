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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TitleScene.inst.theSaveLoad = FindObjectOfType<SaveLoad>();
        TitleScene.inst.theSaveLoad.LoadData();
        SceneData.Inst.LoadSet();
        Destroy(TitleScene.inst.gameObject);
    }
}
