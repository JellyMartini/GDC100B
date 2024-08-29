using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    public int cubeNo;
    void Start()
    {
        for (int i = 0; i < cubeNo; i++)
        {
            Instantiate<GameObject>(cube, cube.transform.position + Vector3.right * (i + 1) * 3.0f, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
