using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : Spawner
{
    void Update()
    {
        MonsterSpawn(orgBat);
    }
}
