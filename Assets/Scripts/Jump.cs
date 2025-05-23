using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float launchPower = 10f; //점프 힘
    public Vector3 launchDirection = Vector3.up;    //점프방향

    //트리거실행
    private void OnTriggerEnter(Collider other)
    {
        //리지드바디 가져옴
        Rigidbody _rigidbody = other.GetComponent<Rigidbody>();
        //리지드바디가 있으면
        if(_rigidbody != null)
        {
            //속도를 0으로
            _rigidbody.velocity = Vector3.zero;

            //설정한 방향으로 점프함
            _rigidbody.AddForce(launchDirection.normalized *  launchPower, ForceMode.Impulse);
        }
    }
}
