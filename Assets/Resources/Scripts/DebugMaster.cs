using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMaster : MonoBehaviour
{
    public int markerCubeCount;
    private GameObject markerCube;
    // Start is called before the first frame update
    void Start()
    {
        spawnCubes();
    }

    void spawnCubes() {
        markerCube = GameObject.Find("MarkerCube");
        Transform markerCubeParent = markerCube.transform.parent;
        for (int i = 0; i < markerCubeCount - 1; i++)
            Instantiate(markerCube, markerCube.transform.position + Vector3.right * (i + 1) * 3.0f, Quaternion.identity, markerCubeParent);
        
        MeshFilter[] meshFilters = markerCubeParent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];

        int combineIter = 0;
        for (int i = 0; i < meshFilters.Length; i++)
        {
            //Debug.Log(combineIter);
            if (String.Compare(meshFilters[i].gameObject.name, markerCubeParent.name) == 0) {
                //Debug.Log("Skipping...");
                continue;
            }
            combine[combineIter].mesh = meshFilters[i].sharedMesh;
            combine[combineIter].transform = meshFilters[i].transform.localToWorldMatrix;
            if (meshFilters[i].gameObject != markerCube) Destroy(meshFilters[i].gameObject);
            combineIter++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        markerCubeParent.GetComponent<MeshFilter>().sharedMesh = mesh;
        markerCubeParent.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
