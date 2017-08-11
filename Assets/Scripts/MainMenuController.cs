using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public int id; // 0 for play - 1 for exit
    public Button button;

    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        switch(id)
        {
            case 0:
                this.transform.parent.gameObject.SetActive(false);
                this.transform.parent.parent.Find("LevelSelectCanvas").gameObject.SetActive(true);
                
                break;
            case 1:
                Application.Quit();
                break;
            case 2:
                this.transform.parent.gameObject.SetActive(false);
                this.transform.parent.parent.Find("MainMenuCanvas").gameObject.SetActive(true);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
