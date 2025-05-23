using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;    //체력
    public Condition hunger;    //배고픔
    public Condition stamina;   //스태미나

    // Start is called before the first frame update
    private void Start()
    {
        //플레이어컨디션에서 UI컨디션을 연결
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

    
}
