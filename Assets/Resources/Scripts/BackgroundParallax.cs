using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private Transform playerTransform;
    public float parallaxConstant;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + new Vector3(-playerTransform.position.x * parallaxConstant + 20.0f, 0.0f, 120.0f);
    }
}
