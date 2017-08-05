using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasController : MonoBehaviour {

	public bool planning;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		planning = GetComponentInParent<LevelController> ().getPlanning();

		transform.GetChild (0).gameObject.SetActive (planning);

	}
}
