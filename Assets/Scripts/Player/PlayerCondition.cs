using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable     //�������� �޴� �������̽�
{
    //���������� �޴�
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition; //UI�� ����

    //�� ���� ������Ƽ UI����ǿ��� ������
    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;   //�������0�϶� ü�°���
    public event Action onTakeDamage;       //�������� �޾�����

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);  //������� ���� ����
        stamina.Add(stamina.passiveValue * Time.deltaTime);     //���¹̳��� ���� ȸ����Ŵ

        //������� 0�϶� ü�°���
        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay *  Time.deltaTime);
        }

        //ü��0�϶� ����
        if(health.curValue <= 0f)
        {
            Die();
        }
    }

    //ü�� ȸ��
    public void Heal(float amount)
    {
        health.Add(amount);
    }

    //����� ä��
    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    //���ó��
    public void Die()
    {
        Debug.Log("����");
    }

    //���������� �޴�
    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke(); //������ ������ ȣ��
    }

    public bool UseStamina(float amount)    //���¹̳��� ��� ����
    {
        //���¹̳��� �����ϸ� false
        if(stamina.curValue - amount < 0f)
        {
            return false;
        }
        //�� �ݴ�� true
        stamina.Subtract(amount);
        return true;
    }

}
