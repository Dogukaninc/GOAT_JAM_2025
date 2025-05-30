using _Main.Scripts.AI.Enemy.Controllers;
using _Main.Scripts.Interface;
using UnityEngine;
using UnityEngine.InputSystem;
using Scripts.GeneralSystems;

public class Player : MonoBehaviour, IDieable
{
    [field: SerializeField] public bool IsDead { get; set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float bulletSpeed = 10f;

    [SerializeField] private GameObject lantern;
    [SerializeField] private GameObject magic;
    [SerializeField] private GameObject bullet;

    private PlayerAnimationHandler _playerAnimationHandler;
    private float verticalVelocity;
    private float turnSmoothVelocity;
    private float lanternRadius = 10f;
    private Camera mainCamera;
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
        _playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
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
        mainCamera = Camera.main;
        cameraTransform = mainCamera.transform;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }
        
        return transform.position;
    }

    void Update()
    {
        // Handle movement
        Vector2 input = moveAction.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0f, input.y).normalized;

        // Handle gravity
        if (!controller.isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity = -0.5f;
        }

        // Rotate towards mouse position
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 directionToMouse = mousePosition - transform.position;
        directionToMouse.y = 0f; // Keep rotation only on Y axis
        
        if (directionToMouse != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothTime * 10f * Time.deltaTime);
        }

        // Handle movement
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f);
            controller.Move((moveDir.normalized * moveSpeed + verticalMove) * Time.deltaTime);
            _playerAnimationHandler.AnimateMovement(moveDir);
        }
        else
        {
            Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f);
            controller.Move(verticalMove * Time.deltaTime);
        }
    }

    private void OnLanternActionPerformed(InputAction.CallbackContext context){
        isLanternButtonHeld = !isLanternButtonHeld;
        lantern.GetComponent<Lantern>().OnLanternOn();
        animator.SetBool("IsLanternOn", isLanternButtonHeld);
    }


    private void OnShootActionPerformed(InputAction.CallbackContext context)
    {
        GameObject oldBullet = magic.transform.GetChild(0).gameObject;
        animator.SetTrigger("Shoot");
        oldBullet.transform.parent = null;
        oldBullet.GetComponent<Bullet>().Thrown();
        GameObject shootBullet = Instantiate(bullet,magic.transform.position,magic.transform.rotation);
        shootBullet.transform.SetParent(magic.transform);


    }

    void OnDestroy(){
        if (lanternAction != null)
        {
            lanternAction.started -= OnLanternActionPerformed;
            lanternAction.canceled -= OnLanternActionPerformed;
        }

        if (shootAction != null)
            shootAction.performed -= OnShootActionPerformed;
    }

    public void OnDead(){
    }

    public void OnRevive(){
    }

}