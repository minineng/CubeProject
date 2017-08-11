using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenuController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.parent.GetComponentInParent<LevelController>().timeBetweenActions == 1)
            transform.Find("FastButton").GetComponent<Image>().sprite = Resources.Load("Images/FastSign0", typeof(Sprite)) as Sprite;
        else
            transform.Find("FastButton").GetComponent<Image>().sprite = Resources.Load("Images/FastSign1", typeof(Sprite)) as Sprite;

    }

    public void goToMainMenu()
    {
        GameObject auxCanvasGame = transform.parent.gameObject;
        print("Name " + auxCanvasGame.transform.name);
        auxCanvasGame.transform.SetParent(auxCanvasGame.transform.parent.parent);
        Destroy(auxCanvasGame.transform.parent.Find("Level Controller").gameObject);
        auxCanvasGame.transform.parent.Find("CanvasMenu").gameObject.SetActive(true);
        auxCanvasGame.transform.parent.Find("CanvasMenu").GetComponent<MainMenuController>().switchMenu(0);
        auxCanvasGame.transform.parent.Find("Scenery").transform.Find("Character").gameObject.SetActive(true);
        auxCanvasGame.SetActive(false);
    }

    public void retryLevel()
    {
        GameObject auxCanvasGame = transform.parent.gameObject;

        auxCanvasGame.transform.Find("CanvasEndgame").gameObject.SetActive(false);
        print(auxCanvasGame.transform.Find("CanvasEndgame").name);
        //print("Name " + auxCanvasGame.transform.name);
        auxCanvasGame.transform.SetParent(auxCanvasGame.transform.parent.parent);
        int auxId, auxWorld;
        auxId = auxCanvasGame.transform.parent.Find("Level Controller").GetComponent<LevelController>().testMap.id;
        auxWorld = auxCanvasGame.transform.parent.Find("Level Controller").GetComponent<LevelController>().testMap.world;

        Destroy(auxCanvasGame.transform.parent.Find("Level Controller").gameObject);
        auxCanvasGame.transform.parent.Find("CanvasMenu").gameObject.SetActive(true);
        auxCanvasGame.transform.parent.Find("CanvasMenu").GetComponent<MainMenuController>().switchMenu(1);
        auxCanvasGame.transform.parent.Find("CanvasMenu").transform.Find("LevelSelectCanvas").GetComponent<LevelSelectController>().loadLevel(auxId, auxWorld);
    }

    public void changeSpeed()
    {
        if (transform.parent.GetComponentInParent<LevelController>().timeBetweenActions == 1)
            transform.parent.GetComponentInParent<LevelController>().timeBetweenActions = 0.33f;
        else
            transform.parent.GetComponentInParent<LevelController>().timeBetweenActions = 1f;

    }
}
