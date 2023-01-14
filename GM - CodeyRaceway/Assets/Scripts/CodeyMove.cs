using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CodeyMove : MonoBehaviour
{
    public float Speed = 15f;
    Animator anim;
    public bool running = false;
    public bool canMove = true;
    public Vector3 move;
    public float _rotationSpeed = 50f;
    private Rigidbody rb;
    public float levamount = 1000f;
    private float originalY;
    public float heightbuffer = 0.5f;
    void Start()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        originalY = rb.position.y;
    }
    void Update()
    {
       
        if (canMove)
        {

            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");            
            Vector3 rotation = new Vector3(0, horizontal * _rotationSpeed * Time.deltaTime, 0);
            move = transform.forward * Speed * Time.deltaTime * vertical;
            transform.Rotate(rotation);

            Vector3 levitation = Vector3.zero;
            if (rb.position.y < originalY + heightbuffer)
            {
                levitation = Vector3.up * levamount;
            }

            rb.AddForce(move + levitation, ForceMode.VelocityChange);

            anim.SetBool("isRunning", move != Vector3.zero);
        }
        
    }
}