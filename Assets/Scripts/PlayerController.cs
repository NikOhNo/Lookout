using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] float groundDist;
    [SerializeField] LayerMask terrainLayer;
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject spriteObject;

    Vector2 moveInput;
    Animator Animator => GetComponent<Animator>();

    private void Update()
    {
        Vector3 move = moveSpeed * Time.deltaTime * new Vector3(moveInput.x, 0, moveInput.y);

        if (controller.isGrounded)
        {
            move.y = -1f;
        }
        else
        {
             move.y += Physics.gravity.y * Time.deltaTime;
        }
        controller.Move(move);
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Animator.SetFloat("x", moveInput.x);
        HandleSpriteFlip();
        Animator.SetFloat("z", moveInput.y);
    }

    private void HandleSpriteFlip()
    {
        if (moveInput.x < 0)
        {
            spriteObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            spriteObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
