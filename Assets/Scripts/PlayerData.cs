using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float lookingTargetDistance = 10.0f; // I don't think this will change anything
    public Vector2 turningSpeed;
    public float movementSpeed;
    public float maxGroundSpeed;
    public float maxAirSpeed;

    [Range(0.0f, 90.0f)]
    public float gimbalLockClamp = 85.0f;

    public float groundCheckDistance = 0.2f;

    public float groundDrag;
    public float airDrag;
    public LayerMask groundLayer;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool holdingSpaceKeepsJumping = false;
}
