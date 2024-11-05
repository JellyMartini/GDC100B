using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simply stores info that the predator observes
public class PredatorController : MonoBehaviour
{
    public int tally;
    // the number of times the isopod can fail to clear an obstacle before being eaten
    public int tallyInterval;
    // Start is called before the first frame update
    void Start()
    {
        tally = 0;
        tallyInterval = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
