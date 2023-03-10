using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWarpSystem : MonoBehaviour
{
    [SerializeField] Transform[] warpPoint;
    //0:�� 1:�� 2:�� 3:��

    public void WarpPoint(int num)
    {
        SceneData.Inst.OnUI = false;
        SceneData.Inst.UIOn();
        SceneLoaded.inst.WarpPoint(num);
        SceneLoaded.inst.isWarp = true;
        LoadingSceneController.LoadScene();
    }
}
