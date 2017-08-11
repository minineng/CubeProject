using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasController : MonoBehaviour
{

    public bool planning;

    // Use this for initialization
    void Start()
    {
        transform.Find("IdentificationText").GetComponent<Text>().text = GetComponentInParent<LevelController>().testMap.world+" - "+ GetComponentInParent<LevelController>().testMap.id;
    }

    public void init()
    {
        planning = true;
        transform.Find("CanvasOrdersFeed").GetComponent<OrderFeedController>().clearFeed();
        transform.Find("CanvasEndgame").gameObject.SetActive(false);
        transform.Find("CanvasEndgame").Find("Star1").GetComponent<Image>().sprite = Resources.Load("Images/StarEmpty", typeof(Sprite)) as Sprite;
        transform.Find("CanvasEndgame").Find("Star2").GetComponent<Image>().sprite = Resources.Load("Images/StarEmpty", typeof(Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        planning = GetComponentInParent<LevelController>().planning;
        transform.Find("IdentificationText").GetComponent<Text>().text = GetComponentInParent<LevelController>().testMap.world + " - " + GetComponentInParent<LevelController>().testMap.id;

        if (!planning)
        {
            if (!GetComponentInParent<LevelController>().end)
            {
                if (GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().alive)
                {
                    transform.Find("PlanningText").GetComponent<Text>().text = "playing time";
                    transform.Find("PlanningText").GetComponent<Text>().color = Color.red;
                    transform.Find("Turn Count").GetComponent<Text>().text = "turn " + GetComponentInParent<LevelController>().turnCount;

                }
                else
                {
                    transform.Find("CanvasEndgame").gameObject.SetActive(true);
                    transform.Find("CanvasEndgame").Find("FinishText").GetComponent<Text>().text = "try again";
                }
            }
            else
            {
                transform.Find("CanvasEndgame").gameObject.SetActive(true);
                transform.Find("CanvasEndgame").Find("FinishText").GetComponent<Text>().text = "well done";
                transform.Find("CanvasEndgame").Find("Star1").GetComponent<Image>().sprite = Resources.Load("Images/Star", typeof(Sprite)) as Sprite;
                if(GetComponentInParent<LevelController>().allPainted)
                    transform.Find("CanvasEndgame").Find("Star2").GetComponent<Image>().sprite = Resources.Load("Images/Star", typeof(Sprite)) as Sprite;

            }

        }


    }
}
