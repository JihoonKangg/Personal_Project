using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : UIchecker
{
    void Update()
    {
        TryOpenQuest();
    }

    private void TryOpenQuest()
    {
        if (Input.GetKeyDown(KeyCode.M))
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
}
