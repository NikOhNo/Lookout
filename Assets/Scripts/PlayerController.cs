using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] float groundDist;
    [SerializeField] LayerMask terrainLayer;
    [SerializeField] Rigidbody rb;
    [SerializeField] SpriteRenderer sr;

    public void HandleMove(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector3(moveVector.x, 0, moveVector.y) * moveSpeed;
    }
}
