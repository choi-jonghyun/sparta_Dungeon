using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    //체크간격 
    public float checkRate = 0.05f;
    private float lastCheckTime;
   //최대 체크 거리
    public float maxCheckDistance;
    //상호작용 가능한 레이어
    public LayerMask layerMask;
    //현재 보는 상호작용오브젝트
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;
    //UI에 표시할 텍스트
    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //마지막체크 후 얼마나 지났는지
        if (Time.time - lastCheckTime > checkRate) 
        {
            lastCheckTime = Time.time;

            //화면 중앙 레이캐스트
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
           //상호작용 가능 레이어에 레이어캐스트가 닿았는지
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                //다른 오브젝트를 바라보는 경우만
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                   //IInteractable 가져오기
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText(); 
                }
            }
            else
            {
                //아무것도 안보면 초기화
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()    //상호작용 문구 표시
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    
    //상호작용 키가 눌렸을때
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        //상호작용 키가 눌리고 대상이 있다면 시작
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();       //상호작용 시작   
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false); //숨김
        }
    }
}
