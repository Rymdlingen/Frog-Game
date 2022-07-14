using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerController : MonoBehaviour
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

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.01f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    private bool isJumping;

    private float groundCheckTimer = 0.5f;
    private float countDown;
    private List<LineRenderer> listOfLines = new List<LineRenderer> { };

    Vector3 velocity;

    [SerializeField] LineRenderer jumpLine;

    private bool useLineView = false;

    [SerializeField] Camera lineCamera;
    [SerializeField] Camera frogCamera;

    [SerializeField] Transform lineViewFrogPosition;
    [SerializeField] Transform startFrogPosition;

    private void Start()
    {
        countDown = 0;
    }

    private void LateUpdate()
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

        transform.position += direction * Time.deltaTime * currentSpeed;

        // Check if character is on ground.
        if (countDown <= 0 && !isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        if (isGrounded)
        {
            velocity.y = -2f;
            isJumping = false;
            Debug.Log("Player is on ground!");
        }

        // Keep player on ground.
        Vector3 offSet = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z);
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity);
        Debug.DrawLine(offSet, hit.point, Color.blue, 2f);


        // Camera modes

        if (Input.GetKeyDown(KeyCode.M))
        {
            useLineView = !useLineView;
        }

        if (useLineView)
        {
            if (lineCamera.enabled == false)
            {
                lineCamera.enabled = true;
                frogCamera.enabled = false;
                transform.position = lineViewFrogPosition.position;
            }

            if (isJumping)
            {
                listOfLines.Add(Instantiate(jumpLine, offSet, transform.rotation));

                if (listOfLines.Count > 2000)
                {
                    Destroy(listOfLines[0]);
                    listOfLines.RemoveAt(0);
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                for (int i = 0; i < listOfLines.Count; i++)
                {
                    Destroy(listOfLines[i]);
                }

                transform.position = lineViewFrogPosition.position;
                listOfLines.RemoveRange(0, listOfLines.Count);
            }
        }
        else
        {
            if (frogCamera.enabled == false)
            {
                frogCamera.enabled = true;
                lineCamera.enabled = false;
                transform.position = startFrogPosition.position;
            }
        }



        if (!isJumping)
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + transform.lossyScale.y / 2, transform.position.z);
        }
        else
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityJumpStart);
            currentGravity = gravityJumpStart;
            isJumping = true;
            isGrounded = false;
            countDown = groundCheckTimer;
            Debug.Log("Jump!");
        }

        if (Input.GetKeyUp(KeyCode.Space) && velocity.y > 0 && isJumping)
        {
            currentGravity = gravityJumpRealeas;
        }

        if (velocity.y < 0 && isJumping)
        {
            currentGravity = gravityJumpFall;
        }

        velocity.y += currentGravity * Time.deltaTime;


        transform.position += velocity * Time.deltaTime;

    }




    /*
        [SerializeField] float walkSpeed = 10;
        [SerializeField] float runSpeed = 15;
        private float currentSpeed;

        private float verticalInput;
        private float horizontalInput;

        [SerializeField] float jumpHight = 10f;
        [SerializeField] float jumpTime;
        [SerializeField] float gravityStart = -9.81f;
        [SerializeField] float gravityRealease;
        [SerializeField] float gravityDown = -9.81f;

        [SerializeField] bool isJumping = false;
        [SerializeField] bool isGrounded = false;
        [SerializeField] float currentGravity;

        Vector3 velocity;

        // Start is called before the first frame update
        void Start()
        {
            currentGravity = gravityDown;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            #region Move
            // Set the correct speed of the player, if the player is holding shift use run speed, if not use walk speed.
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && currentSpeed != runSpeed)
            {
                currentSpeed = runSpeed;
            }
            else
            {
                if (currentSpeed != walkSpeed)
                {
                    currentSpeed = walkSpeed;
                }
            }

            // Save movement input.
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            // Move the character, based on the characters rotation (that is based on the cameras rotation).
            Vector3 direction = horizontalInput * transform.right + verticalInput * transform.forward;


            #endregion Move


            #region Jump

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Jump
                // Use gravityStart
                currentGravity = gravityStart;
                Vector3 verticalMovement = new Vector3(0, currentGravity * jumpHight * Time.deltaTime, 0);
                transform.position += verticalMovement * Time.deltaTime * currentSpeed;
                isJumping = true;
                isGrounded = false;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Use gravityRelease
            }

            // if (moving downwards)
            // {
            //      Use gravityDown
            // }

            // double jump

            // Pull the caharacter down (use gravity).
            if (isGrounded)
            {
                velocity.y = -2f;
            }
            else
            {
                currentGravity = gravityDown;
            }

            //direction.y = currentGravity;
            transform.position += direction * Time.deltaTime * currentSpeed;

            #endregion Jump


            // Check if the character is on the ground.
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity);
            Debug.DrawLine(transform.position, hit.point, Color.blue);

            // Keep the character on the ground.
            if (true)
            {
                //  rigidbody.useGravity = false;
                velocity.y += currentGravity * Time.deltaTime;

                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                /*
                if (jumpUpTimer > 0)
                {

                    jumpUpTimer -= Time.deltaTime;
                }
                else
                {
                    // isJumping = false;
                }
                *
            }

            if (isGrounded)
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + transform.lossyScale.y / 2, transform.position.z);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Floor"))
            {
                isJumping = false;
                isGrounded = true;
            }
        }*/
}
