using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoAim : MonoBehaviour
{
    public UnityEvent<Transform> FindTarget = default; //delegate와 비슷한 형식.
    public UnityEvent LostTarget = default;
    public LayerMask enemyMask = default;
    public Transform myTarget = null;

    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((enemyMask & 1 << other.gameObject.layer) != 0) //타겟이 있음
        {
            //타겟을 처음 발견했을때
            myTarget = other.transform;
            FindTarget?.Invoke(myTarget);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (myTarget == other.transform) //타겟이 빠져나감
        {
            myTarget = null; //타겟이 없음
            LostTarget?.Invoke();
        }
    }
}
