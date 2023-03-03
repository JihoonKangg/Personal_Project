using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class QuestController : UIchecker
{
    public QuestSlot[] slots;
    [SerializeField]
    private GameObject go_SlotsParent;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<QuestSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenQuest();
    }

    private void TryOpenQuest()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            UIActivated = !UIActivated;
            if (UIActivated)
            {
                OpenUI();
            }
            else
            {
                CloseUI();
            }
            SceneData.Inst.OnUI = UIActivated;
            SceneData.Inst.UIOn();
        }
    }

    public void AcquireQuest(Quest _quest)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].quest == null)
            {
                slots[i].Addquest(_quest);
                return;
            }
        }
    }
}
