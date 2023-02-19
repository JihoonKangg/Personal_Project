using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName; //�������� �̸�.
    public string HPpart; //ü�º���.
    public float[] num; //��ġ.
}

public class ItemEffectDatabase : MonoBehaviour
{
    private ItemEffect[] itemEffects;

    //�ʿ��� ������Ʈ
    private CharacterDATAUSE characterHP;

    public void UseItem(Item _item)
    {
        if(_item.itemType == Item.ItemType.Used)
        {
            for(int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {
                    characterHP.curHP += (characterHP.HP * 0.2f); //20% ü��ȸ��
                }
            }
        }
    }
}
