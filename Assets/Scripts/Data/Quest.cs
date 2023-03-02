using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestName", menuName = "Scriptable Object/Quest Data", order = 1)]
public class Quest : ScriptableObject
{
    [SerializeField] string questName; //����Ʈ �̸�(����)
    public string QuestName { get { return questName; } }

    
}
