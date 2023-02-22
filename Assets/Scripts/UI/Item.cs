using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //아이템 이름
    public int itemCode; //아이템 코드
    public ItemType itemType; //아이템 유형
    public Sprite itemImage; //아이템 이미지
    public Sprite upgradeItemImage; //업그레이드 필요한 아이템 이미지.
    public GameObject itemPrefab; //아이템의 프리팹.
    public string Explain; //아이템 설명
    public int MakeItemCount; //업그레이드 필요갯수
    public int UpgradeItemCode; //업그레이드에 필요한 아이템코드

    public enum ItemType
    {
        Equipment, Used, Ingredient, ETC, Upgrade //장비 소모품 재료 기타아이템 합성아이템
    }

}
