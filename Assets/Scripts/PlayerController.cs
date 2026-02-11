using System.Net.Mime;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed = 25f;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] float coffeeDecrementRate = 1.0f; // per second

    [Header("References")]
    [SerializeField] GameObject body;
    [SerializeField] GameObject spriteObject;
    [SerializeField] CharacterController controller;
    [SerializeField] ProgressBar coffeeProgressBar;

    CinemachineCamera cinemaCam;
    Vector2 moveInput;
    Animator Animator => GetComponent<Animator>();

    float CoffeeMeter
    {
        get
        {
            return _coffeeMeter;
        }
        set
        {
            _coffeeMeter = Mathf.Clamp(value, 0.0f, 100.0f);
            coffeeProgressBar.fillAmount = _coffeeMeter / 100.0f;
        }
    }
    float _coffeeMeter = 100.0f;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        cinemaCam = FindFirstObjectByType<CinemachineCamera>();
    }

    private void Update()
    {
        Vector3 move = moveSpeed * Time.deltaTime * new Vector3(moveInput.x, 0, moveInput.y);
        move  = body.transform.TransformDirection(move);

        if (controller.isGrounded)
        {
            move.y = -1f;
        }
        else
        {
            move.y += Physics.gravity.y * Time.deltaTime;
        }
        controller.Move(move);

        // update coffee
        CoffeeMeter -= coffeeDecrementRate * Time.deltaTime;
        if (cinemaCam.TryGetComponent<CinemachineVolumeSettings>(out var volumeSettings))
        {
            if (volumeSettings.Profile.TryGet(out Vignette vignette))
            {
                vignette.intensity.value = 1.0f - (CoffeeMeter / 100.0f);
            }
        }
    }

    public void HandleLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        
        body.transform.Rotate(Vector3.up, lookInput.x * cameraSpeed, Space.World);
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Animator.SetFloat("x", moveInput.x);
        HandleSpriteFlip();
        Animator.SetFloat("z", moveInput.y);
    }

    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponentInChildren<Interactor>().Interact();
        }
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

    // call to reset. the coffee meter
    // basically whenever you Drink this will be called by this script somewhere
    public void ResetCoffee()
    {
        CoffeeMeter = 100.0f;
    }
}
