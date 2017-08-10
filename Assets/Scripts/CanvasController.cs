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

    }

    // Update is called once per frame
    void Update()
    {
        planning = GetComponentInParent<LevelController>().planning;

        if (!planning)
        {
            if (GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().alive)
            {
                transform.GetChild(0).GetComponent<Text>().text = "playing time"; 
                transform.GetChild(0).GetComponent<Text>().color = Color.red;
                transform.GetChild(1).GetComponent<Text>().text = "turn "+ GetComponentInParent<LevelController>().turnCount;

            }
            else
            {
                transform.GetChild(0).GetComponent<Text>().text = "you are dead :)";
                transform.GetChild(0).GetComponent<Text>().color = Color.red;
            }


        }


    }
}
