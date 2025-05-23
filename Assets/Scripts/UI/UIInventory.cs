using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{

    public ItemSlot[] slots;        //아이템슬롯 배열
    public GameObject inventoryWindow;  //인벤토리창
    public Transform slotPanel;     //슬롯배치 판넬
    public Transform dropPosition;  //드랍할 위치


    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;        //선택아이템 이름텍스트
    public TextMeshProUGUI selectedItemDescription; //선택아이템 설명텍스트
    public TextMeshProUGUI selectedStatName;        //선택아이템 효과이름텍스트
    public TextMeshProUGUI selectedStatValue;       //선택아이템 효과수치텍스트
    public GameObject useButton;                    //아이템 사용버튼
    public GameObject dropButton;                   //아이템 버리기버튼

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;      //선택아이템 데이터
    int selectedItemIndex = 0;  //선택아이템 인덱스

    

    private void Start()
    {
        //플레이어컨트롤러,상태 세팅
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;     //토글 연결(인벤토리 열기닫기)
        CharacterManager.Instance.Player.addItem += AddItem;    //additem에 AddItem연결(아이템획득)
        inventoryWindow.SetActive(false);       //인벤토리창 비활성화
        slots = new ItemSlot[slotPanel.childCount];     //슬롯 배열 크기설정

        //슬롯 배열 초기화
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        ClearSeletedItemWindow();       //선택아이템창 초기화
    }

    //선택아이템창 초기화
    void ClearSeletedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);      //사용버튼숨김     
        dropButton.SetActive(false);    //버리기버튼숨김
    }

    //인벤토리창 열기닫기
    public void Toggle()   
    {
        if (isOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    //인벤토리창이 열려있는지
    public bool isOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    //아이템 획득시 호출
    void AddItem() 
    {
        //플레이어가 가진 아이템데이터 가져옴
        ItemData data = CharacterManager.Instance.Player.itemData;
        //아이템을 겹칠수 있으면
        if (data.canStack)
        {
            // 같은아이템 슬롯 찾음
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;    //수량1증가
                UpdateUI();         //UI갱신
                CharacterManager.Instance.Player.itemData = null;   //플레이어 아이템 초기화
                return;
            }
        }
        //겹치지 못하거나 같은아이템이 없으면 빈슬롯 찾기
        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = data;  //빈슬롯에 아이템추가
            emptySlot.quantity = 1; //수량1로 설정
            UpdateUI();             //UI갱신
            CharacterManager.Instance.Player.itemData = null;   //플레이어아이템초기화
            return;
        }
        //슬롯이 없으면 아이템 버림
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null; //플레이어 아이템 초기화
    }

    //인벤토리UI 갱신
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set(); //슬롯에 세팅
            }
            else
            {
                slots[i].Clear();   //빈슬롯 갱신
            }
        }
    }

    //같은아이템이 있는 슬롯 찾음
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null; //없으면 null
    }

    //빈슬롯찾기
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }
    //아이템버리기
    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    //선택아이템 표시
    public void SelectItem(int index)
    {
        if (slots[index].item == null) return; //슬롯에 아이템이 없으면 무시

        selectedItem = slots[index].item;   //선택아이템 설정
        selectedItemIndex = index;          //선택아이템 저장

        selectedItemName.text = selectedItem.displayName;       //아이템 이름표시텍스트
        selectedItemDescription.text = selectedItem.description;    //아이템 설명표시텍스트

        //초기화
        selectedItemName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        //선택아이템 소비효과를 줄바꿈으로 표시
        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedItemName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }
        //소비아이템이면 사용버튼 활성
        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        dropButton.SetActive(true); //버리기버튼 활성화

    }

    //사용버튼
    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            //선택 소비아이템 효과 적용
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value);  //체력회복
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value);   //배고픔회복
                        break;
                    case ConsumableType.Speed:
                        controller.ApplySpeedBoost(selectedItem.consumables[i].value, selectedItem.consumables[i].duration);
                        break;      //속도증가
                }
            }
            RemoveSelectedItem();   //사용아이템 제거
        }
    }

    //버리기버튼
    public void OnDropButton()
    {
        ThrowItem(selectedItem); //선택아이템버리기
        RemoveSelectedItem();   //선택아이템버리기
    }

    //선택아이템 제거
    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--; //수량 감소
        if (slots[selectedItemIndex].quantity <= 0)
        {
            //수량이 0이하면 아이템 삭제 초기화
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSeletedItemWindow(); //아이템 선택창 초기화
        }
        UpdateUI(); //UI갱신
    }

  

  

  
}
