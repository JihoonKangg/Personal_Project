using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaded : MonoBehaviour
{
    public static SceneLoaded inst;
    Vector3 playerPosition = new Vector3() { };

    public int warpNum = 0;
    public bool isWarp = false;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);

    }
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
        if(!isWarp)
        {
            playerPosition = SceneData.Inst.Player.transform.position;
            isWarp = !isWarp;
        }
        
        if(SceneData.Inst.Player != null)
        SceneData.Inst.Player.transform.position = playerPosition;
    }

    public void WarpPoint(int num)
    {
        playerPosition = SceneData.Inst.warpPoint[num].position;
    }
}
