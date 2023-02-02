using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardQSkill : MonoBehaviour
{
    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 1.8f)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            Debug.Log("È°¼ºÈ­µÊ");
        }
    }
}
