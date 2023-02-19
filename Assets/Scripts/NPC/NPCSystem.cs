using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField]
    private LayerMask myPlayer;
    private Transform myTarget;
    private bool CanActivated = false; //합성대 사용 가능할 시 true


    private void CanPickUp()
    {
        if (CanActivated)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(myTarget.GetComponent<ItemPickUp>().item.itemName + " 획득했습니다");
                CanActivated = false;
                Destroy(myTarget.gameObject);
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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (myTarget == other.transform)
        {

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
        }
    }
}
