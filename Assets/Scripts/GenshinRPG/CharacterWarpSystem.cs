using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWarpSystem : MonoBehaviour
{
    [SerializeField] GameObject LoadingUIobj;
    [SerializeField] Slider LoadingBar;
    [SerializeField] Transform[] Warppoint;
    [SerializeField] string[] Tiptext;
    [SerializeField] TMP_Text TIP;

    public void PlayerWarp(int num)
    {
        StartCoroutine(CharacterWarpLoading(num));
    }

    IEnumerator CharacterWarpLoading(int num)
    {
        int i = Random.Range(0, 10);
        TIP.text = Tiptext[i];
        SceneData.Inst.MapUI.CloseUI();
        LoadingUIobj.SetActive(true);
        Time.timeScale = 0.0f;

        while (LoadingBar.value <= 0.999)
        {
            LoadingBar.value += 0.01f;
            yield return null;
        }

        Time.timeScale = 1.0f;
        LoadingBar.value = 0.0f;
        transform.position = Warppoint[num].position;
        SceneData.Inst.warrior.transform.position = Warppoint[num].position;
        SceneData.Inst.wizard.transform.position = Warppoint[num].position;
        LoadingUIobj.SetActive(false);
    }
}
