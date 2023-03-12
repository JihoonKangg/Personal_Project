using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class ItemIntroduce : MonoBehaviour
{
    public Item Item;
    [SerializeField]
    private TMP_Text ItemName;
    [SerializeField]
    private TMP_Text ItemExplain;
    [SerializeField]
    private Image ItemIMG;
    [SerializeField]
    private GameObject UseButton;

    void Start()
    {
        UseButton.SetActive(false);
    }
    public void ItemCheck()
    {
        if (Item == null)
        {
            UseButton.SetActive(false);
            return;
        }

        PottionCheck();
        ItemIMG.sprite = Item.itemImage;
        ItemName.text = Item.itemName;
        ItemExplain.text = Item.Explain;
    }

    public void PottionCheck()
    {
        if (Item.itemType == Item.ItemType.Recovery) //회복 아이템인 경우
        {
            UseButton.SetActive(true);
        }
        else UseButton.SetActive(false);
    }

    public void UseItemClick() //회복 아이템 버튼을 사용하기 눌렀을 때
    {
        for (int i = 0; i < SceneData.Inst.myinven.slots.Length; i++)
        {
            if (SceneData.Inst.myinven.slots[i].item.itemCode == Item.itemCode) //슬롯에 있는 아이템정보를 조회
            {
                if (Item.itemCode == 1) //체력의 10%를 회복해줌
                {
                    SceneData.Inst.warrior.curHP += SceneData.Inst.warrior.HP / 10.0f;
                    SceneData.Inst.wizard.curHP += SceneData.Inst.wizard.HP / 10.0f;
                    SceneData.Inst.myinven.slots[i].Useitem(Item, 1);
                }
                else if (Item.itemCode == 4) //체력의 20%를 회복해줌
                {
                    SceneData.Inst.warrior.curHP += SceneData.Inst.warrior.HP / 5.0f;
                    SceneData.Inst.wizard.curHP += SceneData.Inst.wizard.HP / 5.0f;
                    SceneData.Inst.myinven.slots[i].Useitem(Item, 1);
                }
            }
        }
        SceneData.Inst.warrior.Hpupdate();
        SceneData.Inst.wizard.Hpupdate();
    }
}
