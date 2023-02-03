using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardQSkill : MonoBehaviour
{
    public LayerMask enemyMask;
    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //Q��ų(�ٴ� ��) Ȱ��ȭ
        if(time > 1.4f)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0) //�鿡 ���Ͱ� ����� ��
        {
            
        }
    }
}
