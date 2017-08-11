using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{

    private Button level11;
    private Button level12;
    private Button level13;
    private Button level21;
    private Button level22;
    private Button level23;

    // Use this for initialization
    void Start()
    {
        level11 = transform.GetChild(0).GetComponent<Button>();
        level12 = transform.GetChild(1).GetComponent<Button>();
        level13 = transform.GetChild(2).GetComponent<Button>();
        level21 = transform.GetChild(3).GetComponent<Button>();
        level22 = transform.GetChild(4).GetComponent<Button>();
        level23 = transform.GetChild(5).GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
     * 
     * public void goToMap(int index){
		string aux = ("Mapa "+ index);
		print (aux);
		Scene escena = SceneManager.CreateScene (aux);
		SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene().buildIndex);
		SceneManager.SetActiveScene (escena);
		GameObject obj = new GameObject ();
		obj.AddComponent<MapGenController> ();
		obj.GetComponent<MapGenController> ().init (ListaMapas [index].dificultad, ListaMapas [index].NumPisos, ListaMapas [index].NumHabitaciones, ListaMapas [index].estilo);

		//script = new MapGenController(ListaMapas [index].dificultad, ListaMapas [index].NumPisos, ListaMapas [index].NumHabitaciones, ListaMapas [index].estilo);
		//obj.AddComponent (script);
	}
     */


    public void loadLevel(int index)
    {
        int auxWorld = ((int)index / 3) + 1;
        int auxIndex;

        if (auxWorld == 1)
            auxIndex = index;
        else
            auxIndex = index - 3;

        auxIndex++;

        string aux = ("Level " + auxWorld+"-"+auxIndex);
        print(aux);
        Scene escena = SceneManager.CreateScene(aux);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.SetActiveScene(escena);
        GameObject obj = new GameObject();
        obj.AddComponent<LevelController>();
        obj.GetComponent<LevelController>().mapMatrixInit(getMatrix(auxWorld, auxIndex));





    }

    private LevelController.levelStructure getMatrix(int world, int index)
    {
        print("Intento cargar el nivel "+world+"-"+index);
        FileInfo theSourceFile = null;
        StreamReader reader = null;
        string text = " "; // assigned to allow first line to be read below

        int matSize = 5;

        LevelController.levelStructure auxEstructure = new LevelController.levelStructure();
        auxEstructure.id = index;
        auxEstructure.world = world;
        auxEstructure.mapMatrix = new char[matSize, matSize];
        auxEstructure.heightMatrix = new int[matSize, matSize];
        auxEstructure.tileList = new List<GameObject>();

        theSourceFile = new FileInfo("mapList.txt");
        reader = theSourceFile.OpenText();
        bool listo = false;
        string[] strArr;
        while (!listo)
        {
            text = reader.ReadLine();
            if (text == (world + "-" + index))
            {
                print("lo tengo");
                reader.ReadLine();

                for(int i = 0; i < matSize; i++)
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

                listo = true;
            }
        }
        return auxEstructure;
    }
    
}
