using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewPhysicsCharacterController : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] private float speedWalk;
    [SerializeField] private float speedRun;
    // [SerializeField] private float jumpSpeed;
    private float currentSpeed;

    private Vector3 direction;

    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float jumpFallForce;
    [SerializeField] private float jumpCancelForce;
    bool isJumping;
    bool jumpCancelled;

    [SerializeField] float gravityModifier;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        #region Move player

        // Move player on ground plane.
        // Pick the correct speed.
        if (isJumping)
        {
            // Horizontal speed for drifting in directions while jumping.
            // currentSpeed = jumpSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Make input into a direction based on the rotation of the player.
        direction = horizontalInput * transform.right + verticalInput * transform.forward;
        direction.Normalize();

        #endregion Move Player

        #region Jump

        // If jump button is pressed ad upward force.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Calculate needed force to get to the desired height. (? probably wrong)
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y * gravityModifier));
            playerRb.AddForce(direction * currentSpeed + Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            jumpCancelled = false;
        }

        if (isJumping)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
        }

        Debug.Log(isJumping);

        #endregion Jump
    }

    private void FixedUpdate()
    {
        MovePlayer();


        if (isJumping && playerRb.velocity.y < 0)
        {
            playerRb.AddForce(Vector3.down * jumpFallForce);
        }

        if (jumpCancelled && playerRb.velocity.y > 0)
        {
            playerRb.AddForce(Vector3.down * jumpCancelForce);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}