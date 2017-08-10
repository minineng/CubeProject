using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileController : MonoBehaviour {

    public bool painted;
    public Vector3 coordinates;

    public Material paintedMaterial;
    public Material finishLineMaterial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       /* if (painted && GetComponent<MeshRenderer>().material != paintedMaterial)
            GetComponent<MeshRenderer>().material = paintedMaterial;*/
    }

    public void paintThisTile()
    {
        if (!painted)
        {
            painted = true;
            GetComponent<MeshRenderer>().material = paintedMaterial;
        }
    }

    public bool isPainted()
    {
        return painted;
    }
}
