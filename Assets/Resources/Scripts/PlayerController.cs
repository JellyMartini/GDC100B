using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody m_rigidbody;
    private PlayerInput m_playerInput;
    private InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_playerInput = GetComponent<PlayerInput>();
        moveAction = m_playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = moveAction.ReadValue<Vector2>();

        m_rigidbody.AddForce(new Vector3(velocity.x, velocity.y));
    }
}
