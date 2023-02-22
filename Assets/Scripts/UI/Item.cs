using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //������ �̸�
    public int itemCode; //������ �ڵ�
    public ItemType itemType; //������ ����
    public Sprite itemImage; //������ �̹���
    public Sprite upgradeItemImage; //���׷��̵� �ʿ��� ������ �̹���.
    public GameObject itemPrefab; //�������� ������.
    public string Explain; //������ ����
    public int MakeItemCount; //���׷��̵� �ʿ䰹��
    public int UpgradeItemCode; //���׷��̵忡 �ʿ��� �������ڵ�

    public enum ItemType
    {
        Equipment, Used, Ingredient, ETC, Upgrade //��� �Ҹ�ǰ ��� ��Ÿ������ �ռ�������
    }

}
