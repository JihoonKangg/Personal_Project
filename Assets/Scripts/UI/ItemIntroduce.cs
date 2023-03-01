using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public void ItemCheck()
    {
        if (Item == null) return;

        ItemIMG.sprite = Item.itemImage;
        ItemName.text = Item.itemName;
        ItemExplain.text = Item.Explain;
    }
}
