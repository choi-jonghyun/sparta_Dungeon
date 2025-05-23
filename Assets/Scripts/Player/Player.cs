using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;     //�÷��̾���Ʈ��
    public PlayerCondition condition;       //�÷��̾����
    public ItemData itemData;               //�����Ѿ����۵�����
    public Action addItem;                  //�������߰���

    public Transform dropPosition;          //������ �����ġ

    private void Awake()
    {
        //ĳ���͸Ŵ��� ��Ŭ�濡 �÷��̾� �ν��Ͻ����
        CharacterManager.Instance.Player = this;
        //�÷��̾���Ʈ�ѷ� ����� ������Ʈ ������
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
