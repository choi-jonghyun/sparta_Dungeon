using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ȣ�ۿ� �������̽�
public interface IInteractable
{
    public string GetInteractPrompt();      //��ȣ�ۿ� �� ȭ�鿡 ǥ�� 
    public void OnInteract();   //��ȣ�ۿ� 
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()   //��ȣ�ۿ� �� �� ǥ�� ����
    {
        string str = $"{data.displayName}\n{data.description}"; //�̸��� ������ �ٹٲ��ϰ� ǥ��
        return str;
    }
    public void OnInteract() //��ȣ�ۿ�� ����
    {
        // �÷��̾��� �����۵����Ϳ� �����۵����� ����
        CharacterManager.Instance.Player.itemData = data;
        //�÷��̾��� �������߰� ȣ��
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
