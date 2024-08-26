using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody m_rigidbody;
    private PlayerInput m_playerInput;
    private InputAction moveAction;

    public float moveSpeed;

    private Vector2 velocity = Vector2.zero;

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
        velocity = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MoveCharacter(velocity);
    }

    private void MoveCharacter(Vector2 direction)
    {
        m_rigidbody.velocity = new Vector3(1.0f + direction.x, direction.y) * moveSpeed * Time.fixedDeltaTime;
    }
}
