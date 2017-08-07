using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 lookAt;

    private Vector3 initialPosition;

    public float cameraRadius;
    public float actualRadius;
    public float cameraSpeed;
    public float cameraHeight;
    public float maxHeight;
    public float minHeight;

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
        movement = true;
        initialPosition = transform.position;

        cameraHeight = transform.position.y;

        lookAt = GetComponentInParent<LevelController>().getCameraLookingPoint(); ;
        actualRadius = 0;


    }

    void Update()
    {

        if (movement)
        {

            if (cameraHeight < minHeight)
                cameraHeight = minHeight;
            else if (cameraHeight > maxHeight)
                cameraHeight = maxHeight;


            actualRadius = actualRadius + cameraSpeed;

            x = cameraRadius * Mathf.Cos(actualRadius * Mathf.Deg2Rad);
            z = cameraRadius * Mathf.Sin(actualRadius * Mathf.Deg2Rad);

            if (actualRadius > 360)
                actualRadius = 0;

            transform.position = new Vector3(x, cameraHeight, z);
        }

        transform.LookAt(lookAt);

    }
}
