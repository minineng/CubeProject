using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector2 MapSize;
	public int maxDistance;
	private Vector3 initialPosition;
	private Vector2 radius;

	// Use this for initialization
	void Start () {
		
		MapSize = new Vector2 (5,5);
		radius = new Vector2 (0, 0);

		maxDistance = 5;
		initialPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		radius.x += 0.01f;
		radius.y += 0.01f;
		if (radius.x < -1 || radius.x > 1)
			xSign = xSign * -1;
			
		if (radius.y < -1 || radius.y > 1)
			ySign = ySign * -1;



		transform.position = new Vector3 (initialPosition.x+maxDistance*radius.x, initialPosition.y, initialPosition.z+maxDistance*radius.y);

	}
}
