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

    bool isGrounded = false;

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

        Vector3 movemenetValue =
            (flattenedRightFacingVector * movementInputValue.x * tempPlayerData.movementSpeed.x)
            + (flattenedFacingDirection * movementInputValue.y * tempPlayerData.movementSpeed.y);

        rigidbodyComponent.linearVelocity = new Vector3(movemenetValue.x, rigidbodyComponent.linearVelocity.y, movemenetValue.z);
    }




    public void UpdateMovementInput(InputAction.CallbackContext context)
    {
        movementInputValue = context.ReadValue<Vector2>();
    }

    public void UpdateTurningInput(InputAction.CallbackContext context)
    {
        turningInputValue = context.ReadValue<Vector2>();
    }
}
