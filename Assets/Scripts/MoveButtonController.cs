﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonController : MonoBehaviour {
    public Sprite MainSprite;
    public Sprite AlternateSprite;
    public PlayerController.actionList action;

	// Use this for initialization
	void Start () {

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
            case PlayerController.actionList.wait:
                name = "WaitButton";
                break;
            case PlayerController.actionList.execute:
                name = "PlayButton";
                AlternateSprite = Resources.Load("Images/StopButton", typeof(Sprite)) as Sprite;
                break;
        }
        string ruta = "Images/" + transform.name;
        MainSprite = Resources.Load(ruta, typeof(Sprite)) as Sprite;


        print("Cargo "+ ruta);
        GetComponent<Image>().sprite = MainSprite;

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnClick()
    {
        if (action != PlayerController.actionList.execute)
            transform.parent.GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().addToActionSet(action);
        else
        {
            transform.parent.GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().executeActions();
            if(GetComponent<Image>().sprite == MainSprite)
                GetComponent<Image>().sprite = AlternateSprite;
            else
                GetComponent<Image>().sprite = MainSprite;
        }


    }
}
