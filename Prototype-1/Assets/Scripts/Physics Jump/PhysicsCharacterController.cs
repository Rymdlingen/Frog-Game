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
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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

        /* if (verticalInput == 0)
         {
             playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y, 0);
         }*/

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
    }
}
