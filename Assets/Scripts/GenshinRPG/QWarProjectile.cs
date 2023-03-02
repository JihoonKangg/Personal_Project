using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QWarProjectile : MonoBehaviour
{
    public LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            other.GetComponent<IBattle>().OnDamage(SceneData.Inst.warreir.ChaEAP);
        }
    }
}
