using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawner : Spawner
{
    void Update()
    {
        MonsterSpawn(orgGolem);
    }
}
