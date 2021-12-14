using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int jumpnum = 2;
    public CharacterController controller;
    public float speed = 12f;
    public float sprintspeed = 16f;
    public float walkspeed = 11f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    Vector3 velocity;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        Debug.Log(isGrounded);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 WalkSpeed = transform.right * x + transform.forward * z;

        controller.Move(WalkSpeed * speed * Time.deltaTime);

        controller = GetComponent<CharacterController>();

        if (Input.GetButtonDown("Jump") && jumpnum > 1 && jumpnum < 3)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpnum -= 1;
        }

        if (isGrounded)
        {
            jumpnum = 2;
            controller.stepOffset = 3.8f;
        }else
        {
            controller.stepOffset = 0.0f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && z == 1)
        {
            speed = sprintspeed;
        }else
        {
            speed = walkspeed;
        }



        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

}