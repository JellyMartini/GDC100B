using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEndless : MonoBehaviour
{
    public ObstacleComponentEndless under, through, over;
    // Start is called before the first frame update
    void Start()
    {
        // If the segmented obstacle generates as completely impassable,
        // force the middle to be passable
        if (!under.passable && !over.passable) 
        {
            through.passable = true;
            through.meshRenderer.material = over.dirt_mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
