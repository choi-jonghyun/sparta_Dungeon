using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable     //데미지를 받는 인터페이스
{
    //물리데미지 받는
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition; //UI와 연결

    //각 상태 프로퍼티 UI컨디션에서 가져옴
    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;   //배고픔이0일때 체력감소
    public event Action onTakeDamage;       //데미지를 받았을때

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);  //배고픔을 점점 줄임
        stamina.Add(stamina.passiveValue * Time.deltaTime);     //스태미나를 점점 회복시킴

        //배고픔이 0일때 체력감소
        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay *  Time.deltaTime);
        }

        //체력0일때 죽음
        if(health.curValue <= 0f)
        {
            Die();
        }
    }

    //체력 회복
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    //배고픔 채움
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    //사망처리
    public void Die()
    {
        Debug.Log("죽음");
    }

    //물리데미지 받는
    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke(); //데미지 받을때 호출
    }

    public bool UseStamina(float amount)    //스태미나를 사용 여부
    {
        //스태미나가 부족하면 false
        if(stamina.curValue - amount < 0f)
        {
            return false;
        }
        //그 반대면 true
        stamina.Subtract(amount);
        return true;
    }

}
