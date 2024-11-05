using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scrolls the background according to player position
public class BackgroundParallax : MonoBehaviour
{
    // the player
    private Transform playerTransform;

    // the scroll speed
    public float parallaxConstant;
    // Start is called before the first frame update
    void Start()
    {
        // initialise
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // set the position to the player's X translation * parallaxConstant
        // This makes the background appear to scroll in the opposite direction to player movement
        transform.position = transform.parent.position + new Vector3(-playerTransform.position.x * parallaxConstant + 20.0f, 0.0f, 120.0f);
    }
}
