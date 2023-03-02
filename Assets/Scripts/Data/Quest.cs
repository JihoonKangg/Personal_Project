using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestName", menuName = "Scriptable Object/Quest Data", order = 1)]
public class Quest : ScriptableObject
{
    [SerializeField] string questName; //����Ʈ �̸�(����)
    public string QuestName { get { return questName; } }

    [SerializeField] string questSubName; //����Ʈ �̸�(����)
    public string QuestSubName { get { return questSubName; } }

    [SerializeField] int questCode; //����Ʈ �ڵ�
    public int QuestCode { get { return questCode; } }

    [SerializeField] string explain; //����Ʈ ����
    public string Explain { get { return explain; } }

    [SerializeField] int needItemCode; //�ʿ��� ������ �ڵ�
    public int NeedItemCode { get { return needItemCode; } }

    [SerializeField] int needCount; //�ʿ� ����(���� ���� ��/������ �� ��)
    public int NeedCount { get { return needCount; } }
}
