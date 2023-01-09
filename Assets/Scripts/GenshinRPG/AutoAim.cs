using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoAim : MonoBehaviour
{
    public UnityEvent<Transform> FindTarget = default; //delegate�� ����� ����.
    public UnityEvent LostTarget = default;
    public LayerMask enemyMask = default;
    public Transform myTarget = null;

    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((enemyMask & 1 << other.gameObject.layer) != 0) //Ÿ���� ����
        {
            //Ÿ���� ó�� �߰�������
            myTarget = other.transform;
            FindTarget?.Invoke(myTarget);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (myTarget == other.transform) //Ÿ���� ��������
        {
            myTarget = null; //Ÿ���� ����
            LostTarget?.Invoke();
        }
    }
}
