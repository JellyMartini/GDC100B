using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float moveInput, initial_moveInput, target_moveInput;

    private float interpWeight_moveInput;
    public float interpSpeed_moveInput;
    public float slow, mid, fast;
    public float moveSpeed;
    public float verticalSpeed, gravity;
    private bool isGrounded;
    private bool tryJump, tryBurrow;
    private float jumpTimer, burrowTimer;
    public float jumpSpeed;
    public float coyoteTime;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        moveInput = 0.0f;
        initial_moveInput = 0.0f;
        target_moveInput = 0.0f;

        interpWeight_moveInput = 0.0f;

        rb = GetComponent<Rigidbody>();

        isGrounded = true;
        tryJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Mathf.Lerp(initial_moveInput, target_moveInput, interpWeight_moveInput);

        interpWeight_moveInput = Mathf.Clamp01(interpWeight_moveInput + interpSpeed_moveInput * Time.deltaTime);

        if (isGrounded) 
        {
            if (verticalSpeed < 0.0f)
            {
                verticalSpeed = 0.0f;
            }
            if (tryJump)
            {
                verticalSpeed = jumpSpeed;
                tryJump = false;
                jumpTimer = 0.0f;
            }
            else if (tryBurrow)
            {
                verticalSpeed = -jumpSpeed;
                tryBurrow = false;
                burrowTimer = 0.0f;
                if (Mathf.Sign(gravity) != -1.0f)
                {
                    gravity = -gravity;
                }
            }
        }
        else
        {
            verticalSpeed -= gravity;
        }

        jumpTimer += Time.deltaTime;
        burrowTimer += Time.deltaTime;

        if (jumpTimer > coyoteTime) tryJump = false;
        if (burrowTimer > coyoteTime) tryBurrow = false;

        //Debug.Log("Move Input: " + moveInput.ToString() + ", Jump Input: " + jumpInput.ToString());
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))//, 1.0f, LayerMask.GetMask("Ignore Raycast")))
        {
            isGrounded = (hit.distance < 1.01f) ? true : false;
            //Debug.Log(hit.collider.name.ToString() + ", " + isGrounded + ", " + hit.distance); 
            if (isGrounded)
            {
                if (Mathf.Sign(gravity) != 1.0f)
                {
                    gravity = -gravity;
                }
                Debug.DrawLine(transform.position, transform.position + Vector3.down * hit.distance, Color.green);
            }
            else Debug.DrawLine(transform.position, transform.position + Vector3.down * hit.distance, Color.red);
        }
        rb.velocity = (Vector3.right * moveInput * moveSpeed + Vector3.up * verticalSpeed) * Time.fixedDeltaTime;
    }

    public void OnMoveInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Vector2 temp = callbackContext.ReadValue<Vector2>();
            
            if (temp.x == -1.0f) target_moveInput = 0.0f;
            else if (temp.y == -1.0f) target_moveInput = slow;
            else if (temp.x == 1.0f) target_moveInput = mid;
            else if (temp.y == 1.0f) target_moveInput = fast;
            

            initial_moveInput = moveInput;
            
            interpWeight_moveInput = 0.0f;

            //Debug.Log(callbackContext.ReadValue<Vector2>());
        }
    }

    public void OnJumpInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            tryJump = true;
        }
    }

    public void OnBurrowInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && isGrounded)
        {
            tryBurrow = true;
        }
    }
}
