using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    private float currentSpeed;

    private float verticalInput;
    private float horizontalInput;

    [SerializeField] float jumpHight;
    [SerializeField] float jumpTime;
    [SerializeField] float gravityStart;
    [SerializeField] float gravityRealease;
    [SerializeField] float gravityDown;

    // Start is called before the first frame update
    void Start()
    {

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
        #endregion Move


        #region Jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Jump
            // Use gravityStart
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

        #endregion Jump

    }
}
