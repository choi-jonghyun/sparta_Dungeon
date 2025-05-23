using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{

    public ItemSlot[] slots;        //�����۽��� �迭
    public GameObject inventoryWindow;  //�κ��丮â
    public Transform slotPanel;     //���Թ�ġ �ǳ�
    public Transform dropPosition;  //����� ��ġ


    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;        //���þ����� �̸��ؽ�Ʈ
    public TextMeshProUGUI selectedItemDescription; //���þ����� �����ؽ�Ʈ
    public TextMeshProUGUI selectedStatName;        //���þ����� ȿ���̸��ؽ�Ʈ
    public TextMeshProUGUI selectedStatValue;       //���þ����� ȿ����ġ�ؽ�Ʈ
    public GameObject useButton;                    //������ ����ư
    public GameObject dropButton;                   //������ �������ư

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;      //���þ����� ������
    int selectedItemIndex = 0;  //���þ����� �ε���

    

    private void Start()
    {
        //�÷��̾���Ʈ�ѷ�,���� ����
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;     //��� ����(�κ��丮 ����ݱ�)
        CharacterManager.Instance.Player.addItem += AddItem;    //additem�� AddItem����(������ȹ��)
        inventoryWindow.SetActive(false);       //�κ��丮â ��Ȱ��ȭ
        slots = new ItemSlot[slotPanel.childCount];     //���� �迭 ũ�⼳��

        //���� �迭 �ʱ�ȭ
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        ClearSeletedItemWindow();       //���þ�����â �ʱ�ȭ
    }

    //���þ�����â �ʱ�ȭ
    void ClearSeletedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);      //����ư����     
        dropButton.SetActive(false);    //�������ư����
    }

    //�κ��丮â ����ݱ�
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

    //�κ��丮â�� �����ִ���
    public bool isOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    //������ ȹ��� ȣ��
    void AddItem() 
    {
        //�÷��̾ ���� �����۵����� ������
        ItemData data = CharacterManager.Instance.Player.itemData;
        //�������� ��ĥ�� ������
        if (data.canStack)
        {
            // ���������� ���� ã��
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;    //����1����
                UpdateUI();         //UI����
                CharacterManager.Instance.Player.itemData = null;   //�÷��̾� ������ �ʱ�ȭ
                return;
            }
        }
        //��ġ�� ���ϰų� ������������ ������ �󽽷� ã��
        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = data;  //�󽽷Կ� �������߰�
            emptySlot.quantity = 1; //����1�� ����
            UpdateUI();             //UI����
            CharacterManager.Instance.Player.itemData = null;   //�÷��̾�������ʱ�ȭ
            return;
        }
        //������ ������ ������ ����
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null; //�÷��̾� ������ �ʱ�ȭ
    }

    //�κ��丮UI ����
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set(); //���Կ� ����
            }
            else
            {
                slots[i].Clear();   //�󽽷� ����
            }
        }
    }

    //������������ �ִ� ���� ã��
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null; //������ null
    }

    //�󽽷�ã��
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
    //�����۹�����
    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    //���þ����� ǥ��
    public void SelectItem(int index)
    {
        if (slots[index].item == null) return; //���Կ� �������� ������ ����

        selectedItem = slots[index].item;   //���þ����� ����
        selectedItemIndex = index;          //���þ����� ����

        selectedItemName.text = selectedItem.displayName;       //������ �̸�ǥ���ؽ�Ʈ
        selectedItemDescription.text = selectedItem.description;    //������ ����ǥ���ؽ�Ʈ

        //�ʱ�ȭ
        selectedItemName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        //���þ����� �Һ�ȿ���� �ٹٲ����� ǥ��
        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedItemName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }
        //�Һ�������̸� ����ư Ȱ��
        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        dropButton.SetActive(true); //�������ư Ȱ��ȭ

    }

    //����ư
    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            //���� �Һ������ ȿ�� ����
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value);  //ü��ȸ��
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value);   //�����ȸ��
                        break;
                    case ConsumableType.Speed:
                        controller.ApplySpeedBoost(selectedItem.consumables[i].value, selectedItem.consumables[i].duration);
                        break;      //�ӵ�����
                }
            }
            RemoveSelectedItem();   //�������� ����
        }
    }

    //�������ư
    public void OnDropButton()
    {
        ThrowItem(selectedItem); //���þ����۹�����
        RemoveSelectedItem();   //���þ����۹�����
    }

    //���þ����� ����
    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--; //���� ����
        if (slots[selectedItemIndex].quantity <= 0)
        {
            //������ 0���ϸ� ������ ���� �ʱ�ȭ
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSeletedItemWindow(); //������ ����â �ʱ�ȭ
        }
        UpdateUI(); //UI����
    }

  

  

  
}
