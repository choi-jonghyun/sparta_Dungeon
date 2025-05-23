using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;  //�����ġ
    public float maxValue;  //�ִ��ġ
    public float startValue;    //���ۼ�ġ
    public float passiveValue;  //���� ��ġ
    public Image uiBar;         //����UI

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;  //�����ġ�� ���ۼ�ġ��
    }
    private void Update()
    {
        uiBar.fillAmount = GetPercentage(); //UI�ٸ� �����ġ�� �°� ������Ʈ
    }

    //��ġ����
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    //��ġ����
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }
    
    //���� ��ġ ����
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
