using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestIntroduce : MonoBehaviour
{
    public Quest quest;
    [SerializeField] TMP_Text QuestName;
    [SerializeField] TMP_Text QuestSubName;
    [SerializeField] TMP_Text QuestExplain;
    [SerializeField] TMP_Text NeedCount;
    public int needNum;
    

    public void QuestCheck()
    {
        if(quest == null) return;

        QuestName.text = quest.QuestName;
        QuestSubName.text = quest.QuestSubName;
        QuestExplain.text = quest.Explain;
        NeedCount.text = "(" + needNum.ToString() + "/" + quest.NeedCount + ")";
    }

    void Update()
    {
        if (quest == null) return;

        NeedCount.text = "(" + needNum.ToString() + "/" + quest.NeedCount + ")";
    }
    public void QuestSuccess()
    {
        quest = null;
        QuestName.text = "";
        QuestSubName.text = "";
        QuestExplain.text = "";
        NeedCount.text = "";
    }
}
