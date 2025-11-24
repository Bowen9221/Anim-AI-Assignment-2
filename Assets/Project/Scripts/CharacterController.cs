using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControls : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator anim;

    Vector3 currentMovementInput;
    Vector3 currentMovement;


    bool isMovementPressed;
    bool isJumpPressed = false;

    public float rotationFactorPerFrame;

    public float moveSpeed;

    public float gravity = -4f;
    float groundedGravity = -0.01f;

    float initialiJumpVelocity;
    public float maxJumpHeight = 10.0f;
    public float maxJumpTime = 1.5f;
    [SerializeField] bool isJumping = false;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
        playerInput.Player.Jump.started += DoJump;
        playerInput.Player.Jump.canceled += DoJump;

        SetupJumpVariables();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector3>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.z;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.z != 0;
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;


        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            groundedGravity = -.05f;
            currentMovement.y += groundedGravity;
        }
        else
        {
            currentMovement.y += gravity;
        }
    }
    void DoJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();

    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        //gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialiJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (maxJumpHeight + 4)) / (timeToApex * 1.5f);
    }

    void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialiJumpVelocity;
        }
        else if (!isJumpPressed && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void Update()
    {
        HandleAnimation();
        HandleRotation();
        characterController.Move(currentMovement *  moveSpeed * Time.deltaTime);
        HandleGravity();
        HandleJump();
    }

    void HandleAnimation()
    {
        if (isJumping)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }


        if (isMovementPressed)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
}
