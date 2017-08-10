using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public GameObject floor;
    //public GameObject character;
    public GameObject player;
    public GameObject enemy;
    public Vector3 cameraLookingPoint;
    public int turnCount;
    public bool planning;
    public bool running;
    public bool allPainted;
    public Vector3 finishPosition;
    private float timeToNewAction;
    public float timeBetweenActions;
    
    public List<GameObject> elementsInPlay;

    public int matSize;
    levelStructure testMap;
    
    public struct levelStructure
    {
        public char[,] mapMatrix;
        public int[,] heightMatrix;
        public List<GameObject> tileList;
        //public int setID;
        //public int id;

    };

    // Use this for initialization
    void Start()
    {
        

        matSize = 5;
        testMap.mapMatrix = new char[matSize, matSize];
        testMap.heightMatrix = new int[matSize, matSize];

        elementsInPlay = new List<GameObject>();
        floor = Resources.Load("Prefabs/Tile", typeof(GameObject)) as GameObject;
        turnCount = 0;
        planning = true;
        running = false;
        allPainted = false;
        timeBetweenActions = 1f;
        timeToNewAction = 0;

        mapMatrixInit();
        mapMatrixTest();

        mapMatrixGenerator();

        cameraLookingPoint = new Vector3(-3 + Mathf.Floor(matSize / 2), -0.33f, -2 + Mathf.Floor(matSize / 2));


        //mapMatrixPrint();
    }

    /*
    void Update()
    {
        if (planning && !running)
            timeToNewAction = Time.time + timeBetweenActions;

        if (running)
        {
            if (Time.time > timeToNewAction)
            {
                print("Turnos a hacer "+ player.GetComponent<PlayerController>().actionSet.Count);
                if (turnCount < player.GetComponent<PlayerController>().actionSet.Count)
                {
                    print("asddfasdf");
                    for (int i = 0; i < elementsInPlay.Count; i++)
                    {
                        elementsInPlay[i].GetComponent<mainElement>().makeAction(elementsInPlay[i].GetComponent<mainElement>().actionSet[turnCount]);
                        print("Soy "+elementsInPlay[i].name);
                    }
                    print("ASEREJE");
                    turnCount++;
                    timeToNewAction = Time.time + timeBetweenActions;
                    if (getTileByCoordinates(player.GetComponent<PlayerController>().coordinates) != null)
                    {
                        getTileByCoordinates(player.GetComponent<PlayerController>().coordinates).GetComponent<tileController>().paintThisTile();
                        //print("Quedan "+ (getPaintedTilesCount() - testMap.tileList.Count)+" tiles por pintar");
                        if (getPaintedTilesCount() == (testMap.tileList.Count))
                            allPainted = true;
                    }
                }
            }
            if (player.GetComponent<PlayerController>().coordinates == finishPosition)
                print("Has llegado a la meta");
            if (allPainted)
                print("Has pintado todos los tiles");

        }
        else
        {
            timeToNewAction = Time.time + timeBetweenActions;

        }


    }*/

    void Update()
    {
        if (planning && !running)
            timeToNewAction = Time.time + timeBetweenActions;

        if (running)
        {
            if (Time.time > timeToNewAction)
            {
                if (turnCount < player.GetComponent<PlayerController>().actionSet.Count)
                {
                    for (int i = 0; i < elementsInPlay.Count; i++)
                    {
                        if (elementsInPlay[i].GetComponent<mainElement>().actionSet.Count == 0)
                            elementsInPlay[i].GetComponent<EnemyController>().addActions(player.GetComponent<PlayerController>().actionSet.Count);

                        elementsInPlay[i].GetComponent<mainElement>().makeAction(elementsInPlay[i].GetComponent<mainElement>().actionSet[turnCount]);
                        //print("Soy " + elementsInPlay[i].name);
                    }

                    turnCount++;
                    timeToNewAction = Time.time + timeBetweenActions;
                    if (getTileByCoordinates(player.GetComponent<PlayerController>().coordinates) != null)
                    {
                        getTileByCoordinates(player.GetComponent<PlayerController>().coordinates).GetComponent<tileController>().paintThisTile();
                        //print("Quedan "+ (getPaintedTilesCount() - testMap.tileList.Count)+" tiles por pintar");
                        if (getPaintedTilesCount() == (testMap.tileList.Count))
                            allPainted = true;
                    }
                }
                /*turnCount++;
                timeToNewAction = Time.time + timeBetweenActions;
                print("lol");*/
            }
            if (player.GetComponent<PlayerController>().coordinates == finishPosition)
                print("Has llegado a la meta");
            if (allPainted)
                print("Has pintado todos los tiles");

        }
        else
        {
            timeToNewAction = Time.time + timeBetweenActions;

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
                    if(testMap.mapMatrix[i, j] != 'T')
                        testMap.tileList.Add(tile);

                    if (testMap.mapMatrix[i, j] == 'C')
                        tile.GetComponent<tileController>().paintThisTile();

                    if (testMap.mapMatrix[i, j] == 'E')
                    {
                        tile.GetComponent<MeshRenderer>().material = tile.GetComponent<tileController>().finishLineMaterial;
                        finishPosition = new Vector3(j, testMap.heightMatrix[i, j], i);
                    }
                    tile.name = "Tile " + i + ", " + j;
                }

                if (testMap.mapMatrix[i, j] == 'C')
                {
                    position.Set(-3 + j, 1, -2 + i);
                    player = Instantiate(Resources.Load("Prefabs/Character") as GameObject, position, Quaternion.identity, this.transform);
                    player.GetComponent<PlayerController>().coordinates = new Vector3(j, testMap.heightMatrix[i, j], i);
                    elementsInPlay.Add(player);
                    player.name = "Player";
                }
                if (testMap.mapMatrix[i, j] == 'T')
                {
                    position.Set(-3 + j, 0.33f * (testMap.heightMatrix[i, j] + 2), -2 + i);
                    GameObject auxEnemy = Instantiate(enemy, position, Quaternion.identity, this.transform);
                    auxEnemy.GetComponent<EnemyController>().coordinates = new Vector3(j, testMap.heightMatrix[i, j], i);
                    auxEnemy.GetComponent<EnemyController>().type = EnemyController.enemyType.turret;
                    auxEnemy.GetComponent<EnemyController>().lookingTo = Random.Range(0, 4);
                    elementsInPlay.Add(auxEnemy);
                    auxEnemy.name = "Turret";
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

                if (i == 0)
                    testMap.mapMatrix[i, j] = 'f';

                else if (j == 2 && (i == 1 || i == 2 || i == 3))
                    testMap.mapMatrix[i, j] = 'f';

                else if (i == 4 && (j == 0 || j == 1 || i == 2 || i == 3 || i == 4))
                    testMap.mapMatrix[i, j] = 'f';

                if (i == 0 && j == 1)
                    testMap.mapMatrix[i, j] = 'C';

                if (i == 2 && (j == 0 || j == 4))
                    testMap.mapMatrix[i, j] = 'T';

                if (i == 4 && j == 4)
                    testMap.mapMatrix[i, j] = 'E';

            }
        }
    }

    public int getPaintedTilesCount()
    {
        int paintedTiles = 0;
        for (int i = 0; i < testMap.tileList.Count; i++)
        {
            if (testMap.tileList[i].GetComponent<tileController>().painted)
                paintedTiles++;
        }

        return (paintedTiles);

    }

    private void mapMatrixInit()
    {
        testMap.tileList = new List<GameObject>();
        for (int i = 0; i < testMap.mapMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < testMap.mapMatrix.GetLength(1); j++)
            {
                testMap.mapMatrix[i, j] = '-';
                testMap.heightMatrix[i, j] = i;
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

    public GameObject getTileByCoordinates(Vector2 coordinates)
    {
        GameObject aux = null;

        for (int i = 0; i < testMap.tileList.Count; i++)
        {

            if (coordinates == new Vector2(testMap.tileList[i].GetComponent<tileController>().coordinates.x, testMap.tileList[i].GetComponent<tileController>().coordinates.z))
                aux = testMap.tileList[i];
        }

        return aux;
    }

}
