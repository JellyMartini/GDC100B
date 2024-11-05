using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorNoticesEndless : MonoBehaviour
{
    // How far apart the Predator Spawn boxes are placed,
    // The distance that the predator follows the isopod chain at
    public float notice_offset, predator_offset;
    // the player position
    private Transform player;
    // For how many isopods there are
    private IsopodChain IsopodChainRef;
    // Ready to spawn a new predator, a new obstacle
    public GameObject PredatorPrefab, ObstaclePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This class is instantiated periodically at runtime
    // So use Awake instead of Start for initialising variables
    // Awake is called on instantiation
    void Awake()
    {
        // initialise
        // could do this as a static reference??
        player = GameObject.FindGameObjectWithTag("Player").transform;
        IsopodChainRef = player.gameObject.GetComponent<IsopodChain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // when the player enters
        if (other.gameObject.CompareTag("Isopod_Collision"))
        {
            // Destroy the predator that was following (save on loaded resources)
            Destroy(GameObject.FindGameObjectWithTag("Predator"));
            //Debug.Log("I'm Hit!");
            // spawn another of these, offset its X translation by notice_offset
            GameObject tempNotice = Instantiate(gameObject, transform.parent);
            tempNotice.transform.position += Vector3.right * notice_offset;
            
            // Spawn a new predator at the end of the isopod chain, + the predator offset
            GameObject tempPredator = Instantiate(PredatorPrefab, player);
            tempPredator.transform.position += Vector3.left * (IsopodChainRef.isopodChain.Count * IsopodChainRef.child_offset + predator_offset);

            // Spawn a new obstacle, 2/3s as close as the new self
            GameObject tempObstacle = Instantiate(ObstaclePrefab);
            tempObstacle.transform.position = tempNotice.transform.position - Vector3.right * (notice_offset / 3.0f);
            // Destroys self
            Destroy(gameObject);
        }
    }
}
