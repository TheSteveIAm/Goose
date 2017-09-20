using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GooseController : MonoBehaviour
{

    private Rigidbody body;
    private Animator anim;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float rotateSpeed = 150f;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float motion = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        float speed = 0;

        if (Input.GetKey(KeyCode.W))
        {
            speed = 0.5f;
            motion /= 2;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 1f;
            motion *= 2;
        }

        anim.SetFloat("LegSpeed", speed);

        body.MovePosition(transform.position + transform.forward * motion);

        body.MoveRotation(transform.rotation * Quaternion.Euler(0, rotation, 0));

    }
}
