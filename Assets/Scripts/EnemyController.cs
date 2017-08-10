using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : mainElement {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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


    }
}
