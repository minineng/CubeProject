using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileController : MonoBehaviour {

    public bool painted;
    public Vector3 coordinates;

    public Material paintedMaterial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (painted && GetComponent<MeshRenderer>().material != paintedMaterial)
            GetComponent<MeshRenderer>().material = paintedMaterial;
    }

    public void paintThisTile(bool paint)
    {
        painted = paint;
    }

    public bool isPainted()
    {
        return painted;
    }
}
