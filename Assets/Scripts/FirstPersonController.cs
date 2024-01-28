using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class FirstPersonController : MonoBehaviour
{
    #region Initialization
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey) && Input.GetAxis("Vertical") > 0;
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded && !IsSliding;
    private bool ShouldCrouch => (Input.GetKeyDown(crouchKey) || Input.GetKeyUp(crouchKey)) && !duringCrouchAnimation && characterController.isGrounded;

    [Header("Functions")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool willSlideOnSlopes = true;
    [SerializeField] private bool canZoom = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useFootsteps = true;
    [SerializeField] private bool useStamina = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("Interactions")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float timeBeforeRegen = 3;
    [SerializeField] private float regenIncrementValue = 1;
    [SerializeField] private float regenIncrementTime = 0.1f;
    private float currentHealth;
    private Coroutine regeneratingHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;
    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
    }

    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaUseMultiplier = 5;
    [SerializeField] private float timeBeforeStaminaRegenStarts = 5;
    [SerializeField] private float depletedStaminaRegenTime = 10;
    [SerializeField] private float staminaValueIncrement = 2;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private float currentStamina;
    private Coroutine regeneratingStamina;
    public static Action<float> OnStaminaChange;
    private float originalStaminaRegenTime;

    [Header("Moving")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeSpeed = 8f;

    [Header("Looking")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 90)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 90)] private float lowerLookLimit = 80.0f;

    [Header("Jumping")]
    [SerializeField] private float gravity = 30.0f;
    [SerializeField] private float jumpForce = 8.0f;

    [Header("Crouching")]
    [SerializeField] private float crouchHeight = 0.65f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.65f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Headbob")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYpos = 0f;
    private float timer;

    [Header("Weapon Zoom")]
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomFOV = 45f;
    private float defaultFOV;
    private Coroutine zoomRoutine;

    [Header("Footsteps")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] woodClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : IsSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;

    // Sliding
    private Vector3 hitPointNormal;
    private bool IsSliding
    {
        get
        {
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    public static FirstPersonController instance;
    #endregion

    void Awake()
    {
        instance = this;

        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYpos = playerCamera.transform.localPosition.y;
        defaultFOV = playerCamera.fieldOfView;

        currentHealth = maxHealth;
        currentStamina = maxStamina;
        originalStaminaRegenTime = timeBeforeStaminaRegenStarts;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        OnDamage?.Invoke(currentHealth);
        OnStaminaChange?.Invoke(currentStamina);
    }

    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump)
                HandleJump();

            if (canCrouch)
                HandleCrouch();

            if (canUseHeadbob)
                HandleHeadbob();

            if (canZoom)
                HandleZoom();

            if (useFootsteps)
                Handle_Footsteps();

            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            if (useStamina)
                HandleStamina();

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"),
            (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection = moveDirection.normalized * Mathf.Clamp(moveDirection.magnitude, 0, (IsSprinting ? sprintSpeed : walkSpeed));

        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        // Look Up & Down
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Look Left & Right
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }

    private void HandleHeadbob()
    {
        if (!characterController.isGrounded) return;

        if (Mathf.Abs(new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude) > 0.1f || (Mathf.Abs(defaultYpos - playerCamera.transform.localPosition.y) > 0.005f)) //(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, defaultYpos + Mathf.Sin(timer) *
                (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount), playerCamera.transform.localPosition.z);
        }
        else if (playerCamera.transform.localPosition.y != defaultYpos)
        {
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, defaultYpos, playerCamera.transform.localPosition.z);
        }

    }

    private void HandleStamina()
    {
        if (IsSprinting && !isCrouching && currentInput != Vector2.zero) // && characterController.isGrounded
        {
            if (regeneratingStamina != null)
            {
                StopCoroutine(regeneratingStamina);
                regeneratingStamina = null;
            }

            currentStamina -= staminaUseMultiplier * Time.deltaTime;

            if (currentStamina < 0)
                currentStamina = 0;

            if (currentStamina < 10)
            {
                timeBeforeStaminaRegenStarts = depletedStaminaRegenTime;
            }

            if (currentStamina > 10)
            {
                timeBeforeStaminaRegenStarts = originalStaminaRegenTime;
            }

            OnStaminaChange?.Invoke(currentStamina);

            if (currentStamina <= 0)
                canSprint = false;
        }

        if (!IsSprinting && currentStamina < maxStamina && regeneratingStamina == null)
        {
            regeneratingStamina = StartCoroutine(RegenerateStamina());
        }
    }

    private void HandleZoom()
    {
        if(Input.GetKeyDown(zoomKey))
        {
            if(zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        
        if (Input.GetKeyUp(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }

    private void HandleInteractionCheck()
    {
        if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if(hit.collider.gameObject.layer == 7 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.gameObject.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if(currentInteractable)
                    currentInteractable.OnFocus();
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractable.OnInteract();
        }
    }

    private void ApplyDamage(float dmg)
    {
        currentHealth -= dmg;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0)
            KillPlayer();
        else if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);

        regeneratingHealth = StartCoroutine(RegenerateHealth());
    }

    private void KillPlayer()
    {
        currentHealth = 0;
        OnDamage(currentHealth);

        if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);

        print("Player Died");
    }


    private void ApplyFinalMovements()
    { 
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        if (willSlideOnSlopes && IsSliding)
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        if (characterController.velocity.y < -1 && characterController.isGrounded)
            moveDirection.y = 0;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Handle_Footsteps()
    {
        if (!characterController.isGrounded) return;
        if (currentInput == Vector2.zero) return;

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            footstepAudioSource.pitch = UnityEngine.Random.Range(0.65f, 0.9f);
            
            if (Physics.Raycast(characterController.transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch(hit.collider.tag)
                {
                    case "Footsteps/Wood":
                        footstepAudioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "Footsteps/Metal":
                        footstepAudioSource.PlayOneShot(metalClips[UnityEngine.Random.Range(0, metalClips.Length - 1)]);
                        break;
                    case "Footsteps/Grass":
                        footstepAudioSource.PlayOneShot(grassClips[UnityEngine.Random.Range(0, grassClips.Length - 1)]);
                        break;
                    default:
                        footstepAudioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;
                }
            }

            footstepTimer = GetCurrentOffset;
        }
    }

    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        isCrouching = !isCrouching;

        while(timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        duringCrouchAnimation = false;
    }

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defaultFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while(timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(timeBeforeRegen);
        WaitForSeconds timeToWait = new WaitForSeconds(regenIncrementTime);

        while (currentHealth < maxHealth)
        {
            currentHealth += regenIncrementValue;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            OnHeal?.Invoke(currentHealth);
            yield return timeToWait;
        }

        regeneratingHealth = null;
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds (timeBeforeStaminaRegenStarts);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while (currentStamina < maxStamina)
        {
            if (currentStamina > 0)
                canSprint = true;

            currentStamina += staminaValueIncrement;

            if (currentStamina > maxStamina)
                currentStamina = maxStamina;

            OnStaminaChange?.Invoke(currentStamina);

            yield return timeToWait;
        }

        regeneratingStamina = null;
    }
}
