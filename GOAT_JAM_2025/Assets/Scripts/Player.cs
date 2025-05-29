using _Main.Scripts.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour,IDieable
{
    public bool IsDead { get; set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    private float verticalVelocity;
    private float turnSmoothVelocity;
    
    private Animator animator;
    private CharacterController controller;
    private Vector3 movement;
    private Transform cameraTransform;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction lanternAction;
    private bool isLanternOn;
    private bool isLanternButtonHeld;
        
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        shootAction = playerInput.actions["Shoot"];
        lanternAction = playerInput.actions["Lantern"];
        lanternAction.started += OnLanternActionPerformed;
        lanternAction.canceled += OnLanternActionPerformed;
        shootAction.performed += OnShootActionPerformed;
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    
    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0f, input.y).normalized;


        if (!controller.isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity = -0.5f;
        }

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f);
            controller.Move((moveDir.normalized * moveSpeed + verticalMove) * Time.deltaTime);
        }
        else
        {
            Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f);
            controller.Move(verticalMove * Time.deltaTime);
        }
    }

    private void OnLanternActionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Lantern action performed");
        isLanternButtonHeld = !isLanternButtonHeld;
        animator.SetBool("IsLanternOn", isLanternButtonHeld);
    }



    private void OnShootActionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot action performed");
        animator.SetTrigger("Shoot");
    }

    void OnDestroy()
    {
        if (lanternAction != null)
        {
            lanternAction.started -= OnLanternActionPerformed;
            lanternAction.canceled -= OnLanternActionPerformed;
        }
        if (shootAction != null)
            shootAction.performed -= OnShootActionPerformed;
    }

    public void OnDead()
    {
    }

    public void OnRevive()
    {
    }
}
