using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; //이동속도
    private Vector2 curMovementInput;   //현재이동입력
    private float originalSpeed;        //원래이동속도
    private Coroutine speedBoostCoroutine;  //속도증가 코루틴
    public float jumpPower;             //점프 힘
    public LayerMask groundLayerMask;   //점프가능 판정용레이어

    [Header("Look")]
    public Transform cameraContainer;   //카메라가 따라다닐 위치
    public float minXLook;      //상하회전 최소각
    public float maxXLook;      //상하회전 최대각
    public float camCurXRot;    //현재 카메라 x축 회전값
    public float lookSensitivity;   //마우스 감도
    private Vector2 mouseDelta;     //마우스 이동값
    public bool canLook = true;     //카메라 회전가능여부
    public Rigidbody _rigidbody;    

    public Action inventory;    //인벤토리 열기

    private void Awake()
    {
        //리지드바디 컴포넌트 가져오기
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   //커서 잠금
        originalSpeed = moveSpeed;                  //원래이동속도 설정
    }
    private void FixedUpdate()
    {
        Move();     //물리이동
    }
    private void LateUpdate()
    {
        
        if (canLook)
        {
            CameraLook();
        }
    }

    //마우스 움직임
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    //이동
    public void OnmoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    //점프
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    //인벤토리
    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    //마우스커서 상태
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    //이동
    private void Move()
    {
       Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    //카메라 회전
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    //속도 증가 효과
    public void ApplySpeedBoost(float amount, float duration)
    {
        if(speedBoostCoroutine != null) 
            StopCoroutine(speedBoostCoroutine);

        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(amount, duration));
    }

    //일정 시간 후 복구 코루틴
    private IEnumerator SpeedBoostRoutine(float amount, float duration)
    {
        moveSpeed += amount;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
        speedBoostCoroutine = null;
    }

    //땅에 닿았는지
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f)+(transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+(transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f)+(transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f)+(transform.up * 0.01f), Vector3.down),
        };
        
        for(int i = 0; i< rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
            
    }

    //외부에서 커서 토글 가능
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
