using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPassThrough : MonoBehaviour
{
    private Material holeShader;
    private Transform playerTransform;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        holeShader = GetComponent<Renderer>().material;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        holeShader.SetVector("_PlayerScreenPos", mainCamera.WorldToScreenPoint(playerTransform.position) / mainCamera.pixelRect.size);
    }
}
