using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class QuestSlot : MonoBehaviour
{
    public Quest quest;
    [SerializeField]
    public TMP_Text QuestName;
    [SerializeField] QuestIntroduce QuestIntroduce;
    [SerializeField] Image questimg;


    //���� �ʱ�ȭ.
    private void ClearSlot()
    {
        quest = null;
        QuestName.text = "";
        SetColor(0);
    }

    public void QuestClick()
    {
        QuestIntroduce.quest = quest;
        QuestIntroduce.QuestCheck();
    }

    //������ ȹ��.
    public void Addquest(Quest _quest)
    {
        quest = _quest;
        QuestName.text = quest.QuestName;
        SetColor(1);
    }

    //�̹����� ���� ����.
    private void SetColor(float _alpha)
    {
        Color color = questimg.color;
        color.a = _alpha;
        questimg.color = color;
    }

    public void QuestSuccess()
    {
        ClearSlot();
    }
}
