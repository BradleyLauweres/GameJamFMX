using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = Settings.Gravity;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Camera Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private bool lockCursor = true;

    [Header("UI")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject InteractableTextPlaceholder;
    [SerializeField] private GameObject PickableTextPlaceholder;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
                playerCamera = mainCamera.transform;
        }

        GameManager.lockMode = CursorLockMode.Locked;
    }

    void Update()
    {
        if(GameManager.state == GameState.Playing)
        {

            GameManager.lockMode = CursorLockMode.Locked;
            UI.SetActive(true);

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            HandleCameraRotation();
            HandleMovement();
            HandleJumping();

            ApplyGravity();
        }
        else if (GameManager.state == GameState.Interacting)
        {
            UI.SetActive(false);
            ToggleInteractableUI(false);
            TogglePickableUI(false);
        }
        else
        {
            UI.SetActive(false);
        }        
    }

    void HandleCameraRotation()
    {
        if (playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(Settings.Sprint);
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        float currentGravity = gravity;
        if (velocity.y < 0)
            currentGravity = gravity * 3f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void ToggleInteractableUI(bool state)
    {
        InteractableTextPlaceholder.SetActive(state);
    }

    public void TogglePickableUI(bool state)
    {
        PickableTextPlaceholder.SetActive(state);
    }
}