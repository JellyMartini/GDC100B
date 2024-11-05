using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // current input, the initial input, and the lerpto target input
    private float moveInput, initial_moveInput, target_moveInput;
    private float interpWeight_moveInput; // lerp weight
    public float interpSpeed_moveInput; // lerp speed
    public float slow, mid, fast; // speed multipliers
    public float moveSpeed; // player moveSpeed
    public float verticalSpeed, gravity; // accumulated verticalSpeed to apply, gravity constant
    private bool isGrounded, check_isGrounded; // is the character on the ground? Do we check if the player is on the ground?
    private bool tryJump, tryBurrow; // Did the player press jump or burrow?
    private float actionCoyote, actionTimer; // so that the player can try burrowing just before actually able to enter that state
    public float jumpSpeed; // determines jump height
    public float coyoteTime, check_isGroundedTime; // so that the player can try jumping just before actually able to enter that state

    // Where the isopod model is in relation to the GameObject
    private float view_model_offset;

    //private Rigidbody rb;
    //private Collider playerCollider;

    public GameObject ground; // ground object
    private Material groundMat; // turns more transparent when player burrows

    // Start is called before the first frame update
    void Awake()
    {
        // initialise
        moveInput = 0.0f;
        initial_moveInput = mid;
        target_moveInput = mid;

        interpWeight_moveInput = 0.0f;

        //rb = GetComponent<Rigidbody>();
        //playerCollider = GetComponentInChildren<Collider>();

        isGrounded = true;
        check_isGrounded = true;
        tryJump = false;

        actionCoyote = 0.0f;
        actionTimer = 0.0f;

        view_model_offset = GameObject.FindGameObjectWithTag("Isopod_ViewModel").transform.localPosition.y;
        Debug.Log(view_model_offset);
        groundMat = ground.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {   // HANDLING INPUT IN UPDATE
        // Interpolate between the four speeds (STOP, SLOW, MID, FAST) by lerping to whichever one was pressed
        // Smoothly transitions between speeds
        moveInput = Mathf.Lerp(initial_moveInput, target_moveInput, interpWeight_moveInput);
        interpWeight_moveInput = Mathf.Clamp01(interpWeight_moveInput + interpSpeed_moveInput * Time.deltaTime);

        // if the player is rising above the ground after burrowing, or sinking after jumping, set the verticalSpeed to 0
        // then snap the player to the ground
        if ((Mathf.Sign(gravity) == -1.0f && transform.position.y > 1.0f) || (Mathf.Sign(gravity) == 1.0f && transform.position.y < -1.0f))
        {
            verticalSpeed = 0.0f;
            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        } 

        if (isGrounded) 
        {
            
            if (verticalSpeed != 0.0f)
            {
                verticalSpeed = 0.0f;
                transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
                groundMat.SetFloat("_Alpha", 0.2f);
            }
            if (tryJump) // if the player pressed jump within actionCoyote of touching the ground, jump
            {
                check_isGrounded = false;
                isGrounded = false;
                verticalSpeed = jumpSpeed;
                tryJump = false;
                actionCoyote = 0.0f;
                actionTimer = 0.0f;
            } else if (tryBurrow) // if the player pressed burrow within actionCoyote of touching the ground, burrow
            {
                groundMat.SetFloat("_Alpha", 0.8f);
                check_isGrounded = false;
                isGrounded = false;
                verticalSpeed = -jumpSpeed;
                tryBurrow = false;
                actionCoyote = 0.0f;
                actionTimer = 0.0f;
                // burrowing is like upside down jumping, which means gravity has to go the other way
                if (Mathf.Sign(gravity) != -1.0f)
                {
                    gravity = -gravity;
                }
            }
            
        }
        else // if the player is in the air
        {
            verticalSpeed -= gravity;
        } 
        

        actionCoyote += Time.deltaTime;
        actionTimer += Time.deltaTime;

        if (actionCoyote > coyoteTime)
        {
            tryJump = false;
            tryBurrow = false;
        }

        if (actionTimer > check_isGroundedTime)
        {
            check_isGrounded = true;
        }
        //Debug.Log("Move Input: " + moveInput.ToString() + ", Jump Input: " + jumpInput.ToString());
    }

    void FixedUpdate()
    {   // HANDLING ACTUAL MOVEMENT IN FIXEDUPDATE
            // if we are checking for isGrounded, see if the player is close enough to the ground
            if (check_isGrounded && Physics.Raycast(transform.position + Vector3.up * view_model_offset, Vector3.down, out RaycastHit hit))//, 1.0f, LayerMask.GetMask("Ignore Raycast")))
            {
                isGrounded = (hit.distance < 0.5f) ? true : false;
                //Debug.Log(hit.collider.name.ToString() + ", " + isGrounded + ", " + hit.distance); 
                if (isGrounded)
                {
                    if (Mathf.Sign(gravity) != 1.0f)
                    {
                        gravity = -gravity;
                    }
                    Debug.DrawLine(transform.position + Vector3.up * view_model_offset, transform.position + Vector3.down * hit.distance, Color.green);
                }
                else Debug.DrawLine(transform.position + Vector3.up * view_model_offset, transform.position + Vector3.down * hit.distance, Color.red);
            }
        Vector3 velocity = (Vector3.right * moveInput * moveSpeed + Vector3.up * verticalSpeed) * Time.fixedDeltaTime;
        transform.Translate(velocity);
    }

    public void OnMoveInput(InputAction.CallbackContext callbackContext)
    {
        // When the player makes an input on a dpad, or arrowpad, or left control stick
        // interpret it as one of the moveSpeed multipliers
        if (callbackContext.performed)
        {
            Vector2 temp = callbackContext.ReadValue<Vector2>();
            
            if (temp.x == -1.0f) target_moveInput = 0.0f;
            else if (temp.y == -1.0f) target_moveInput = slow;
            else if (temp.x == 1.0f) target_moveInput = mid;
            else if (temp.y == 1.0f) target_moveInput = fast;
            
            // initialise lerping
            initial_moveInput = moveInput;
            
            interpWeight_moveInput = 0.0f;

            //Debug.Log(callbackContext.ReadValue<Vector2>());
        }
    }

    public void OnJumpInput(InputAction.CallbackContext callbackContext)
    {
        // if the player pressed jump, then try to jump and do not burrow
        if (callbackContext.performed)
        {
            tryJump = true;
            tryBurrow = false;
        }
    }

    public void OnBurrowInput(InputAction.CallbackContext callbackContext)
    {
        // if the player pressed burrow, then try to burrow and do not jump
        if (callbackContext.performed && isGrounded)
        {
            tryBurrow = true;
            tryJump = false;
        }
    }
}
