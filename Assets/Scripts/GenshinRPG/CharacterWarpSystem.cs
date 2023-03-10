using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWarpSystem : MonoBehaviour
{
    [SerializeField] Transform[] warpPoint;
    //0:µ¿ 1:¼­ 2:³² 3:ºÏ

    public void WarpPoint(int num)
    {
        SceneData.Inst.OnUI = false;
        SceneData.Inst.UIOn();
        SceneLoaded.inst.WarpPoint(num);
        SceneLoaded.inst.isWarp = true;
        LoadingSceneController.LoadScene();
    }
}
