using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상호작용 인터페이스
public interface IInteractable
{
    public string GetInteractPrompt();      //상호작용 시 화면에 표시 
    public void OnInteract();   //상호작용 
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()   //상호작용 할 때 표시 문구
    {
        string str = $"{data.displayName}\n{data.description}"; //이름과 설명을 줄바꿈하고 표시
        return str;
    }
    public void OnInteract() //상호작용시 실행
    {
        // 플레이어의 아이템데이터에 아이템데이터 전달
        CharacterManager.Instance.Player.itemData = data;
        //플레이어의 아이템추가 호출
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
