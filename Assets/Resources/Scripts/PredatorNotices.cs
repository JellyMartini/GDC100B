using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorNotices : MonoBehaviour
{
    public float notice_offset, predator_offset;
    private Transform player;
    private IsopodChain IsopodChainRef;
    public GameObject PredatorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        IsopodChainRef = player.gameObject.GetComponent<IsopodChain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Isopod_Collision"))
        {
            if (GameObject.FindGameObjectWithTag("Predator") != null)
            {
                //Destroy(GameObject.FindGameObjectWithTag("Predator"));
                //Debug.Log("I'm Hit!");
            }
            GameObject tempPredator = Instantiate(PredatorPrefab, player);
            tempPredator.transform.position += Vector3.left * (IsopodChainRef.isopodChain.Count * IsopodChainRef.child_offset + predator_offset);
        }
    }
}
