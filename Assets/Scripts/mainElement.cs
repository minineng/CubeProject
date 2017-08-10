using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class mainElement : MonoBehaviour
{
    public int lookingTo;
    public Vector3 coordinates;
    public List<actionList> actionSet;

    public enum actionList
    {
        nothing,
        forward,
        backward,
        turnRight,
        turnLeft,
        jump,
        wait,
        execute,

    };

    public abstract void makeAction(actionList action);

}
