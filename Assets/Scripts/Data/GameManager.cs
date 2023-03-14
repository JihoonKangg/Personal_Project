using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;

    private void Awake()
    {
        SceneData.Inst.GameOverUI.SetActive(false);
    }
    private void FixedUpdate()
    {
        GameOver();
    }

    public void GameOver()
    {
        if (SceneData.Inst.warrior.IsDead && SceneData.Inst.wizard.IsDead)
        {
            gameOver = true;
            if (gameOver)
            {
                SceneData.Inst.warrior.IsDead = false;
                SceneData.Inst.wizard.IsDead = false;
                Debug.Log("게임종료");
                StartCoroutine(gameexit());
                return;
            }

        }
    }

    IEnumerator gameexit()
    {
        yield return new WaitForSeconds(1.0f);
        //게임종료 이미지 호출
        Time.timeScale = 0.2f;
        SceneData.Inst.GameOverUI.SetActive(true);
        SceneData.Inst.GameOverUI.GetComponent<Animator>().SetTrigger("GameOver");

        yield return new WaitForSeconds(2.0f);
        LoadingSceneController.LoadScene("TitleScene");
    }
}
