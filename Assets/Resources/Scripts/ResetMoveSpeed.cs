using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMoveSpeed : MonoBehaviour
{
    public float initialMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        initialMoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Isopod_Collision"))
        {
            other.GetComponentInParent<PlayerController>().moveSpeed = initialMoveSpeed;
        }
    }
}
