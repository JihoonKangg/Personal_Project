using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWarpSystem : MonoBehaviour
{
    [SerializeField] Transform[] WarpPoint;
    //0:µ¿ 1:¼­ 2:³² 3:ºÏ

    public void WarpPoint_E()
    {
        //LoadingSceneController.LoadScene("MainScene");
        transform.position = WarpPoint[0].position;
        transform.GetChild(0).transform.position = transform.position;
        transform.GetChild(1).transform.position = transform.position;
    }
}
