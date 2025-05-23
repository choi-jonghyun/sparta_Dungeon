using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    //üũ���� 
    public float checkRate = 0.05f;
    private float lastCheckTime;
   //�ִ� üũ �Ÿ�
    public float maxCheckDistance;
    //��ȣ�ۿ� ������ ���̾�
    public LayerMask layerMask;
    //���� ���� ��ȣ�ۿ������Ʈ
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;
    //UI�� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //������üũ �� �󸶳� ��������
        if (Time.time - lastCheckTime > checkRate) 
        {
            lastCheckTime = Time.time;

            //ȭ�� �߾� ����ĳ��Ʈ
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
           //��ȣ�ۿ� ���� ���̾ ���̾�ĳ��Ʈ�� ��Ҵ���
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                //�ٸ� ������Ʈ�� �ٶ󺸴� ��츸
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                   //IInteractable ��������
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText(); 
                }
            }
            else
            {
                //�ƹ��͵� �Ⱥ��� �ʱ�ȭ
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()    //��ȣ�ۿ� ���� ǥ��
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    
    //��ȣ�ۿ� Ű�� ��������
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        //��ȣ�ۿ� Ű�� ������ ����� �ִٸ� ����
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();       //��ȣ�ۿ� ����   
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false); //����
        }
    }
}
