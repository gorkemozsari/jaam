using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    private Animator animator;
    public Transform mainCam;
    
    private bool isJumping;
    private bool isGrounded;

    [SerializeField] private float JumpHorizontalSpeed;

    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        isJumping = false;
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(isGrounded)
        {
            if (x == 0 && z == 0)
            {
                animator.SetBool("IsMoving", false);
            }
            else
            {
                animator.SetBool("IsMoving", true);
            }
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        /* if(!isGrounded)
        {
            Vector3 move = transform.right * x * JumpHorizontalSpeed;
            move.y = velocity.y;
            controller.Move(move * speed * Time.deltaTime);
        } */
        
        if(isGrounded)
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                animator.SetBool("IsJumping", true);
                isJumping = true;
            }

        }
        else
        {
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if((isJumping && velocity.y < 0) || velocity.y < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        controller.transform.Rotate(Vector3.up * x * (100f * Time.deltaTime));

    }

}
