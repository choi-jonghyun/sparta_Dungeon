using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item; //슬롯안에 아이템데이터

    public Button button;   //슬롯 버튼
    public Image icon;      //슬롯 이미지
    public TextMeshProUGUI quantityText;    //수량텍스트 2개 이상일때
    private Outline outline;    // 슬롯 외곽선(선택 장착표시)

    public UIInventory inventory;   //슬롯 인벤토리ui

    public int index;
    public int quantity;    //수량

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

 

    public void Set()
    {
        icon.gameObject.SetActive(true); //아이콘보이기
        icon.sprite = item.icon;    //아이콘 이미지 설정
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty; //수량표시

      
    }

    public void Clear() //슬롯 비우기
    {
        item = null;
        icon.gameObject.SetActive(false); //아이콘 숨기기
        quantityText.text = string.Empty; //수량텍스트 제거
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}

