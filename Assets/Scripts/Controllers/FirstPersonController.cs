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
    [SerializeField] bool canSprint = true;
    [SerializeField] bool canJump = true;
    [SerializeField] bool canCrouch = true;
    [SerializeField] bool canUseHeadbob = true;
    [SerializeField] bool willSlideOnSlopes = true;
    [SerializeField] bool canZoom = true;
    [SerializeField] bool canInteract = true;
    [SerializeField] bool useFootsteps = true;
    [SerializeField] bool useStamina = true;
    [SerializeField] bool useSpotlight = true;

    [Header("Controls")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField] KeyCode interactKey = KeyCode.E;
    [SerializeField] KeyCode spotlightKey = KeyCode.Q;

    [Header("Interactions")]
    [SerializeField] Vector3 interactionRayPoint = default;
    [SerializeField] float interactionDistance = default;
    [SerializeField] LayerMask interactionLayer = default;
    Interactable currentInteractable;

    [Header("Health")]
    [SerializeField] public float maxHealth = 100;
    [SerializeField] float timeBeforeRegen = 3;
    [SerializeField] float regenIncrementValue = 1;
    [SerializeField] float regenIncrementTime = 0.1f;
    public float currentHealth;
    Coroutine regeneratingHealth;
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
    [SerializeField] public float maxStamina = 100;
    [SerializeField] float staminaUseMultiplier = 5;
    [SerializeField] float timeBeforeStaminaRegenStarts = 5;
    [SerializeField] float depletedStaminaRegenTime = 10;
    [SerializeField] float staminaValueIncrement = 2;
    [SerializeField] float staminaTimeIncrement = 0.1f;
    public float currentStamina;
    Coroutine regeneratingStamina;
    public static Action<float> OnStaminaChange;
    float originalStaminaRegenTime;

    [Header("Moving")]
    [SerializeField] float walkSpeed = 3.0f;
    [SerializeField] float sprintSpeed = 6.0f;
    [SerializeField] float crouchSpeed = 1.5f;
    [SerializeField] float slopeSpeed = 8f;

    [Header("Looking")]
    [SerializeField, Range(1, 10)] float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 90)] float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 90)] float lowerLookLimit = 80.0f;

    [Header("Spotlight")]
    [SerializeField] GameObject spotlight;
    Light spotlightLight;

    [Header("Jumping")]
    [SerializeField] float gravity = 30.0f;
    [SerializeField] float jumpForce = 8.0f;

    [Header("Crouching")]
    [SerializeField] float crouchHeight = 0.65f;
    [SerializeField] float standingHeight = 2f;
    [SerializeField] float timeToCrouch = 0.25f;
    [SerializeField] Vector3 crouchingCenter = new Vector3(0, 0.65f, 0);
    [SerializeField] Vector3 standingCenter = new Vector3(0, 0, 0);
    bool isCrouching;
    bool duringCrouchAnimation;

    [Header("Headbob")]
    [SerializeField] float walkBobSpeed = 14f;
    [SerializeField] float walkBobAmount = 0.05f;
    [SerializeField] float sprintBobSpeed = 18f;
    [SerializeField] float sprintBobAmount = 0.11f;
    [SerializeField] float crouchBobSpeed = 8f;
    [SerializeField] float crouchBobAmount = 0.025f;
    float defaultYpos = 0f;
    float timer;

    [Header("Weapon Zoom")]
    [SerializeField] Animator weaponAnimator;
    [SerializeField] WeaponSway weaponSwayControl;
    [SerializeField] float timeToZoom = 0.3f;
    [SerializeField] float zoomFOV = 45f;
    float defaultFOV;
    Coroutine zoomRoutine;
    float weaponSwayOriginalIntensity;
    float weaponSwayOriginalSmooth;

    [Header("Footsteps")]
    [SerializeField] float baseStepSpeed = 0.5f;
    [SerializeField] float crouchStepMultiplier = 1.5f;
    [SerializeField] float sprintStepMultiplier = 0.6f;
    [SerializeField] AudioSource footstepAudioSource = default;
    [SerializeField] AudioClip[] grassClips = default;
    [SerializeField] AudioClip[] metalClips = default;
    [SerializeField] AudioClip[] woodClips = default;
    float footstepTimer = 0;
    float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : IsSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;

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

    Camera playerCamera;
    CharacterController characterController;

    Vector3 moveDirection;
    Vector2 currentInput;

    float rotationX = 0;

    public static FirstPersonController instance;
    #endregion

    void Awake()
    {
        instance = this;

        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYpos = playerCamera.transform.localPosition.y;
        defaultFOV = playerCamera.fieldOfView;

        spotlightLight = spotlight.GetComponent<Light>();

        currentHealth = maxHealth;
        currentStamina = maxStamina;
        originalStaminaRegenTime = timeBeforeStaminaRegenStarts;

        weaponSwayOriginalIntensity = weaponSwayControl.intensity;
        weaponSwayOriginalSmooth = weaponSwayControl.smooth;

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
            HandleSpotlight();

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

    private void HandleSpotlight()
    {
        if (Input.GetKeyUp(spotlightKey) && useSpotlight)
        {
            useSpotlight = false;
            spotlightLight.enabled = false;
        }
        else if (Input.GetKeyUp(spotlightKey) && !useSpotlight)
        {
            useSpotlight = true;
            spotlightLight.enabled = true;
        }
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
            weaponAnimator.SetBool("isAiming", true);
            weaponSwayControl.intensity = weaponSwayOriginalIntensity / 3;
            weaponSwayControl.smooth = weaponSwayOriginalSmooth / 3;

            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                weaponAnimator.SetBool("isAiming", false);
                weaponSwayControl.intensity = weaponSwayOriginalIntensity;
                weaponSwayControl.smooth = weaponSwayOriginalSmooth;
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        
        if (Input.GetKeyUp(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                weaponAnimator.SetBool("isAiming", false);
                weaponSwayControl.intensity = weaponSwayOriginalIntensity;
                weaponSwayControl.smooth = weaponSwayOriginalSmooth;
                zoomRoutine = null;
            }

            weaponAnimator.SetBool("isAiming", false);
            weaponSwayControl.intensity = weaponSwayOriginalIntensity;
            weaponSwayControl.smooth = weaponSwayOriginalSmooth;

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
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1.2f))
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
