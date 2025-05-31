using System.Collections;
using _Main.Scripts.AI.Enemy.Controllers;
using _Main.Scripts.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDieable
{
    [field: SerializeField] public bool IsDead { get; set; }

    [Header("Ik Arm Hold Settings")] [SerializeField]
    private Transform leftHandGrip;

    [SerializeField] private Vector3 holdPos;
    [SerializeField] private Vector3 unholdPos;

    [Header("Weapon Settings")] [SerializeField]
    private float reloadTime = 1f;

    public Transform mouseTargetPos;

    [Header("Lantern Settings")] [SerializeField]
    private MeshRenderer lanternMesh;


    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float bulletSpeed = 10f;

    [SerializeField] private GameObject lantern;
    [SerializeField] private GameObject weaponMuzzle;
    [SerializeField] private GameObject bullet;

    private PlayerAnimationHandler _playerAnimationHandler;
    private float verticalVelocity;
    private float turnSmoothVelocity;
    private float lanternRadius = 10f;
    private Camera mainCamera;
    private Animator animator;
    public Animator croosBowAnimator;
    private CharacterController controller;
    private Vector3 movement;
    private Transform cameraTransform;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction lanternAction;
    private bool isLanternOn;
    private bool isLanternButtonHeld;
    private Vector3 moveDir;

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
        lanternAction.canceled += OnLanternActionCanceled;
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
            mouseTargetPos.position = ray.GetPoint(rayDistance);
            return ray.GetPoint(rayDistance);
        }

        return transform.position;
    }

    void Update()
    {
        // Handle movement input
        Vector2 input = moveAction.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0f, input.y).normalized;

        // Gravity
        if (!controller.isGrounded)
            verticalVelocity += gravity * Time.deltaTime;
        else
            verticalVelocity = -0.5f;

        // Rotate towards mouse
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 directionToMouse = mousePosition - transform.position;
        directionToMouse.y = 0f;
        if (directionToMouse != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothTime * 10f * Time.deltaTime);
        }

        moveDir = Vector3.zero; // Hatalı yön aktarımını önlemek için HER FRAME sıfırla!

        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f);
            controller.Move((moveDir.normalized * moveSpeed + verticalMove) * Time.deltaTime);
        }
        else
        {
            Vector3 verticalMove = new Vector3(0f, verticalVelocity, 0f);
            controller.Move(verticalMove * Time.deltaTime);
        }

        _playerAnimationHandler.AnimateMovement(moveDir);
    }

    private void OnLanternActionPerformed(InputAction.CallbackContext context)
    {
        isLanternButtonHeld = !isLanternButtonHeld;
        lantern.GetComponent<Lantern>().OnLanternOn();
        animator.SetBool("IsLanternOn", isLanternButtonHeld);
        lanternMesh.enabled = true;
        HoldLantern();
    }

    private void OnLanternActionCanceled(InputAction.CallbackContext context)
    {
        isLanternButtonHeld = false;
        lantern.GetComponent<Lantern>().OnLanternOff();
        lanternMesh.enabled = false;
        animator.SetBool("IsLanternOn", isLanternButtonHeld);
        UnHoldLantern();
    }

    private void HoldLantern()
    {
        leftHandGrip.transform.localPosition = Vector3.Lerp(leftHandGrip.transform.localPosition, holdPos, 0.5f);
    }

    private void UnHoldLantern()
    {
        leftHandGrip.transform.localPosition = Vector3.Lerp(leftHandGrip.transform.localPosition, unholdPos, 0.5f);
    }

    private void OnShootActionPerformed(InputAction.CallbackContext context)
    {
        if (weaponMuzzle.transform.childCount <= 0) return;

        GameObject oldBullet = weaponMuzzle.transform.GetChild(0).gameObject;
        croosBowAnimator.SetTrigger("Shoot");
        oldBullet.transform.parent = null;
        oldBullet.GetComponent<Bullet>().Thrown();
        StartCoroutine(ArrowReloadDelay());
    }

    IEnumerator ArrowReloadDelay()
    {
        yield return new WaitForSeconds(reloadTime);
        GameObject shootBullet = Instantiate(bullet, weaponMuzzle.transform.position, weaponMuzzle.transform.rotation);
        shootBullet.transform.SetParent(weaponMuzzle.transform);
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