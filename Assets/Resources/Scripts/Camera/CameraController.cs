using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera Logic
public class CameraController : MonoBehaviour
{
    // What the camera will copy the X translation of
    public Transform target;
    
    // Added to the X translation of target
    private float x_offset;

    // The unchanging Y and Z translation components
    private float initial_y, initial_z;

    // Are we copying the target?
    public bool follow = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the target to the player isopod's transform
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // Set the values as they are in editor
        x_offset = transform.position.x;
        initial_y = transform.position.y;
        initial_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // When following the target, set the Camera's X position to the target + x_offset
        if (follow)
            transform.position = new Vector3(target.position.x + x_offset, initial_y, initial_z);
    }
}
