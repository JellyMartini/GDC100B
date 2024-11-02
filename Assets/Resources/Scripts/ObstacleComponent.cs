using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleComponent : MonoBehaviour
{
    public bool passable;
    public Material stone_mat, dirt_mat;
    public MeshRenderer meshRenderer;
    private Camera mainCamera;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        passable = (Random.value > 0.5f) ? true : false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        stone_mat = Resources.Load<Material>("Materials/PassThrough_Red");
        dirt_mat = Resources.Load<Material>("Materials/PassThrough_Green");
        meshRenderer = GetComponent<MeshRenderer>();
        if (passable) meshRenderer.material = dirt_mat;
        else meshRenderer.material = stone_mat;
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.SetVector("_PlayerScreenPos", mainCamera.WorldToScreenPoint(playerTransform.position) / mainCamera.pixelRect.size);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Isopod_Collision"))
        {
            if (!passable)
            {
                other.GetComponentInParent<PlayerController>().moveSpeed = -other.GetComponentInParent<PlayerController>().moveSpeed;
                PredatorController tempPredator = GameObject.FindGameObjectWithTag("Predator").GetComponent<PredatorController>();
                tempPredator.tally++;
                if (tempPredator.tally >= tempPredator.tallyInterval) {
                    tempPredator.tally = 0;
                    List<GameObject> isopodChain = other.GetComponentInParent<IsopodChain>().isopodChain;
                    Destroy(isopodChain.Last<GameObject>());
                    isopodChain.RemoveAt(isopodChain.Count - 1);
                    if (isopodChain.Count <= 0) SceneManager.LoadScene(3);
                }
            }
            else
            {
                GameObject tempPredator = GameObject.FindGameObjectWithTag("Predator");
                tempPredator.transform.SetParent(null);

                foreach (GameObject collider in GameObject.FindGameObjectsWithTag("Obstacle"))
                {
                    collider.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}
