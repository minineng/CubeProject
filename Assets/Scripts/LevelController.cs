using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public GameObject floor;
    public GameObject character;
    public Vector3 cameraLookingPoint;

    public int turnCount;

    public bool planning;
    public bool running;

    public GameObject player;

    levelStructure testMap;

    public struct levelStructure
    {
        public char[,] mapMatrix;
        public List<GameObject> tileList;
        public int[,] heightMatrix;
        public int setID;
        public int id;
       
    };

    // Use this for initialization
    void Start()
    {

        floor = Resources.Load("Prefabs/Tile", typeof(GameObject)) as GameObject;

        int matSize = 5;
        turnCount = 0;
        planning = true;
        running = false;

        testMap.mapMatrix = new char[matSize, matSize];
        testMap.heightMatrix = new int[matSize, matSize];

        //print("Matriz de " + mapMatrix.GetLength(0) + "x" + mapMatrix.GetLength(1));

        mapMatrixInit();
        mapMatrixTest();

        mapMatrixGenerator();

        cameraLookingPoint = new Vector3(-3 + Mathf.Floor(matSize / 2), -0.33f, -2 + Mathf.Floor(matSize / 2));


        //mapMatrixPrint();
    }

    void Update()
    {

        if (running)
        {



        }


    }

    private void mapMatrixGenerator()
    {

        Vector3 position = new Vector3(0, 0, 0);

        for (int i = 0; i < testMap.mapMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < testMap.mapMatrix.GetLength(1); j++)
            {

                if (testMap.mapMatrix[i, j] != '-')
                {
                    position.Set(-3 + j, 0.33f * testMap.heightMatrix[i, j], -2 + i);
                    GameObject tile = Instantiate(floor, position, Quaternion.identity, this.transform);
                    tile.GetComponent<tileController>().coordinates = new Vector3(j, testMap.heightMatrix[i, j], i);
                    testMap.tileList.Add(tile);

                    if (testMap.mapMatrix[i, j] == 'F')
                        tile.GetComponent<tileController>().paintThisTile(true);

                    tile.name = "Tile " + i + ", " + j;
                }

                if (testMap.mapMatrix[i, j] == 'F')
                {
                    position.Set(-3 + j, 1, -2 + i);
                    player = Instantiate(character, position, Quaternion.identity, this.transform);
                    player.GetComponent<PlayerController>().coordinates = new Vector3(j, 0, i);

                    player.name = "Player";
                }
            }
        }

    }

    private void mapMatrixTest()
    {

        for (int i = 0; i < testMap.mapMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < testMap.mapMatrix.GetLength(1); j++)
            {

                if (i == 0 && (j == 0 || j == 1 || j == 2))
                    testMap.mapMatrix[i, j] = 'f';

                else if (j == 2 && (i == 1 || i == 2 || i == 3))
                    testMap.mapMatrix[i, j] = 'f';

                else if (i == 4 && (j == 0 || j == 1 || i == 2 || i == 3 || i == 4))
                    testMap.mapMatrix[i, j] = 'f';

                if (i == 0 && j == 1)
                    testMap.mapMatrix[i, j] = 'F';

            }
        }
    }

    private void mapMatrixInit()
    {
        testMap.tileList = new List<GameObject>();
        for (int i = 0; i < testMap.mapMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < testMap.mapMatrix.GetLength(1); j++)
            {
                testMap.mapMatrix[i, j] = '-';
                testMap.heightMatrix[i, j] = 0;
            }
        }
    }

    private void mapMatrixPrint()
    {
        string line;
        for (int i = 0; i < testMap.mapMatrix.GetLength(0); i++)
        {
            line = "(";
            for (int j = 0; j < testMap.mapMatrix.GetLength(1); j++)
            {
                line += " " + testMap.mapMatrix[i, j];
                if (j < testMap.mapMatrix.GetLength(1) - 1)
                    line += ",";
            }
            print(line + " )");
        }
    }

    public Vector3 getCameraLookingPoint()
    {
        return cameraLookingPoint;
    }

    public GameObject getTileByCoordinates(Vector3 coordinates)
    {
        GameObject aux = null;

        for (int i = 0; i < testMap.tileList.Count; i++)
        {

            if (coordinates == testMap.tileList[i].GetComponent<tileController>().coordinates)
                aux = testMap.tileList[i];
        }

        return aux;
    }

}
