using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    public CharacterController controller;
    InputController playerInput;
    [SerializeField]AudioController footSteps;
    [SerializeField] float minimalMoveTreshold;

    [SerializeField] CinemachineVirtualCamera vcam;
    //GameObject follow;
    //GameObject lookAt;


    Vector3 previousPosition;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float defaultSpeed = 3f;
    public float sprintSpeed = 5f;
    public float walkSpeed = 1f;
    public float crouchSpeed = 2f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    void Awake()
    {
        playerInput = GameManager.Instance.InputController;

        //vcam

        //vcam2 = new CinemachineVirtualCamera();
        //follow = GameObject.Find("mixamorig:Head");
        //Transform followTransfrom = follow.GetComponent<Transform>();
        //lookAt = GameObject.Find("LookAt");
        //Transform LookAtTransform = lookAt.GetComponent<Transform>();
        //vcam2.Follow = followTransfrom;
        //vcam2.LookAt = LookAtTransform;

        //CinemachineTransposer transposer = new CinemachineTransposer();
        //CinemachineComposer composer = new CinemachineComposer();

        //vcam.AddCinemachineComponent<CinemachineComponentBase>();
        //vcam.AddCinemachineComponent<CinemachineTransposer>();
        //vcam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;

        //vcam.GetCinemachineComponent<CinemachineTransposer>().m_BindingMode.Equals(1);
        //vcam.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
        //vcam.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
        //vcam.GetComponent<CinemachineComposer>().m_LookaheadTime = 0;
        //vcam.GetComponent<CinemachineComposer>().m_LookaheadSmoothing = 10;
        //vcam.GetComponent<CinemachineComposer>().m_HorizontalDamping = 0;
        //vcam.GetComponent<CinemachineComposer>().m_VerticalDamping = 0;
        //vcam.GetComponent<CinemachineComposer>().m_ScreenX = 0.5f;
        //vcam.GetComponent<CinemachineComposer>().m_ScreenY = 0.5f;
        //vcam.GetComponent<CinemachineComposer>().m_SoftZoneWidth = 0.06f;
        //vcam.GetComponent<CinemachineComposer>().m_SoftZoneHeight = 0.29f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
            Move();
    }

    void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float moveSpeed = defaultSpeed;

        if (vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping > 0 || vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping > 0)
        {
            vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping - 0.01f;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping - 0.01f;
        }

        if (playerInput.Vertical > 0 || playerInput.Vertical < 0)
        {
            vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 2;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 2;
        }

        if (playerInput.Horizontal > 0 || playerInput.Horizontal < 0)
        {
            vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 2;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 2;
        }

        if (playerInput.IsWalking)
        {
            moveSpeed = walkSpeed;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 0.5f;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 0.5f;
        }

        if (playerInput.IsCrouched)
        {
            moveSpeed = crouchSpeed;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 0.2f;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 0.2f;
        }

        if (playerInput.IsSprinting)
        {
            moveSpeed = sprintSpeed;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 4;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 4;
        }

        if (playerInput.IsAiming)
        {
            vcam.GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping = 0;
            vcam.GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = playerInput.Horizontal;
        float z = playerInput.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (x != 0 || z != 0 )
        {
            if (Vector3.Distance(transform.position, previousPosition) > minimalMoveTreshold)
            {
                footSteps.Play();
            }
        }

        previousPosition = transform.position;
    }
}
