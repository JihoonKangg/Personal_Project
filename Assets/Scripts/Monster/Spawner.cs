using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject orgGolem;
    [SerializeField] protected GameObject orgTreant;
    [SerializeField] protected GameObject orgBat;
    protected List<GameObject> list = new List<GameObject>();


    public void MonsterSpawn(GameObject monster)
    {
        if (list.Count < 3)
        {
            Vector3 pos = transform.position;
            pos.x = Random.Range(-10.0f, 10.0f);
            pos.y = 0.5f;
            pos.z = Random.Range(-10.0f, 10.0f);
            Vector3 rot = Vector3.zero;
            rot.y = Random.Range(0.0f, 360.0f);
            monster = Instantiate(monster, transform.position + pos, Quaternion.Euler(rot));
            list.Add(monster);
        }

        for (int i = 0; i < list.Count;)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i);
                continue;
            }
            ++i;
            Debug.Log("몇번 체크함 ?");
        }
    }

    protected IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(10.0f);
    }
}
