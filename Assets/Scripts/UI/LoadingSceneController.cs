using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    [SerializeField] Slider LoadSlider;

    public static void LoadScene(string scenename)
    {
        nextScene = scenename;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewSceneProcess());
    }

    IEnumerator NewSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false; 
        //���� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��Ұ����� �����ϴ°�.

        float timer = 0.0f;
        while(!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                LoadSlider.value = op.progress;
            }

            else
            {
                timer += Time.unscaledDeltaTime;
                LoadSlider.value = Mathf.Lerp(0.9f, 1f, timer);
                if (LoadSlider.value >= 1.0f)
                {
                    op.allowSceneActivation = true;
                }
            }
        }
    }
}
