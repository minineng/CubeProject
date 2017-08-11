using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public GameObject floor;
    public GameObject player;
    public GameObject enemy;
    public Vector3 cameraLookingPoint;
    public int turnCount;
    public bool planning;
    public bool running;
    public bool allPainted;
    public bool end;
    public Vector3 finishPosition;
    public float timeToNewAction;
    public float timeBetweenActions;
    
    public List<GameObject> elementsInPlay;

    public int matSize;
    public levelStructure testMap;
    
    public struct levelStructure
    {
        public char[,] mapMatrix;
        public int[,] heightMatrix;
        public List<GameObject> tileList;
        public int world;
        public int id;

    };

    // Use this for initialization
    void Start()
    {
        elementsInPlay = new List<GameObject>();
        floor = Resources.Load("Prefabs/Tile", typeof(GameObject)) as GameObject;
        enemy = Resources.Load("Prefabs/Enemy1", typeof(GameObject)) as GameObject;

        testMap.tileList = new List<GameObject>();

        turnCount = 0;
        planning = true;
        running = false;
        allPainted = false;
        end = false;
        timeBetweenActions = 1f;
        timeToNewAction = 0;

        mapMatrixGenerator();

        cameraLookingPoint = new Vector3(-3 + Mathf.Floor(matSize / 2), -0.33f, -2 + Mathf.Floor(matSize / 2));


        //mapMatrixPrint();
    }

    void Update()
    {
        //transform.Find("EventSystem").gameObject.SetActive(true);
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
                        if (elementsInPlay[i].name == "player")
                            elementsInPlay[i].GetComponent<mainElement>().makeAction(elementsInPlay[i].GetComponent<mainElement>().actionSet[turnCount]);
                    }

                    for (int i = 0; i < elementsInPlay.Count; i++)
                    {

                        if (elementsInPlay[i].name != "player")
                        {
                            if (elementsInPlay[i].GetComponent<mainElement>().actionSet.Count == 0)
                                elementsInPlay[i].GetComponent<EnemyController>().addActions(player.GetComponent<PlayerController>().actionSet.Count);

                            elementsInPlay[i].GetComponent<mainElement>().makeAction(elementsInPlay[i].GetComponent<mainElement>().actionSet[turnCount]);
                        }
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
            }
            if (player.GetComponent<PlayerController>().coordinates == finishPosition)
                end = true;

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
                    player.name = "Player";
                    if((testMap.world == 1 && (testMap.id == 1 || testMap.id == 2)) || testMap.world == 2 && testMap.id ==2)
                        player.GetComponent<PlayerController>().lookingTo = 2;
                    else
                        player.GetComponent<PlayerController>().lookingTo = 3;

                    elementsInPlay.Add(player);
                    
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

    public void mapMatrixInit(levelStructure level)
    {
        testMap = level;
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
