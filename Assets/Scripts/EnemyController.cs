using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : mainElement {

    public enum enemyType
    {
        turret,
        patroller


    };

    public enemyType type;

    // Use this for initialization
    void Start () {
        actionSet = new actionList[GetComponentInParent<LevelController>().maxTurns];

        switch (type)
        {
            case enemyType.turret:
                for (int i = 0; i < GetComponentInParent<LevelController>().maxTurns; i++)
                    addToActionSet(actionList.turnRight);
                break;
            case enemyType.patroller:

                break;
        }



		
	}
	
	// Update is called once per frame
	void Update () {
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
    }

    public void init()
    {
        actionSet = new actionList[GetComponentInParent<LevelController>().maxTurns];
    }

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

        }

        if (type == enemyType.turret)
            checkPlayer();
    }

    public void checkPlayer()
    {
        Vector3 playerPos = GetComponentInParent<LevelController>().player.GetComponent<PlayerController>().coordinates;

        switch (lookingTo)
        {//GetComponentInParent<LevelController>().matSize
            case 0:
                for (int i = (int)coordinates.z; i <= playerPos.z; i++)
                {
                    if (playerPos == new Vector3(coordinates.x, coordinates.y, i))
                        print("Disparo hacia arriba");
                }
                break;
            case 1:
                for(int i=0; i < coordinates.x; i++)
                {
                    if (playerPos == new Vector3(i, coordinates.y, coordinates.z))
                        print("Disparo hacia la izquierda");
                }
                break;

            case 2:
                for (int i = 0; i < coordinates.z; i++)
                {
                    if (playerPos == new Vector3(coordinates.x, coordinates.y, i))
                        print("Disparo hacia abajo");
                }
                break;
            case 3:
                for (int i = (int)coordinates.x; i <= playerPos.x; i++)
                {
                    if (playerPos == new Vector3(i, coordinates.y, coordinates.z))
                        print("Disparo hacia la derecha");
                }
                break;
        }

    }







}
