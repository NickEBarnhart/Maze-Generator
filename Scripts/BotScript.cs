using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotScript : MonoBehaviour {

	public networkCalculation brain;
	public Button deleteButton;
	public float positionX;
	public float positionZ;
	public float moveDelay;
	private Transform trans;

	int randPath;
	int expectedPath;
	public bool doneOnce = true;
	public bool turnback = false;


	int pieces;
	int chase;
	Vector3 basisBlock;


	List<Vector3> mainPath = new List<Vector3>();
	List<int> pathValues = new List<int> ();



	public IEnumerator movementScript(List<Vector3> recievePath, GameObject bot){
		mainPath.Clear();
		bot.transform.localPosition = new Vector3 (0f, 1f, 0f);
		int loopBreaker = 0;


		pieces = recievePath.Count;
		brain.counter = 0;
		brain.firstSet2 = false;
		turnback = false;
		drawPath (recievePath);

		while (mainPath.Count < expectedPath) {
			loopBreaker++;
			drawPath (recievePath);

			if (loopBreaker > 1000) {
				break;
			}
		}

		brain.firstSet2 = true;



		for (int i = 0; i < brain.pathMarkerCoordinates.Count; i++) {
			print (brain.pathMarkerCoordinates[i]);


		}

		for(int i = 0; i < mainPath.Count; i++){
			print (mainPath[i]);
		}



		WaitForSeconds timer = new WaitForSeconds (moveDelay);
		print (mainPath.Count);
		for (int i = 0; i < mainPath.Count; i++) {
		
			bot.transform.position = new Vector3 (mainPath [i].x, 1f, mainPath [i].z);
			yield return timer;
		}
		
	}






	void drawPath(List<Vector3> blockCoordinates){
		int changePath;
		int botmarker;
		int breakLoop = 0;

		if (doneOnce) {
			randPath = brain.botPath ();
			expectedPath = randPath;
			doneOnce = false;
		}
			

		Vector3 storageUnit;

		if (!turnback) {
			print ("Chase called");
			chase = brain.incrementProgres ();
			turnback = true;
		}

		mainPath.Clear ();

		basisBlock = new Vector3 (0f, 0f, 0f);

		print ("Chase:" + chase);
		for (int i = 0; i < chase; i++) {
			mainPath.Add (brain.pathMarkerCoordinates [i]);
		}

		basisBlock = mainPath [chase-1];

		for (int i = 0; i < randPath-chase; i++) {
			storageUnit = basisBlock;
			changePath = Random.Range (1, 4);
			botmarker = brain.botMarker();
			breakLoop += 1;

			if (!turnback) {
				i = i - 1;
			}


			if (changePath == 1) {
				if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z)))) {
					//basisBlock = new Vector3 (basisBlock.x + 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//basisBlock = blockCoordinates [];
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z)))) {
					//basisBlock = new Vector3 (basisBlock.x - 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z + 2);				
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z - 2);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else {
					i = randPath;
				}
			}

			if (changePath == 2) {
				if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z)))) {
					
					mainPath.Add (basisBlock);
			
				

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z)))) {
					//basisBlock = new Vector3 (basisBlock.x - 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z + 2);				
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z - 2);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else {
					i = randPath;
				}
			}

			if (changePath == 3) {
				if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)))) {
					//basisBlock = new Vector3 (basisBlock.x + 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//basisBlock = blockCoordinates [];
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)))) {
					//basisBlock = new Vector3 (basisBlock.x - 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z)))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z + 2);				
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z )))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z - 2);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else {
					i = randPath;
				}
			}

			if (changePath == 4) {
				if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z - 2)))) {
					//basisBlock = new Vector3 (basisBlock.x + 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//basisBlock = blockCoordinates [];
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x, 0f, storageUnit.z + 2)))) {
					//basisBlock = new Vector3 (basisBlock.x - 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z )) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x + 2, 0f, storageUnit.z)))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z + 2);				
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else if (blockCoordinates.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z)) && !(mainPath.Contains (basisBlock = new Vector3 (storageUnit.x - 2, 0f, storageUnit.z)))) {
					//basisBlock = new Vector3 (basisBlock.x, 0f, basisBlock.z - 2);
					mainPath.Add (basisBlock);
					//x = blockCoordinates.Count;

				} else {
					i = randPath;
				}
			}


			/*if (brain.botCheckMove (basisBlock, chase)) {
				print ("adding basis block");
				//mainPath.Add (basisBlock);
			} else {
				print ("decrementing progress");
				//brain.botDecrementProgres ();
				//turnback = true;

			}*/
				

		}
			

		}


	}
	