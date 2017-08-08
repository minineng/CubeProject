using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonController : MonoBehaviour {

    public Sprite image;
    public PlayerController.actionList action;

	// Use this for initialization
	void Start () {
        image = new Sprite();

        switch (action)
        {
            case PlayerController.actionList.turnLeft:
               name = "LeftArrow";
                break;
            case PlayerController.actionList.turnRight:
                name = "RightArrow";

                break;
            case PlayerController.actionList.forward:
                name = "ForwardArrow";

                break;
            case PlayerController.actionList.backward:
                name = "BackwardArrow";

                break;
            case PlayerController.actionList.jump:
                name = "JumpArrow";
                break;
        }
        string ruta = "Images/" + transform.name;

        //print("Cargo "+ ruta);
        GetComponent<Image>().sprite = Resources.Load(ruta, typeof(Sprite)) as Sprite;

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnClick()
    {
        transform.parent.GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().addToActionSet(action);


    }
}
