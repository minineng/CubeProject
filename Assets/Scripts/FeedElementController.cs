using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedElementController : MonoBehaviour {

    public PlayerController.actionList action;
    public Button button;

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
        }
        string ruta = "Images/" + transform.name;
        GetComponent<Image>().sprite = Resources.Load(ruta, typeof(Sprite)) as Sprite;

        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        


        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}

}
