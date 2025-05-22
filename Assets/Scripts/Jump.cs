using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float launchPower = 30f;
    public Vector3 launchDirection = Vector3.up;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody _rigidbody = other.GetComponent<Rigidbody>();
        if(_rigidbody != null)
        {
            _rigidbody.velocity = Vector3.zero;

            _rigidbody.AddForce(launchDirection.normalized *  launchPower, ForceMode.Impulse);
        }
    }
}
