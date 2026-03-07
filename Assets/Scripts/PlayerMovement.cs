using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private GameObject LookingTarget;
    [SerializeField]
    private Transform headPosition;
    [SerializeField]
    private Rigidbody rigidbodyComponent;

    Vector3 facingDirection = Vector3.forward;
    Vector3 rightFacingVector = Vector3.right;
    Vector3 flattenedFacingDirection = Vector3.forward;
    Vector3 flattenedRightFacingVector = Vector3.right;

    Vector2 movementInputValue = Vector2.zero;
    Vector2 turningInputValue = Vector2.zero;
    bool jumpInputValue = false;

    bool isGrounded = false;
    bool canJump;

    float yaw;
    float pitch;

    [SerializeField]
    PlayerData tempPlayerData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Turning
        if (turningInputValue != Vector2.zero)
        {
            yaw += turningInputValue.x * tempPlayerData.turningSpeed.x * Time.deltaTime;
            pitch -= turningInputValue.y * tempPlayerData.turningSpeed.y * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -tempPlayerData.gimbalLockClamp, tempPlayerData.gimbalLockClamp);

            facingDirection = Quaternion.Euler(pitch, yaw, 0.0f) * Vector3.forward;

            facingDirection.Normalize();
            flattenedFacingDirection = Vector3.ProjectOnPlane(facingDirection, Vector3.up).normalized;
            rightFacingVector = Vector3.Cross(Vector3.up, facingDirection).normalized;
            flattenedRightFacingVector = Vector3.Cross(Vector3.up, flattenedFacingDirection).normalized;


            LookingTarget.transform.position = headPosition.position + facingDirection * tempPlayerData.lookingTargetDistance;
        }

        // GroundCheck
        isGrounded = Physics.Raycast(transform.position, Vector3.down, headPosition.localPosition.y, tempPlayerData.groundLayer);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJumping();
    }

    private void HandleMovement()
    {
        rigidbodyComponent.linearDamping = isGrounded ?
            tempPlayerData.groundDrag :
            1.0f;


        Vector3 movementDirection =
            (flattenedRightFacingVector * movementInputValue.x)
            + (flattenedFacingDirection * movementInputValue.y);

        float speedValue = isGrounded ?
            tempPlayerData.movementSpeed :
            tempPlayerData.movementSpeed * tempPlayerData.airMultiplier;
        rigidbodyComponent.AddForce(movementDirection * speedValue,
            ForceMode.Force);

        Vector3 directionalVelocity = new Vector3(rigidbodyComponent.linearVelocity.x,
            0.0f,
            rigidbodyComponent.linearVelocity.z);

        if (directionalVelocity.magnitude
            >
            tempPlayerData.movementSpeed)
        {
            rigidbodyComponent.linearVelocity = directionalVelocity.normalized * tempPlayerData.movementSpeed;
        }
    }

    private void HandleJumping()
    {
        if (!jumpInputValue || !isGrounded)
        {
            return;
        }

        rigidbodyComponent.linearVelocity = new Vector3(
            rigidbodyComponent.linearVelocity.x,
            0.0f,
            rigidbodyComponent.linearVelocity.z);

        rigidbodyComponent.AddForce(transform.up * tempPlayerData.jumpForce, ForceMode.Impulse);

        if (!tempPlayerData.holdingSpaceKeepsJumping)
        {
            jumpInputValue = false;
        }
    }


    public void UpdateMovementInput(InputAction.CallbackContext context)
    {
        movementInputValue = context.ReadValue<Vector2>();
    }

    public void UpdateTurningInput(InputAction.CallbackContext context)
    {
        turningInputValue = context.ReadValue<Vector2>();
    }
    public void UpdateJumpingInput(InputAction.CallbackContext context)
    {
        jumpInputValue = context.ReadValue<bool>();
    }
}
