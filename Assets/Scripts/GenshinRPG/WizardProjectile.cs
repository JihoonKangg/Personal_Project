using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{
    [SerializeField] float speed = 20.0f;
    public LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            other.GetComponent<IBattle>().OnDamage(SceneData.Inst.wizard.ChaEAP);
            Destroy(gameObject);
        }
    }
}
