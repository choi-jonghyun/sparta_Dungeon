using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;    //ü��
    public Condition hunger;    //�����
    public Condition stamina;   //���¹̳�

    // Start is called before the first frame update
    private void Start()
    {
        //�÷��̾�����ǿ��� UI������� ����
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

    
}
