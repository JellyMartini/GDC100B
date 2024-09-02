using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollowPlayer : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.right * player.position.x;
    }
}
