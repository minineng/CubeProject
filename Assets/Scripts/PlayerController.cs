using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public actionList[] actionSet;


    public enum actionList
    {
        nothing,
        forward,
        backward,
        turnRight,
        turnLeft,
        jump,

    };

    int lookingTo;

    int actionTurn;
    public int maxTurns;
    bool playingTime;
    public float timeBetweenActions;
    private float timeToNewAction;
    public Vector3 coordinates;


    // Use this for initialization
    void Start()
    {
        lookingTo = 3;

        maxTurns = 20;

        actionSet = new actionList[maxTurns];

    }

    // Update is called once per frame
    void Update()
    {

        switch (lookingTo)
        {
            case 0:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
            case 2:
                transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 3:
                transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }

        if (GetComponentInParent<LevelController>().planning)
        {

            if (Input.GetButtonDown("Forward"))
                addToActionSet(actionList.forward);
            if (Input.GetButtonDown("Right"))
                addToActionSet(actionList.turnRight);
            if (Input.GetButtonDown("Left"))
                addToActionSet(actionList.turnLeft);
            if (Input.GetButtonDown("Backward"))
                addToActionSet(actionList.backward);

            if (Input.GetButtonDown("ExecuteActions"))
                executeActions();
        }
        else
        {
            if (Time.time > timeToNewAction)
            {
                if (actionTurn < (actionSet.Length - actionSetGapCount()))
                {
                    makeAction(actionSet[actionTurn]);
                    actionTurn++;
                    timeToNewAction = Time.time + timeBetweenActions;
                    if (GetComponentInParent<LevelController>().getTileByCoordinates(coordinates) != null)
                        GetComponentInParent<LevelController>().getTileByCoordinates(coordinates).GetComponent<tileController>().paintThisTile(true);
                }
            }
        }



    }

    public void executeActions()
    {
        GetComponentInParent<LevelController>().planning = false;
        timeToNewAction = Time.time + timeBetweenActions;
        print("Ejecuto las acciones");
        actionTurn = 0;
    }

    public int actionSetGapCount()
    {
        int auxCont = 0;
        for (int i = 0; i < actionSet.Length; i++)
        {
            if (actionSet[i] == actionList.nothing)
                auxCont++;
        }

        return auxCont;
    }

    public void addToActionSet(actionList action)
    {
        print("Añado " + action);



        if (actionSetGapCount() > 0)
            actionSet[actionSet.Length - actionSetGapCount()] = action;
        else
        {
            print("Lista de acciones llena");

            for (int i = 0; i < actionSet.Length; i++)
            {
                print("Accion " + i + " " + actionSet[i]);

            }

        }

    }

    public void makeAction(actionList action)
    {

        switch (action)
        {
            case actionList.forward:

                switch (lookingTo)
                {
                    case 0:
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);
                        coordinates.z++;
                        break;
                    case 1:
                        this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
                        coordinates.x--;
                        break;
                    case 2:
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
                        coordinates.z--;
                        break;
                    case 3:
                        this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
                        coordinates.x++;
                        break;
                }

                break;
            case actionList.backward:
                switch (lookingTo)
                {
                    case 0:
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);
                        coordinates.z--;
                        break;
                    case 1:
                        this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
                        coordinates.x++;
                        break;
                    case 2:
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);
                        coordinates.z++;
                        break;
                    case 3:
                        this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
                        coordinates.x--;
                        break;
                }
                break;
            case actionList.turnRight:
                lookingTo--;
                if (lookingTo < 0)
                    lookingTo = 3;

                break;
            case actionList.turnLeft:
                lookingTo++;
                if (lookingTo > 3)
                    lookingTo = 0;
                break;
            case actionList.jump:

                break;

        }


    }


}
