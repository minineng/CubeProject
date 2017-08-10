using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFeedController : MonoBehaviour
{

    public GameObject feedElement;
    public List<GameObject> feedList;

    // Use this for initialization
    void Start()
    {
        feedList = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        //print(feedList.Count+" es distinto de "+ GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().actionSet.Count);
        if (feedList.Count != GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().actionSet.Count)
        {
            float division = feedList.Count / 7;
            Vector3 auxPosition = new Vector3(transform.position.x + (feedList.Count - 7 * (int)division) * 35, transform.position.y-35* (int)division, transform.position.z);

            GameObject auxElement = Instantiate(feedElement, auxPosition, Quaternion.identity, transform);
            auxElement.GetComponent<FeedElementController>().action = GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().actionSet[feedList.Count];
            auxElement.GetComponent<FeedElementController>().id = feedList.Count;
            feedList.Add(auxElement);


        }
    }

    public void updateFeedPositions()
    {
        for (int i = 0; i < feedList.Count; i++) {

            float division = i / 7;
            //print("Divison es "+(int)division);
            Vector3 auxPosition = new Vector3(transform.position.x + (i - 7 * (int)division) * 35, transform.position.y - 35 * (int)division, transform.position.z);
            feedList[i].transform.position = auxPosition;
            feedList[i].GetComponent<FeedElementController>().id = i;

                }
    }
}
