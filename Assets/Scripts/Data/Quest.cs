using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestName", menuName = "Scriptable Object/Quest Data", order = 1)]
public class Quest : ScriptableObject
{
    [SerializeField] string questName; //퀘스트 이름(메인)
    public string QuestName { get { return questName; } }

    [SerializeField] string questSubName; //퀘스트 이름(서브)
    public string QuestSubName { get { return questSubName; } }

    [SerializeField] int questCode; //퀘스트 코드
    public int QuestCode { get { return questCode; } }

    [SerializeField] int[] needMonsterCode; //퀘스트 설명
    public int[] NeedMonsterCode { get { return needMonsterCode; } }

    [SerializeField] string explain; //퀘스트 설명
    public string Explain { get { return explain; } }

    [SerializeField] int needItemCode; //필요한 아이템 코드
    public int NeedItemCode { get { return needItemCode; } }

    [SerializeField] int needCount; //필요 갯수(몬스터 잡을 때/아이템 줄 때)
    public int NeedCount { get { return needCount; } }

    public bool Success = false;

    public QuestType questType; //퀘스트 유형

    public enum QuestType
    {
        Hunt, Item//사냥, 아이템
    }
}
