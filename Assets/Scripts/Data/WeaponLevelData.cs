using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponLevel Data", menuName = "Scriptable Object/WeaponLevel Data", order = 1)]

public class WeaponLevelData : ScriptableObject
{
    [SerializeField] float[] WeaponAp; //무기 공격력
    public float[] WeaponAP
    {
        get => WeaponAp;
    }
    [SerializeField] float[] Criticalpercent;
    public float[] CriticalPercent
    {
        get => Criticalpercent;
    }
    [SerializeField] float[] CriticalAp;
    public float[] CriticalAP
    {
        get => CriticalAp;
    }

}
