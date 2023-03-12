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
        if (Item.itemType == Item.ItemType.Recovery) //ȸ�� �������� ���
        {
            UseButton.SetActive(true);
        }
        else UseButton.SetActive(false);
    }

    public void UseItemClick() //ȸ�� ������ ��ư�� ����ϱ� ������ ��
    {
        for (int i = 0; i < SceneData.Inst.myinven.slots.Length; i++)
        {
            if (SceneData.Inst.myinven.slots[i].item.itemCode == Item.itemCode) //���Կ� �ִ� ������������ ��ȸ
            {
                if (Item.itemCode == 1) //ü���� 10%�� ȸ������
                {
                    SceneData.Inst.warrior.curHP += SceneData.Inst.warrior.HP / 10.0f;
                    SceneData.Inst.wizard.curHP += SceneData.Inst.wizard.HP / 10.0f;
                    SceneData.Inst.myinven.slots[i].Useitem(Item, 1);
                }
                else if (Item.itemCode == 4) //ü���� 20%�� ȸ������
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
