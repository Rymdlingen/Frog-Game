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

    [SerializeField] private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {

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

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = horizontalInput * transform.right + verticalInput * transform.forward;

        // transform.position += direction * Time.deltaTime * currentSpeed;

        playerRb.AddForce(direction, ForceMode.Impulse);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityJumpStart);
            currentGravity = gravityJumpStart;
            isJumping = true;
            isGrounded = false;
            Debug.Log("Jump!");
        }

        if (Input.GetKeyUp(KeyCode.Space) /*&& velocity.y > 0*/ && isJumping)
        {
            currentGravity = gravityJumpRealeas;
        }

        if (/*velocity.y < 0 && */isJumping)
        {
            currentGravity = gravityJumpFall;
        }

        // velocity.y += currentGravity * Time.deltaTime;


        // transform.position += velocity * Time.deltaTime;
    }
}
