using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCharacterController : MonoBehaviour
{
    [SerializeField] private float speedWalk;
    [SerializeField] private float speedRun;
    [SerializeField] private float jumpSpeed;
    private float currentSpeed;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityJumpStart;
    [SerializeField] private float gravityJumpRealeas;
    [SerializeField] private float gravityJumpFall;
    private float currentGravity;

    private bool isJumping;
    private bool isGrounded;
    private Rigidbody playerRb;
    private CapsuleCollider playerCollider;
    private Vector3 direction;

    [SerializeField] Vector3 currentVelocity;

    private float heightOfPlayerModel;
    private float goalYPositionOffset;

    [SerializeField] private float rideHeight;
    [SerializeField] private float rideSpringStrenght;
    [SerializeField] private float rideSpringDamper;

    float theXfloat;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        heightOfPlayerModel = transform.Find("Frog").GetComponent<BoxCollider>().size.y;
        goalYPositionOffset = (heightOfPlayerModel - transform.GetComponent<CapsuleCollider>().height) / 2;
        rideHeight = heightOfPlayerModel / 2;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float deltaTime = Time.deltaTime;

        Physics.Raycast(playerRb.position + playerCollider.center, Vector3.down, out hit, heightOfPlayerModel);
        Debug.DrawRay(playerRb.position + playerCollider.center, Vector3.down, Color.blue, 1f);

        Vector3 playerVelocity = playerRb.velocity;
        Vector3 rayDirection = transform.TransformDirection(-transform.up);

        Vector3 otherVelocity = Vector3.zero;
        Rigidbody hitBody = hit.rigidbody;
        if (hitBody != null)
        {
            otherVelocity = hitBody.velocity;
        }

        float rayDirectionVelocity = Vector3.Dot(rayDirection, playerVelocity);
        float otherDirectionVelocity = Vector3.Dot(rayDirection, otherVelocity);

        float relativeVelocity = rayDirectionVelocity - otherDirectionVelocity;


        float x = hit.distance - rideHeight;
        theXfloat = x;

        float springForce = (x * rideSpringStrenght) - (relativeVelocity * rideSpringDamper);

        if (!isJumping)
        {
            playerRb.AddForce(rayDirection * springForce);
        }

        // Spring that keep player on ground
        if (!isJumping)
        {





            /*
            float playerYPosition = transform.position.y;
            float playerYVelocity = playerRb.velocity.y;

            CalculateDampedSpring(ref playerYPosition,
                ref playerYVelocity,
                transform.position.y + goalYPositionOffset,
                deltaTime, 0, 0);
            */

        }











        #region Move player

        // Move player on ground plane.
        if (isJumping)
        {
            currentSpeed = jumpSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = speedRun;
        }
        else
        {
            currentSpeed = speedWalk;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        direction = horizontalInput * transform.right + verticalInput * transform.forward;
        direction.Normalize();

        #endregion Move Player

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityJumpStart);
            currentGravity = gravityJumpStart;

            playerRb.AddForce(transform.up * currentGravity, ForceMode.Impulse);

            isJumping = true;
            isGrounded = false;
            Debug.Log("Start jump!");
        }

        if (Input.GetKeyUp(KeyCode.Space) /*&& velocity.y > 0*/ && playerRb.velocity.y > 0 && isJumping)
        {
            currentGravity = gravityJumpRealeas;
            // playerRb.AddForce(transform.up * currentGravity, ForceMode.Impulse);

            Debug.Log("Release jump!");

        }

        if (/*velocity.y < 0 && */ playerRb.velocity.y < 0 && isJumping)
        {
            currentGravity = gravityJumpFall;
            // playerRb.AddForce(transform.up * -currentGravity, ForceMode.Impulse);

            Debug.Log("Falling!");

        }

        // velocity.y += currentGravity * Time.deltaTime;


        // transform.position += velocity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void MovePlayer()
    {
        playerRb.AddForce(direction * currentSpeed * Time.deltaTime, ForceMode.VelocityChange);
        ClampMovementSpeed();

        currentVelocity = playerRb.velocity;
    }

    private void ClampMovementSpeed()
    {

        playerRb.velocity = new Vector3(Mathf.Clamp(playerRb.velocity.x, -currentSpeed / 10, currentSpeed / 10), playerRb.velocity.y, Mathf.Clamp(playerRb.velocity.z, -currentSpeed / 10, currentSpeed / 10));

    }

    public static void CalculateDampedSpring(ref float position, ref float velocity, float goalPosition, float deltaTime, float frequency, float damping)
    {
        if (position != goalPosition)
        {

        }
    }
}
