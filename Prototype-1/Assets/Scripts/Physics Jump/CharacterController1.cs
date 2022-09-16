using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController1 : MonoBehaviour
{
    [Header("References")]
    public Renderer playerRenderer;
    public Transform playerCamera;

    [Header("Gravity Force")]
    public float gravityForce = 9.8f;
    public float floorDistanceWhereGravityStartsToSlowDown = 1.0f;
    public AnimationCurve gravityCurve;

    [Header("Ride height spring")]
    public float floorDistanceWhereSpringForceIsMaximal = 0.02f;
    public float maxSpringForce = 100;
    public AnimationCurve springCurve;

    [Header("Character controller settings")]
    // public float jumpForce = 10f;
    // public float moveSpeed = 5f;
    public float targetRideHeight = 0.1f;
    [Space]
    public float groundCheckDistance;

    private Rigidbody playerRb;
    private CapsuleCollider playerCollider;

    private Vector3 rendererTargetScale;
    private Quaternion rendererTargetRotation;

    private bool grounded;

    [Header("Horizontal movement")]
    [SerializeField] private float speedWalk;
    [SerializeField] private float speedRun;
    private float currentSpeed;

    private Vector3 direction;

    float horizontalInput;
    float verticalInput;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float jumpFallForce;
    [SerializeField] private float jumpCancelForce;
    bool isJumping;
    bool jumpCancelled;

    [SerializeField] float gravityModifier;



    [SerializeField]
    private GameModeScriptableObject gameModeManager;
    bool inExplorationMode = true;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        grounded = false;
        isJumping = false;
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        targetRideHeight = (GetComponentInChildren<BoxCollider>().size.y - playerCollider.height) / 2;
        groundCheckDistance = playerCollider.height / 2 + targetRideHeight + 0.1f;

        gameModeManager.modeChangeEvent.AddListener(CanMove);
    }

    private void CanMove(GameModes currentGameMode)
    {
        if (currentGameMode == GameModes.Explore)
        {
            inExplorationMode = true;
        }
        else if (currentGameMode == GameModes.Map)
        {
            inExplorationMode = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inExplorationMode)
        {
            #region Move

            // Move player on ground plane.
            // Pick the correct speed.
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (currentSpeed != speedRun)
                {
                    currentSpeed = speedRun;
                }
            }
            else
            {
                currentSpeed = speedWalk;
            }

            // Get movement input from player.
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // Make input into a direction based on the rotation of the player.
            direction = horizontalInput * playerCamera.right + verticalInput * new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized;
            direction.Normalize();

            #endregion Move

            #region Jump

            // If jump button is pressed ad upward force.
            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                // Calculate needed force to get to the desired height. (? probably wrong)
                float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y * gravityModifier));
                playerRb.AddForce(direction * currentSpeed + Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
                jumpCancelled = false;
                grounded = false;
            }

            if (isJumping)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    jumpCancelled = true;
                }
            }

            Debug.Log("isJumping = " + isJumping);

            /*
            if (grounded && Input.GetButtonDown("Jump"))
            {
                grounded = false;
                playerRb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            }
            */

            #endregion Jump
        }
    }

    private void FixedUpdate()
    {

        grounded = GetGroundInfo(out float distanceToGround);
        if (grounded)
        {
            // Gravity
            float gravityFactor = 1.0f; // gravity factor goes from 0 to 1 when character is above rideHeight
            if (distanceToGround <= targetRideHeight)
            {
                gravityFactor = 0;
            }
            else if (distanceToGround <= floorDistanceWhereGravityStartsToSlowDown)
            {
                gravityFactor = (distanceToGround - targetRideHeight) / (floorDistanceWhereGravityStartsToSlowDown - targetRideHeight);
            }
            gravityFactor = Mathf.Clamp(gravityFactor, 0, 1);
            gravityFactor = gravityCurve.Evaluate(gravityFactor);
            ApplyGravityWithFactor(gravityFactor);

            // "Spring" to keep character above the ground
            float springFactor = 0.0f; // spring factor is 1 when character touches the ground, and is zero when character is exactly at rideHeight
            if (distanceToGround <= floorDistanceWhereSpringForceIsMaximal)
            {
                springFactor = 1.0f;
            }
            else if (distanceToGround < targetRideHeight)
            {
                springFactor = (targetRideHeight - distanceToGround) / (targetRideHeight - floorDistanceWhereSpringForceIsMaximal);
            }
            springFactor = Mathf.Clamp(springFactor, 0, 1);
            springFactor = springCurve.Evaluate(springFactor);
            ApplySpringForceWithFactor(springFactor);
        }
        else if (isJumping)
        {
            // Do the jump thing
            if (playerRb.velocity.y < 0)
            {
                playerRb.AddForce(Vector3.down * jumpFallForce, ForceMode.Force);
            }

            if (jumpCancelled && playerRb.velocity.y > 0)
            {
                playerRb.AddForce(Vector3.down * jumpCancelForce, ForceMode.Force);
            }
        }
        else
        {
            // No ground nowhere, standard gravity
            ApplyGravityWithFactor(1.0f);

        }

        if (inExplorationMode)
        {

            #region Move
            MovePlayer();

            /*
            // Horizontal movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            playerRb.AddForce(moveSpeed * horizontalInput * playerCamera.right);
            playerRb.AddForce(moveSpeed * verticalInput * playerCamera.forward);
            */

            #endregion Move

            // Set new target renderer rotation and scale
            rendererTargetRotation = Quaternion.Euler(-10 * direction.magnitude, Vector3.SignedAngle(Vector3.forward, direction, Vector3.up), 0);
            if (!grounded)
            {
                float distanceToGroundFactor = distanceToGround / 3.0f;
                float yScale = 1 + (distanceToGroundFactor - targetRideHeight);
                yScale = Mathf.Clamp(yScale, 0.6f, 1.7f);
                float horizontalScale = 1 - (yScale - 1);
                rendererTargetScale = new Vector3(horizontalScale, yScale, horizontalScale);
            }
            else
            {
                rendererTargetScale = Vector3.one;
            }

            #region Jump



            #endregion Jump

            if (grounded && direction.magnitude < 0.01f)
            {
                playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
            }
        }
    }

    private void LateUpdate()
    {
        if (inExplorationMode)
        {
            // set renderer actual rotation and scale
            playerRenderer.transform.localScale = Vector3.Lerp(playerRenderer.transform.localScale, rendererTargetScale, 0.1f);

            if (direction.magnitude > 0.01f)
            {
                playerRenderer.transform.localRotation = Quaternion.Lerp(playerRenderer.transform.localRotation, rendererTargetRotation, 0.01f);
            }
            else
            {
                playerRenderer.transform.localRotation = Quaternion.Lerp(playerRenderer.transform.localRotation, Quaternion.Euler(rendererTargetRotation.eulerAngles.x, playerRenderer.transform.localRotation.eulerAngles.y, rendererTargetRotation.eulerAngles.z), 0.1f);
            }
        }
    }

    private void MovePlayer()
    {
        playerRb.AddForce(direction * currentSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        ClampMovementSpeed();
    }

    private void ClampMovementSpeed()
    {
        playerRb.velocity = new Vector3(Mathf.Clamp(playerRb.velocity.x, -currentSpeed / 10, currentSpeed / 10), playerRb.velocity.y, Mathf.Clamp(playerRb.velocity.z, -currentSpeed / 10, currentSpeed / 10));
    }

    private bool GetGroundInfo(out float distance)
    {
        bool isGround = false;
        distance = 0;
        float distanceToTheGround = -1; // no ground
        float sphereRadius = playerCollider.radius * 0.1f;
        if (Physics.SphereCast(this.transform.position, sphereRadius, Vector3.down, out RaycastHit hit, groundCheckDistance))
        {
            // found the ground!
            distance = hit.distance - (playerCollider.height / 2.0f);
            DebugMessageWithTimestamp("Raycast hit with distance to the ground = " + distanceToTheGround);
            isGround = true;
            // This need to be fixed! Now is jumping stays on forever, I need a better check for if player is grounded.
            //isJumping = false;
        }
        Debug.Log("grounded = " + grounded);
        return isGround;
    }

    private void ApplyGravityWithFactor(float gravityFactor)
    {
        float gForce = gravityFactor * gravityForce;
        DebugMessageWithTimestamp("gravityForce = " + gravityForce * gravityFactor);
        playerRb.AddForce(-gForce * Vector3.up, ForceMode.Force);
    }

    private void ApplySpringForceWithFactor(float springFactor)
    {
        float springForce = springFactor * maxSpringForce;
        DebugMessageWithTimestamp("springForce = " + springForce);
        playerRb.AddForce(springForce * Vector3.up);
    }

    private void DebugMessageWithTimestamp(string message)
    {
        //Debug.Log(Time.time.ToString("0.00") + ": " + message);
    }
}
