using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item; //���Ծȿ� �����۵�����

    public Button button;   //���� ��ư
    public Image icon;      //���� �̹���
    public TextMeshProUGUI quantityText;    //�����ؽ�Ʈ 2�� �̻��϶�
    private Outline outline;    // ���� �ܰ���(���� ����ǥ��)

    public UIInventory inventory;   //���� �κ��丮ui

    public int index;
    public int quantity;    //����

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

 

    public void Set()
    {
        icon.gameObject.SetActive(true); //�����ܺ��̱�
        icon.sprite = item.icon;    //������ �̹��� ����
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty; //����ǥ��

      
    }

    public void Clear() //���� ����
    {
        item = null;
        icon.gameObject.SetActive(false); //������ �����
        quantityText.text = string.Empty; //�����ؽ�Ʈ ����
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}

