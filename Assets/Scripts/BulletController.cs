using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public int direction;
    public float bulletSpeed;

    // Use this for initialization
    void Start()
    {
        bulletSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {

        switch (direction)
        {
            case 0:
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, bulletSpeed);
                break;
            case 1:
                GetComponent<Rigidbody>().velocity = new Vector3(-bulletSpeed, 0, 0);
                break;
            case 2:
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -bulletSpeed);
                break;
            case 3:
                GetComponent<Rigidbody>().velocity = new Vector3(bulletSpeed, 0, 0);
                break;

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }


    }
}
