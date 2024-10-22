using System;
using System.Collections;
using System.Collections.Generic;
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
    private bool isGrounded, check_isGrounded;
    private bool tryJump, tryBurrow;
    private float actionCoyote, actionTimer;
    public float jumpSpeed;
    public float coyoteTime, check_isGroundedTime;

    private float view_model_offset;

    //private Rigidbody rb;
    //private Collider playerCollider;

    public GameObject ground;
    private Material groundMat;

    // Start is called before the first frame update
    void Awake()
    {
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
    {
        moveInput = Mathf.Lerp(initial_moveInput, target_moveInput, interpWeight_moveInput);

        interpWeight_moveInput = Mathf.Clamp01(interpWeight_moveInput + interpSpeed_moveInput * Time.deltaTime);

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
            if (tryJump)
            {
                check_isGrounded = false;
                isGrounded = false;
                verticalSpeed = jumpSpeed;
                tryJump = false;
                actionCoyote = 0.0f;
                actionTimer = 0.0f;
            } else if (tryBurrow)
            {
                groundMat.SetFloat("_Alpha", 0.8f);
                check_isGrounded = false;
                isGrounded = false;
                verticalSpeed = -jumpSpeed;
                tryBurrow = false;
                actionCoyote = 0.0f;
                actionTimer = 0.0f;
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
    {
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
            tryBurrow = false;
        }
    }

    public void OnBurrowInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && isGrounded)
        {
            tryBurrow = true;
            tryJump = false;
        }
    }
    
    public void OnCancel(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Application.Quit();
        }
    }
}
