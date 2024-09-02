using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsopodChain : MonoBehaviour
{
    public GameObject IsopodPrefab;
    public float child_offset;
    public int max_child_count;
    public List<GameObject> isopodChain;
    // Start is called before the first frame update
    void Start()
    {
        isopodChain = new List<GameObject>
        {
            GameObject.FindGameObjectWithTag("Isopod")
        };
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
