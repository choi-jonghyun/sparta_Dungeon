using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float launchPower = 10f; //���� ��
    public Vector3 launchDirection = Vector3.up;    //��������

    //Ʈ���Ž���
    private void OnTriggerEnter(Collider other)
    {
        //������ٵ� ������
        Rigidbody _rigidbody = other.GetComponent<Rigidbody>();
        //������ٵ� ������
        if(_rigidbody != null)
        {
            //�ӵ��� 0����
            _rigidbody.velocity = Vector3.zero;

            //������ �������� ������
            _rigidbody.AddForce(launchDirection.normalized *  launchPower, ForceMode.Impulse);
        }
    }
}
