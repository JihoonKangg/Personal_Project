using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField]
    private LayerMask myPlayer;
    private Transform myTarget;
    private bool CanActivated = false; //합성대 사용 가능할 시 true
    [SerializeField]
    private Synthesis mySynthesis;

    [SerializeField]
    private TMP_Text actionText; //필요한 컴포넌트


    private void CanUse()
    {
        if (CanActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("NPC가 반응합니다");
                actionText.gameObject.SetActive(false);
                CanActivated = false;
                //mySynthesis.SetActive(true);
                mySynthesis.CanUpdate();
                myTarget = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((myPlayer & 1 << other.gameObject.layer) != 0)
        {
            myTarget = other.transform;
            CanActivated = true;
            Debug.Log("말걸 수 있음");
            actionText.gameObject.SetActive(true);
            actionText.text = "합성대" + "<color=yellow>" + "(F)" + "</color>";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform)
        {
            CanUse();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (myTarget != null) return;
        if (myTarget == other.transform)//타겟이 빠져나감
        {
            Debug.Log("못주움");
            CanActivated = false;
            myTarget = null;
            actionText.gameObject.SetActive(false);
        }
    }
}
