using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject orgGolem;
    [SerializeField] GameObject orgTreant;
    [SerializeField] GameObject orgBat;
    List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(list.Count < 3)
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-10.0f, 10.0f);
            pos.y = 0.5f;
            pos.z = Random.Range(-10.0f, 10.0f);
            Vector3 rot = Vector3.zero;
            rot.y = Random.Range(0.0f, 360.0f);
            GameObject objGol = Instantiate(orgGolem, pos, Quaternion.Euler(rot));
            GameObject objTre = Instantiate(orgTreant, pos, Quaternion.Euler(rot));
            GameObject orgBa = Instantiate(orgBat, pos, Quaternion.Euler(rot));
            list.Add(objTre);
            list.Add(objGol);
            list.Add(orgBa);
        }

        for(int i  = 0; i < list.Count;)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i);
                continue;
            }
            ++i;
        }
    }
}
