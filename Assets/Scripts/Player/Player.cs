using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;     //플레이어컨트롤
    public PlayerCondition condition;       //플레이어상태
    public ItemData itemData;               //보유한아이템데이터
    public Action addItem;                  //아이템추가시

    public Transform dropPosition;          //아이템 드랍위치

    private void Awake()
    {
        //캐릭터매니저 싱클톤에 플레이어 인스턴스등록
        CharacterManager.Instance.Player = this;
        //플레이어컨트롤러 컨디션 컴포넌트 가져옴
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}
