using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 10.0f;
    float AP = 50.0f;
    public LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            other.GetComponent<IBattle>().OnDamage(AP);
            Destroy(gameObject);
        }
    }
}
