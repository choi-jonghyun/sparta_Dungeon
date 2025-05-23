using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Consumable  //소비아이템
    
}

//소비아이템 종류
public enum ConsumableType
{
    Hunger, //배고픔
    Health, //체력
    Speed   //속도
}

//소비아이템 효과 클래스
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; //효과 종류
    public float value;         //효과 수치
    public float duration;      //지속시간
}
//아이템을 ScriptableObject로 정의
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;  //아이템 이름
    public string description;  //아이템 설명
    public ItemType type;       //아이템 종류
    public Sprite icon;         //아이템 아이콘
    public GameObject dropPrefab;   //드랍시 프리팹

    [Header("Stacking")]
    public bool canStack;       //겹칠수 있는지
    public int maxStackAmount;  //최대 겹치는 수량

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;    //소비아이템 배열


}
