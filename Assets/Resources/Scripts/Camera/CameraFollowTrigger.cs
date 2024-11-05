using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The code for the box trigger for following the target
public class CameraFollowTrigger : MonoBehaviour
{
    // The CameraController component of the MainCamera
    private CameraController cameraController;

    void Start()
    {
        // Get the CameraController component of the Camera so we can affect the public variables
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        //Debug.Log(cameraController.name);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.name + " Hit");
        // If the isopod enters, toggle Camera's follow bool
        if (other.CompareTag("Isopod_Collision")) cameraController.follow = !cameraController.follow;
    }
}
