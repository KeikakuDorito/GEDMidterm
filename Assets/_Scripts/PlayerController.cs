using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;


    public PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;
    private float walkSpeed = 5f;
    public Camera playerCamera;
    Vector3 cameraRotation;

    public float jump = 5.0f;
    
    //Jump
    Rigidbody rb;
    private float distanceToGround;
    private bool isGrounded = true;

    Animator playerAnimator;
    //private bool isWalking = false;


    //Projectile Bullets
    public GameObject bullet;
    public Transform projectilePos;

    

    // Start is called before the first frame update
    void Start()//Awake()
    {
        if (!instance)
            instance = this;

        //inputAction = new PlayerAction();
        //Now using contrller from playerinputcontroller
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Player.Move.performed += cntex => move = cntex.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntex => move = Vector2.zero;

        inputAction.Player.Jump.performed += cntex =>Jump();
        inputAction.Player.Shoot.performed += cntex => Shoot();

        rb = GetComponent<Rigidbody>();
        Animator animator = GetComponent<Animator>();
        playerAnimator = animator;

        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        Debug.Log(distanceToGround);
    }

    //private void OnEnable()
    //{
    //    inputAction.Player.Enable();
    //}

    //private void OnDisable()
    //{
    //    inputAction.Player.Disable();
    //}

    private void Jump()
    {
        Debug.Log("jumping");
        if (isGrounded)
        {
            Debug.Log("jumped");
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }
    }

    public void Shoot()
    {
        Rigidbody bulletRB = Instantiate(bullet, projectilePos.position, Quaternion.identity).GetComponent<Rigidbody>();
        bulletRB.AddForce(transform.forward * 37f, ForceMode.Impulse);
        bulletRB.AddForce(transform.up * 5f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.forward * move.x * Time.deltaTime * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
    }
}
