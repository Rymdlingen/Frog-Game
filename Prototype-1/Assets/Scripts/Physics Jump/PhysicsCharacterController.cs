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
    [SerializeField] private float gravityJumpRelease;
    [SerializeField] private float gravityJumpFall;
    private float gravityModifier;

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
    float lastDistance;

    float wantedYVelocity;

    RaycastHit hit;

    // Testing the jump from the article.
    public float gravityScale = 5;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        heightOfPlayerModel = transform.Find("Frog").GetComponent<BoxCollider>().size.y;
        goalYPositionOffset = (heightOfPlayerModel - transform.GetComponent<CapsuleCollider>().height) / 2;
        rideHeight = heightOfPlayerModel / 2;
        gravityModifier = gravityJumpStart;
    }

    // Update is called once per frame
    void Update()
    {


        /*
        if (!isJumping)
        {
            playerRb.AddForceAtPosition(rayDirection * springForce, new Vector3(hit.point.x, goalY, hit.point.z));
        }*/

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
        // Pick the correct speed.
        if (isJumping)
        {
            // Horizontal speed for drifting in directions while jumping.
            currentSpeed = jumpSpeed;
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

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            gravityModifier = gravityJumpStart;
            wantedYVelocity = Mathf.Sqrt(-2 * Physics.gravity.y + gravityModifier * jumpHeight);

            playerRb.AddForce(transform.up * gravityModifier, ForceMode.Impulse);
            playerRb.AddForce(Physics.gravity * (gravityScale - 1) * playerRb.mass);

            isJumping = true;
            isGrounded = false;
            // Debug.Log("Start jump!");
        }

        if (Input.GetKeyUp(KeyCode.Space) && playerRb.velocity.y > 0 && isJumping)
        {
            gravityModifier = gravityJumpRelease;
            wantedYVelocity = Mathf.Sqrt(-2 * gravityModifier * jumpHeight);

            // Debug.Log("Release jump!");

        }

        if (/*velocity.y < 0 && */ playerRb.velocity.y < 0 && isJumping)
        {
            gravityModifier = gravityJumpFall;
            // playerRb.AddForce(transform.up * -currentGravity, ForceMode.Impulse);

            // Debug.Log("Falling!");

        }

        // velocity.y += currentGravity * Time.deltaTime;


        // transform.position += velocity * Time.deltaTime;
    }

    private void FixedUpdate()
    {

        Physics.Raycast(playerRb.position + playerCollider.center, Vector3.down, out hit, heightOfPlayerModel);
        //playerRb.SweepTest(Vector3.down, out hit, heightOfPlayerModel);
        Debug.DrawRay(playerRb.position + playerCollider.center, Vector3.down, Color.blue, 1f);

        MovePlayer();

        /*
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

        float x = hit.distance;
        theXfloat = x;

        float springForce = (x * rideSpringStrenght) - (relativeVelocity * rideSpringDamper);

        // Debug.Log(Time.time.ToString("00:00:00") + " springForce " + springForce);
        
        if (Physics.Raycast(playerRb.position + playerCollider.center, transform.TransformDirection(-Vector3.up), out hit, heightOfPlayerModel))
        {
            // Debug.Log(Time.time.ToString("00:00:00") + "the raycast hit");

            float forceAmount = 0;

            forceAmount = springForce * (hit.distance) / rideHeight + rideSpringDamper * (hit.distance - lastDistance);
            // Debug.Log(Time.time.ToString("00:00:00") + "forceAmount " + forceAmount);
            playerRb.AddForceAtPosition(transform.up * forceAmount, transform.position);

            playerRb.AddForce(Physics.gravity * (forceAmount - 1) * playerRb.mass);

            lastDistance = hit.distance;

            // Debug.Log("hit normal: " + hit.normal);

        }
        else
        {
            lastDistance = rideHeight;
        }*/
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
        // RaycastHit sweepHit;
        // playerRb.SweepTest(-transform.up, out sweepHit); 
        // Physics.Raycast(playerRb.position + (direction * GetComponent<CapsuleCollider>().radius), Vector3.down, out sweepHit);
        // Debug.DrawLine(playerRb.position + (direction * GetComponent<CapsuleCollider>().radius), sweepHit.point, Color.black, 1f);

        // Debug.DrawLine(sweepHit.point, playerRb.position, Color.red, 2f);

        // direction = Vector3.ProjectOnPlane(direction, sweepHit.normal);
        // direction.Normalize();
        playerRb.AddForce(direction * currentSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        ClampMovementSpeed();

        currentVelocity = playerRb.velocity;


    }

    private void ClampMovementSpeed()
    {

        playerRb.velocity = new Vector3(Mathf.Clamp(playerRb.velocity.x, -currentSpeed / 10, currentSpeed / 10), playerRb.velocity.y, Mathf.Clamp(playerRb.velocity.z, -currentSpeed / 10, currentSpeed / 10));

    }
}
