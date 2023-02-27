using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EXP Data", menuName = "Scriptable Object/EXP Data", order = 2)]
public class ExpSystemData : ScriptableObject
{
    [SerializeField] float[] exp; //°æÇèÄ¡
    public float[] EXP { get => exp; }
}
