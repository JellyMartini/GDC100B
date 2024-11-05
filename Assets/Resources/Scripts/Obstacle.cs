using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Identical to the endless mode version
// Except it uses ObstacleComponent, not ObstacleComponentEndless
public class Obstacle : MonoBehaviour
{
    public ObstacleComponent under, through, over;
    // Start is called before the first frame update
    void Start()
    {
        // If the segmented obstacle generates as completely impassable,
        // force the middle to be passable
        // just in case (these are curated levels, they should be set properly)
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
