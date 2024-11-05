using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a trigger that unnegates player speed
public class ResetMoveSpeed : MonoBehaviour
{
    public float initialMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // this doesn't change
        initialMoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // if player enters
        if (other.CompareTag("Isopod_Collision"))
        {
            // set the speed to originally defined
            other.GetComponentInParent<PlayerController>().moveSpeed = initialMoveSpeed;
        }
    }
}
