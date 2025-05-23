using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Consumable  //�Һ������
    
}

//�Һ������ ����
public enum ConsumableType
{
    Hunger, //�����
    Health, //ü��
    Speed   //�ӵ�
}

//�Һ������ ȿ�� Ŭ����
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; //ȿ�� ����
    public float value;         //ȿ�� ��ġ
    public float duration;      //���ӽð�
}
//�������� ScriptableObject�� ����
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;  //������ �̸�
    public string description;  //������ ����
    public ItemType type;       //������ ����
    public Sprite icon;         //������ ������
    public GameObject dropPrefab;   //����� ������

    [Header("Stacking")]
    public bool canStack;       //��ĥ�� �ִ���
    public int maxStackAmount;  //�ִ� ��ġ�� ����

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;    //�Һ������ �迭


}
