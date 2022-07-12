using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float currentSpeed;

    Rigidbody rigidbody;

    [SerializeField] private float jump;

    bool isJumping = false;
    float jumpUpTimer;

    UI UIScript;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        rigidbody = GetComponent<Rigidbody>();

        UIScript = GameObject.Find("Canvas").GetComponent<UI>();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
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
        if (verticalInput != 0)
        {
            Vector3 forward = verticalInput * transform.forward * Time.deltaTime * currentSpeed;
            // controller.Move(forward * Time.deltaTime * currentSpeed);
            transform.position += forward;
        }

        if (horizontalInput != 0)
        {
            Vector3 sideways = horizontalInput * transform.right * Time.deltaTime * currentSpeed;
            // controller.Move(sideways * Time.deltaTime * currentSpeed);
            transform.position += sideways;
        }

        if (verticalInput == 0 && horizontalInput == 0 && !isJumping)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rigidbody.AddForce(Vector3.up * jump, ForceMode.Impulse);

            isJumping = true;
            jumpUpTimer = 1f;
        }

        if (isJumping)
        {
            Vector3 up = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, jump, jumpUpTimer), transform.position.z);
            //controller.Move(up);

            rigidbody.useGravity = true;
        }

        // Check if the character is on the ground.
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity);
        Debug.DrawLine(transform.position, hit.point, Color.blue);

        // Keep the character on the ground.
        if (!isJumping)
        {
            rigidbody.useGravity = false;
            transform.position = new Vector3(transform.position.x, hit.point.y + transform.lossyScale.y / 2, transform.position.z);
        }
        else
        {
            if (jumpUpTimer > 0)
            {

                jumpUpTimer -= Time.deltaTime;
            }
            else
            {
                // isJumping = false;
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") && jumpUpTimer < 0)
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
            UIScript.collectedTrash++;
        }
    }
}