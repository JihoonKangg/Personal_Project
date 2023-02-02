using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExpAiPerception : MonoBehaviour
{
    public UnityEvent<Transform> FindTarget = default;
    public LayerMask myPlayer = default;
    public Transform myTarget = null;

    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((myPlayer & 1 << other.gameObject.layer) != 0) //Ÿ���� ����
        {
            //Ÿ���� ó�� �߰�������
            myTarget = other.transform;
            FindTarget?.Invoke(myTarget);
        }
    }
}
