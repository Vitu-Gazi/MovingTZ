using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float speed;

    private Vector3 direction = new Vector3();


    private void Update()
    {
    }

    private void FixedUpdate()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        direction *= speed;
        rigid.velocity = direction;
    }
}
