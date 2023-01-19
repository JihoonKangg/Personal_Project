using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject[] PlayerTarget;
    public GameObject[] myPlayer;
    // Update is called once per frame
    void Update()
    {
        //this.transform.position = PlayerTarget.position;
        if (myPlayer[0].activeSelf == true)
        {
            this.transform.position = PlayerTarget[0].transform.position;
        }
        else if(myPlayer[1].activeSelf == true)
        {
            this.transform.position = PlayerTarget[1].transform.position;
        }
    }
}
