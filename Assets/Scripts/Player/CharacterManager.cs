using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //�̱���
    private static CharacterManager _instance;

    
    public static CharacterManager Instance
    {
        get
        {
            //�ν��Ͻ��� ������ �� ���ӿ�����Ʈ ĳ���͸Ŵ����� �߰�
            if(_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }
    public Player _player;  //�÷��̾� ������ ����

    public Player Player        //�÷��̾� ���ٿ�
    {
        get {  return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        //�ν��Ͻ��� ������ �ν��Ͻ��� �����ϰ� ������� �ı���������
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //�ν��Ͻ��� �ִٸ� �ı�
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
