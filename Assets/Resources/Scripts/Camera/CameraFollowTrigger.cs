using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTrigger : MonoBehaviour
{
    private CameraController cameraController;

    void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        Debug.Log(cameraController.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " Hit");
        if (other.CompareTag("Isopod_Collision")) cameraController.follow = !cameraController.follow;
    }
}
