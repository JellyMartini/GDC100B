using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purely for setting up in-scene visual objects
public class DebugMaster : MonoBehaviour
{
    // We spawn a bunch of cubes in a row, figure out how many
    public int markerCubeCount;
    // just a cube MeshRenderer parented to the DebugMaster object
    private GameObject markerCube;
    // Start is called before the first frame update
    void Start()
    {
        // This could contain lots of other logic
        // So preemptively shrinking the visual information
        spawnCubes();
    }

    // Spawn markerCubeCount cubes in a row
    void spawnCubes() {
        // initialise
        markerCube = GameObject.Find("MarkerCube");
        Transform markerCubeParent = markerCube.transform.parent;
        // Spawn the cube (using 0 indexing)
        for (int i = 0; i < markerCubeCount - 1; i++)
            Instantiate(markerCube, markerCube.transform.position + Vector3.right * (i + 1) * 3.0f, Quaternion.identity, markerCubeParent);
        

        // Combine the cubes into one mesh (they can be drawn in one draw call)
        // This also grabs the MeshFilter in markerCubeParent, which we don't want
        MeshFilter[] meshFilters = markerCubeParent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];

        // need a second iterator for the one-element-smaller combine array
        // In-editor, the markerCubeParent's MeshFilter will always be the first in the array
        // This behaviour is not guaranteed in Builds
        int combineIter = 0;
        for (int i = 0; i < meshFilters.Length; i++)
        {
            //Debug.Log(combineIter);
            // skip if it's the parent's MeshFilter
            if (String.Compare(meshFilters[i].gameObject.name, markerCubeParent.name) == 0) {
                //Debug.Log("Skipping...");
                // goto next iteration
                continue;
            }

            // Add the mesh with transformation to the sharedMesh
            combine[combineIter].mesh = meshFilters[i].sharedMesh;
            combine[combineIter].transform = meshFilters[i].transform.localToWorldMatrix;
            if (meshFilters[i].gameObject != markerCube) Destroy(meshFilters[i].gameObject);
            combineIter++;
        }

        // Create a new single mesh from the combined meshes
        // Ordinarily I wouldn't use sharedMesh for assigning,
        // but this is how it's written in the documentation
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
