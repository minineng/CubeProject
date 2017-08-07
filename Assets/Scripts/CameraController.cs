using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 lookAt;

    private Vector3 initialPosition;
    private Vector3 lastSavedPosition;

    public float cameraRadius;
    public float actualRadius;
    public float cameraSpeed;
    public float cameraHeight;
    public float maxHeight;
    public float minHeight;
    public float inactiveTime; //Time that will take to the camera to start moving on it's own
    public float timeToGetMoving;

    public bool movement;

    public float x;
    public float z;

    // Use this for initialization
    void Start()
    {
        x = 0;
        z = 0;
        minHeight = 0;
        maxHeight = 20;

        lastSavedPosition = new Vector3(-1, -1, -1);
        initialPosition = new Vector3(-1, -1, -1);

        cameraHeight = transform.position.y;

        lookAt = GetComponentInParent<LevelController>().getCameraLookingPoint();

        timeToGetMoving = inactiveTime + Time.time;
    }

    void Update()
    {

        if (cameraHeight < minHeight)
            cameraHeight = minHeight;
        else if (cameraHeight > maxHeight)
            cameraHeight = maxHeight;



        if (timeToGetMoving < Time.time)
            movement = true;

        if (Input.GetAxis("CameraMovement") > 0)
        {
            movement = false;
            timeToGetMoving = Time.time + inactiveTime;

        }

        if (movement)
            actualRadius = actualRadius + cameraSpeed;
        else
        {

            if (Input.GetAxis("CameraMovement") > 0 && initialPosition == new Vector3(-1, -1, -1))
            { //Saves the inital position
                initialPosition = Input.mousePosition;
                lastSavedPosition = initialPosition;
            }

            else if (Input.GetAxis("CameraMovement") > 0 && initialPosition != new Vector3(-1, -1, -1) && lastSavedPosition != Input.mousePosition)
            {

                float diff = (initialPosition.x - Input.mousePosition.x) / 100;
                lastSavedPosition = Input.mousePosition;
                actualRadius = actualRadius + diff;

            }

            else if (Input.GetAxis("CameraMovement") > 0 && initialPosition != new Vector3(-1, -1, -1) && lastSavedPosition == Input.mousePosition)
            {
                initialPosition = Input.mousePosition;
            }

            else if (Input.GetAxis("CameraMovement") == 0 && initialPosition != new Vector3(-1, -1, -1))
            {
                initialPosition.Set(-1, -1, -1);
            }


        }

        x = cameraRadius * Mathf.Cos(actualRadius * Mathf.Deg2Rad);
        z = cameraRadius * Mathf.Sin(actualRadius * Mathf.Deg2Rad);

        if (actualRadius > 360)
            actualRadius = 0;

        transform.position = new Vector3(x, cameraHeight, z);


        transform.LookAt(lookAt);

    }
}
