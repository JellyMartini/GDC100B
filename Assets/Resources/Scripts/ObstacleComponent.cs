using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    public bool passable;
    public Material stone_mat, dirt_mat;
    public MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        passable = (Random.value > 0.5f) ? true : false;
        meshRenderer = GetComponent<MeshRenderer>();
        if (passable) meshRenderer.material = dirt_mat;
        else meshRenderer.material = stone_mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Isopod_Collision"))
        {
            if (!passable)
            {
                other.GetComponentInParent<PlayerController>().moveSpeed = -other.GetComponentInParent<PlayerController>().moveSpeed;
            }
            else
            {
                GameObject tempPredator = GameObject.FindGameObjectWithTag("Predator");
                Destroy(tempPredator);

                foreach (GameObject collider in GameObject.FindGameObjectsWithTag("Obstacle"))
                {
                    collider.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}
