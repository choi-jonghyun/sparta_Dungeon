using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;  //현재수치
    public float maxValue;  //최대수치
    public float startValue;    //시작수치
    public float passiveValue;  //지속 수치
    public Image uiBar;         //상태UI

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue;  //현재수치를 시작수치로
    }
    private void Update()
    {
        uiBar.fillAmount = GetPercentage(); //UI바를 현재수치에 맞게 업데이트
    }

    //수치증가
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    //수치감소
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }
    
    //현재 수치 비율
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
