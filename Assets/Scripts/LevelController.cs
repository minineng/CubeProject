using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public GameObject flor;
	public GameObject character;
	public char[,] mapMatrix; 
	public int turnCount;
	public bool planning;

	// Use this for initialization
	void Start () {

		int matSize = 5;
		turnCount = 0;
		planning = true;

		mapMatrix = new char[matSize, matSize];

		print ("Matriz de "+mapMatrix.GetLength(0)+"x"+mapMatrix.GetLength(1));

		mapMatrixInit ();
		mapMatrixTest ();

		mapMatrixGenerator ();






		mapMatrixPrint ();
	}

	private void mapMatrixGenerator(){

		Vector3 position = new Vector3 (0,0,0);

		for (int i = 0; i < mapMatrix.GetLength (0); i++) {
			for (int j = 0; j < mapMatrix.GetLength (1); j++) {

				if (mapMatrix [i, j] != '-') {
					position.Set (-3 + j, -0.33f, -2+i);
					GameObject tile = Instantiate (flor, position, Quaternion.identity, this.transform);
					tile.name = "Tile " + i + ", " + j;
				}

				if (mapMatrix [i, j] == 'F') {
					position.Set (-3 + j, 1, -2+i);
					GameObject player = Instantiate (character, position, Quaternion.identity, this.transform);
					player.name = "Player";
				}
			}
		}

	}



	private void mapMatrixTest (){

		for (int i = 0; i < mapMatrix.GetLength (0); i++) {
			for (int j = 0; j < mapMatrix.GetLength (1); j++) {

				if(i==0 && (j==0 || j==1 || j==2 ))
					mapMatrix [i, j] = 'f';

				else if(j==2 && (i==1 || i==2 || i==3))
					mapMatrix [i, j] = 'f';

				else if(i==4 && (j==0 || j==1 || i==2 || i==3 || i==4))
					mapMatrix [i, j] = 'f';

				if (i == 0 && j == 1) 
					mapMatrix [i, j] = 'F';

			}
		}
	}

	public bool getPlanning(){
		return planning;
	}

	private void mapMatrixInit(){

		for (int i = 0; i < mapMatrix.GetLength (0); i++) {
			for (int j = 0; j < mapMatrix.GetLength (1); j++) {
				mapMatrix [i, j] = '-';
			}
		}
	}

	private void mapMatrixPrint(){
		string line;
		for (int i = 0; i < mapMatrix.GetLength (0); i++) {
			line = "(";
			for (int j = 0; j < mapMatrix.GetLength (1); j++) {
				line += " "+mapMatrix[i,j];
				if (j < mapMatrix.GetLength (1) - 1)
					line += ",";
			}
			print (line+" )");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		


	}

}
