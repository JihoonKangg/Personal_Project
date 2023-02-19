using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName; //아이템의 이름.
    public string HPpart; //체력부위.
    public float[] num; //수치.
}

public class ItemEffectDatabase : MonoBehaviour
{
    private ItemEffect[] itemEffects;

    //필요한 컴포넌트
    private CharacterDATAUSE characterHP;

    public void UseItem(Item _item)
    {
        if(_item.itemType == Item.ItemType.Used)
        {
            for(int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {
                    characterHP.curHP += (characterHP.HP * 0.2f); //20% 체력회복
                }
            }
        }
    }
}
