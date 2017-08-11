using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {


    }

    public void switchMenu(int id)
    {
        switch(id)
        {
            case 0://Switches Bewtwwen menus
                this.transform.Find("MainMenuCanvas").gameObject.SetActive(false);
                this.transform.Find("LevelSelectCanvas").gameObject.SetActive(true); 
                break;
            case 1:
                Application.Quit();
                break;
            case 2:
                this.transform.Find("MainMenuCanvas").gameObject.SetActive(true);
                this.transform.Find("LevelSelectCanvas").gameObject.SetActive(false);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
