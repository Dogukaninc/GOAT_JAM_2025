using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    private CharacterController controller;
    private Vector3 movement;
    private Transform cameraTransform;
    private PlayerInput playerInput;
    private InputAction moveAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Input
        Vector2 input = moveAction.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0f, input.y).normalized;

        if (movement.magnitude >= 0.1f)
        {
            // Calculate the angle to rotate towards
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            // Rotate the player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move in the direction we're facing
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
