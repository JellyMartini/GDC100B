using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Populate a List with isopods up to max_child_count
public class IsopodChain : MonoBehaviour
{
    // Reference to the template isopod
    // This is not an actual prefab
    public GameObject IsopodPrefab;

    // determines the distance between isopods
    // public so that the variable is exposed in-editor
    public float child_offset;
    // max isopods in the chain
    public int max_child_count;
    // the isopod chain, public so it can be referenced from anywhere
    public List<GameObject> isopodChain;
    // Start is called before the first frame update
    void Start()
    {
        // initialise the isopod chain as containing the existing isopod
        isopodChain = new List<GameObject>
        {
            GameObject.FindGameObjectWithTag("Isopod")
        };
        // populate the remainder of the list with isopods
        GameObject temp;
        for (int i = 1; i < max_child_count; i++)
        {
            temp = Instantiate(IsopodPrefab, transform);
            temp.transform.position += Vector3.left * child_offset * i;
            isopodChain.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
