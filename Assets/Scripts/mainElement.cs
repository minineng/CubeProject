using UnityEngine;
using System.Collections;

abstract public class mainElement : MonoBehaviour
{
    public int lookingTo;
    public Vector3 coordinates;
    public actionList[] actionSet;

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

    public int actionSetCount()
    {
        int auxCont = 0;
        for (int i = 0; i < actionSet.Length; i++)
        {
            if (actionSet[i] != actionList.nothing)
                auxCont++;
        }

        return auxCont;
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
        // print("Añado " + action);
        if (actionSetGapCount() > 0)
            actionSet[actionSetCount()] = action;
        else
        {
            print("Lista de acciones llena del "+transform.name);

            for (int i = 0; i < actionSet.Length; i++)
            {
                print("Accion " + i + " " + actionSet[i]);

            }

        }

    }

    public abstract void makeAction(actionList action);

}
