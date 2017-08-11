using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    private List<LevelController.levelStructure> levelList;

    public void Start()
    {
        levelList = new List<LevelController.levelStructure>();
        readMaps();
    }


    public void loadLevel(int index)
    {

        int auxWorld = ((int)index / 3) + 1;
        int auxIndex;

        if (auxWorld == 1)
            auxIndex = index;
        else
            auxIndex = index - 3;

        auxIndex++;

        string aux = ("Level " + auxWorld + "-" + auxIndex);
        print(aux);
        GameObject obj = new GameObject();
        obj.transform.SetParent(transform.parent.parent);
        obj.name = "Level Controller";
        obj.AddComponent<LevelController>();

        obj.GetComponent<LevelController>().mapMatrixInit(levelList[getMapById(auxWorld, auxIndex)]);

        transform.parent.parent.Find("CanvasGame").gameObject.SetActive(true);
        transform.parent.parent.Find("CanvasGame").GetComponent<CanvasController>().init();
        transform.parent.parent.Find("Scenery").transform.Find("Character").gameObject.SetActive(false);
        transform.parent.parent.Find("CanvasMenu").gameObject.SetActive(false);
        transform.parent.parent.Find("CanvasGame").SetParent(obj.transform);
        this.gameObject.SetActive(false);

    }

    public void loadLevel(int index, int world)
    {
        if (index != 0)
        {
            int auxWorld = world;
            int auxIndex = index;

            string aux = ("Level " + auxWorld + "-" + auxIndex);
            print(aux);
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform.parent.parent);
            obj.name = "Level Controller";
            obj.AddComponent<LevelController>();

            obj.GetComponent<LevelController>().mapMatrixInit(levelList[getMapById(auxWorld, auxIndex)]);

            transform.parent.parent.Find("CanvasGame").gameObject.SetActive(true);
            transform.parent.parent.Find("CanvasGame").GetComponent<CanvasController>().init();
            transform.parent.parent.Find("Scenery").transform.Find("Character").gameObject.SetActive(false);
            transform.parent.parent.Find("CanvasMenu").gameObject.SetActive(false);
            transform.parent.parent.Find("CanvasGame").SetParent(obj.transform);
            this.gameObject.SetActive(false);

        }
        else
        {


        }

    }

    private int getMapById(int world, int id)
    {
        int auxlevel = -1;

        for (int i = 0; i < levelList.Count; i++)
        {
            if (levelList[i].world == world && levelList[i].id == id)
                auxlevel = i;
        }
        return auxlevel;
    }

    private void readMaps()
    {

        FileInfo theSourceFile = null;
        StreamReader reader = null;
        string text = " "; // assigned to allow first line to be read below

        int matSize = 5;

        int index, world;

        index = 1;
        world = 1;

        theSourceFile = new FileInfo("mapList.txt");
        reader = theSourceFile.OpenText();
        string[] strArr;
        while (levelList.Count < 6)
        {

            text = reader.ReadLine();
            if (text == (world + "-" + index))
            {
                //print("lo tengo");

                LevelController.levelStructure auxEstructure = new LevelController.levelStructure();
                auxEstructure.id = index;
                auxEstructure.world = world;
                auxEstructure.mapMatrix = new char[matSize, matSize];
                auxEstructure.heightMatrix = new int[matSize, matSize];
                auxEstructure.tileList = new List<GameObject>();

                reader.ReadLine();

                for (int i = 0; i < matSize; i++)
                {
                    text = reader.ReadLine();
                    strArr = text.Split(' ');
                    for (int j = 0; j < matSize; j++)
                    {
                        //print("Intento parsear "+ strArr[j]);
                        auxEstructure.mapMatrix[i, j] = char.Parse(strArr[j]);
                    }
                }

                reader.ReadLine();

                for (int i = 0; i < matSize; i++)
                {
                    text = reader.ReadLine();
                    strArr = text.Split(' ');
                    for (int j = 0; j < matSize; j++)
                    {
                        auxEstructure.heightMatrix[i, j] = int.Parse(strArr[j]);
                    }
                }
                index++;
                if (index > 3)
                {
                    index = 1;
                    world++;
                }

                levelList.Add(auxEstructure);
            }
        }
    }

}
