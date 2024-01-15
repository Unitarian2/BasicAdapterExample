using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : Subject
{
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey) && playerStats.IsManaEnough();
    private bool ShouldJump => characterController.isGrounded && Input.GetKey(jumpKey);

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canUseHeadMotion = true;

    [Header("Keycodes")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Params")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
 
    [Header("Look Params")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jump Params")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Head Motion Params")]
    [SerializeField] private float walkHeadMotionSpeed = 14.0f;
    [SerializeField] private float walkHeadMotionAmount = 0.025f;
    [SerializeField] private float sprintHeadMotionSpeed = 18.0f;
    [SerializeField] private float sprintHeadMotionAmount = 0.05f;
    private float defaultYPos;
    private float timer;

    private Camera playerCamera;
    private CharacterController characterController;
    private PlayerStats playerStats;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;


    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            
            HandleMovementInput();
            HandleMouseLook();
            HandleMana();

            if (canJump) HandleJump();

            if (canUseHeadMotion) HandleHeadMotion();

            ApplyMovement();
        }

        
    }

    private void HandleMovementInput()
    {
        
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        //Debug.Log(new Vector2(walkSpeed * Input.GetAxis("Vertical"), walkSpeed * Input.GetAxis("Horizontal")));
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.forward * currentInput.x) + (transform.right * currentInput.y);
        moveDirection.y = moveDirectionY;
        //Debug.Log(currentInput);

        
    }
    private void HandleMana()
    {
        if (IsSprinting)
        {
            NotifyObservers(StatType.Mana, -30f * Time.deltaTime);
        }
        
    }

    private void HandleJump()
    {
        if (ShouldJump)
        {
            moveDirection.y = jumpForce;
        }
    }

    private void HandleHeadMotion()
    {
        if (!characterController.isGrounded) return;

        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (IsSprinting ? sprintHeadMotionSpeed : walkHeadMotionSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (IsSprinting ? sprintHeadMotionAmount : walkHeadMotionAmount),
                playerCamera.transform.localPosition.z
                );
        }
    }

    private void HandleMouseLook()
    {
        //Up and Down için kamerayý döndürüyoruz.
        rotationX -= Input.GetAxis("Mouse Y")*lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //Left and Right için Player'ý döndürüyoruz.
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void ApplyMovement()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
