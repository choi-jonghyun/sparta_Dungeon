using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //싱글톤
    private static CharacterManager _instance;

    
    public static CharacterManager Instance
    {
        get
        {
            //인스턴스가 없으면 새 게임오브젝트 캐릭터매니저를 추가
            if(_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }
    public Player _player;  //플레이어 정보를 저장

    public Player Player        //플레이어 접근용
    {
        get {  return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        //인스턴스가 없으면 인스턴스로 설정하고 씬변경시 파괴되지않음
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //인스턴스가 있다면 파괴
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
