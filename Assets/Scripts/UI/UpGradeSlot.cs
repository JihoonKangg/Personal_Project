using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpGradeSlot : Slot, IPointerClickHandler
{
    public MainSlot MainSlot;
    public Image UpgradeItemImage; //���׷��̵��� ������ �̹���
    public Image UpImage; //������ �̹����� �����ϰ� �� �̹���.
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if(MainSlot.item != null)
        {
            MainSlot.AddItem(item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
