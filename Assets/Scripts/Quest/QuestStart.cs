using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStart : MonoBehaviour
{
    public Quest quest;
    protected bool pickupActivated = false; //���� ������ �� true
    public bool Success = false;

    [SerializeField]
    protected LayerMask layerMask; //������ ���̾ �ߵ�
    protected Transform myTarget;
    protected Dialogue dia = null;


    protected void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((layerMask & 1 << other.gameObject.layer) != 0)
        {
            myTarget = other.transform;
            pickupActivated = true;
            SceneData.Inst.actionText.gameObject.SetActive(true);
            SceneData.Inst.actionText.text = "NPC�� ��ȭ " + "<color=yellow>" + "(F)" + "</color>";
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (myTarget == other.transform)//Ÿ���� ��������
        {
            Debug.Log("��ȭ �Ұ�");
            myTarget = null;
            pickupActivated = false;
            SceneData.Inst.actionText.gameObject.SetActive(false);
        }
    }

    protected IEnumerator RotateTarget(Collider other)
    {
        Vector3 dir = other.transform.position - transform.position;
        float delta = 360.0f * Time.deltaTime;
        dir.y = 0.0f;  //������ ��ġ�� ���� �ڵ�
        dir.Normalize(); //������ ��ġ�� ���� �ڵ�

        float Angle = Vector3.Angle(dir, transform.forward);
        float rotDir = 1.0f;

        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -rotDir;
        }

        while (Angle > 0.0f)
        {
            if (delta > Angle)
            {
                delta = Angle;
            }
            Angle -= delta;
            transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            yield return null;
        }
    }
}
