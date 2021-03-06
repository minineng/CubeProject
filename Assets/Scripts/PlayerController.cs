﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : mainElement
{

    public bool alive;


    // Use this for initialization
    void Start()
    {
        alive = true;
        actionSet = new List<actionList>();

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
                actionSet.Add(actionList.forward);
            if (Input.GetButtonDown("Right"))
                actionSet.Add(actionList.turnRight);
            if (Input.GetButtonDown("Left"))
                actionSet.Add(actionList.turnLeft);
            if (Input.GetButtonDown("Backward"))
                actionSet.Add(actionList.backward);
            if (Input.GetButtonDown("ExecuteActions"))
                executeActions();
        }

        if (transform.position.y < -2)
            alive = false;

    }

    public void executeActions()
    {
        if (GetComponentInParent<LevelController>().planning)
        {
            GetComponentInParent<LevelController>().running = true;
            GetComponentInParent<LevelController>().planning = false;
        }
        else
        {
            if (GetComponentInParent<LevelController>().running)
                GetComponentInParent<LevelController>().running = false;
            else
                GetComponentInParent<LevelController>().running = true;
        }
    }
    /*
    public void executeActions()
    {
        if (GetComponentInParent<LevelController>().planning)
        {
            GetComponentInParent<LevelController>().running = true;
            GetComponentInParent<LevelController>().planning = false;
            timeToNewAction = Time.time + timeBetweenActions;
            print("Ejecuto las acciones");
            actionTurn = 0;
        }
    }*/

    /*
public void addToActionSet(actionList action)
{
   // print("Añado " + action);
    if (actionSetGapCount() > 0)
        actionSet[actionSetCount()] = action;
    else
    {
        print("Lista de acciones llena");

        for (int i = 0; i < actionSet.Length; i++)
        {
            print("Accion " + i + " " + actionSet[i]);

        }

    }

}*/

    public override void makeAction(actionList action)
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
                float jumptHeight = 0.5f;
                switch (lookingTo)
                {
                    case 0:
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + jumptHeight, this.transform.position.z + 1);
                        coordinates.z++;
                        break;
                    case 1:
                        this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y + jumptHeight, this.transform.position.z);
                        coordinates.x--;
                        break;
                    case 2:
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + jumptHeight, this.transform.position.z - 1);
                        coordinates.z--;
                        break;
                    case 3:
                        this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y + jumptHeight, this.transform.position.z);
                        coordinates.x++;
                        break;
                }
                if (GetComponentInParent<LevelController>().getTileByCoordinates(new Vector2(coordinates.x, coordinates.z)) != null)
                {
                    GetComponentInParent<LevelController>().getTileByCoordinates(new Vector2(coordinates.x, coordinates.z)).GetComponent<tileController>().paintThisTile();
                    coordinates = GetComponentInParent<LevelController>().getTileByCoordinates(new Vector2(coordinates.x, coordinates.z)).GetComponent<tileController>().coordinates;
                }
                break;
            case actionList.execute:
                executeActions();
                break;
            case actionList.wait:

                break;

        }


    }


}
