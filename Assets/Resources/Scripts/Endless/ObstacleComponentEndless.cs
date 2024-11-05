using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// 
public class ObstacleComponentEndless : MonoBehaviour
{
    // Can the player move through this segment?
    public bool passable;

    // References to the Passable and Impassable Material resources
    public Material stone_mat, dirt_mat;
    
    // The component that has the material set on instantiation
    public MeshRenderer meshRenderer;

    // The MainCamera
    private Camera mainCamera;
    // The transform of the player
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This class is instantiated periodically at runtime
    // So use Awake instead of Start for initialising variables
    // Awake is called on instantiation
    void Awake()
    {
        // Randomly decide if this object is passable, no probability weighting
        passable = (Random.value > 0.5f) ? true : false;
        // initialise
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        stone_mat = Resources.Load<Material>("Materials/PassThrough_Red");
        dirt_mat = Resources.Load<Material>("Materials/PassThrough_Green");
        meshRenderer = GetComponent<MeshRenderer>();

        // set the material to visually represent the passable state
        if (passable) meshRenderer.material = dirt_mat;
        else meshRenderer.material = stone_mat;
    }

    // Update is called once per frame
    void Update()
    {
        // The materials use CustomShaders that require inputs
        // This one use the position of the player on the screen to determine where to discard the pixels of this object
        meshRenderer.material.SetVector("_PlayerScreenPos", mainCamera.WorldToScreenPoint(playerTransform.position) / mainCamera.pixelRect.size);
    }

    void OnTriggerEnter(Collider other)
    {
        // if the player enters
        if (other.CompareTag("Isopod_Collision"))
        {
            // if player can't pass
            if (!passable)
            {
                // Knock the player back by reversing their speed
                other.GetComponentInParent<PlayerController>().moveSpeed = -other.GetComponentInParent<PlayerController>().moveSpeed;

                // Update the predator with how many times they've seen this isopod fail this obstacle
                
                PredatorController tempPredator = GameObject.FindGameObjectWithTag("Predator").GetComponent<PredatorController>();
                tempPredator.tally++;
                // if failed attempts exceeds how many chances the predator gives the isopod, eat the isopod
                if (tempPredator.tally >= tempPredator.tallyInterval) {
                    // reset the failed attempts
                    tempPredator.tally = 0;
                    // eat the isopod
                    List<GameObject> isopodChain = other.GetComponentInParent<IsopodChain>().isopodChain;
                    Destroy(isopodChain.Last<GameObject>());
                    isopodChain.RemoveAt(isopodChain.Count - 1);
                    // if all the isopods are gone, the player loses
                    if (isopodChain.Count <= 0) SceneManager.LoadScene("EndlessLoseScreen");
                }
            }
            else
            {
                // The predator is cannot follow the player through obstacles
                // To represent this, we deparent the predator so it no longer follows
                // This could be one-line, but we should check to make sure the GameObject exists
                // before doing anything with it
                GameObject tempPredator = GameObject.FindGameObjectWithTag("Predator");
                tempPredator.transform.SetParent(null);

                // The obstacle is formed from three colliders
                // It is possible for the player to succeed in passing the whole obstacle,
                // but touches an impassable component, triggering the failure interaction defined above
                // To avoid this, as soon as a successful clear has been made we stop checking for collisions on this obstacle
                foreach (GameObject collider in GameObject.FindGameObjectsWithTag("Obstacle"))
                {
                    collider.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}
