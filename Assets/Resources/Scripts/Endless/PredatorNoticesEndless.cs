using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorNoticesEndless : MonoBehaviour
{
    public float notice_offset, predator_offset;
    private Transform player;
    private IsopodChain IsopodChainRef;
    public GameObject PredatorPrefab, ObstaclePrefab;
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
            Destroy(GameObject.FindGameObjectWithTag("Predator"));
            //Debug.Log("I'm Hit!");
            GameObject tempNotice = Instantiate(gameObject, transform.parent);
            tempNotice.transform.position += Vector3.right * notice_offset;
            
            GameObject tempPredator = Instantiate(PredatorPrefab, player);
            tempPredator.transform.position += Vector3.left * (IsopodChainRef.isopodChain.Count * IsopodChainRef.child_offset + predator_offset);

            GameObject tempObstacle = Instantiate(ObstaclePrefab);
            tempObstacle.transform.position = tempNotice.transform.position - Vector3.right * (notice_offset / 3.0f);
            Destroy(gameObject);
        }
    }
}
