using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float impulseForce;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bumper"))
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * impulseForce * rb.mass, ForceMode.Impulse);
        }
    }
}