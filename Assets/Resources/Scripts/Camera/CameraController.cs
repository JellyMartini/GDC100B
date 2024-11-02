using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    
    private float x_offset;
    private float initial_y, initial_z;
    public bool follow = false;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        x_offset = transform.position.x;
        initial_y = transform.position.y;
        initial_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
            transform.position = new Vector3(target.position.x + x_offset, initial_y, initial_z);
    }
}
