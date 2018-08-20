using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeConstructor : MonoBehaviour {

	public int mazeSize;
	public int hallwaySize;
	public double botSpeed;
	int pickDirection;
	int rndPosition;
	int objectivePath;
	Quaternion direction;
	Vector3 basisBlock;


	public bool startHub = false;
	public float buildDelay;
	public Transform nextSpot;
	public mazePassage recentBlock;
	public mazePassage hubBlock;
	public bool sessionCheck = false;
	public networkCalculation neuralMind;

	//network items
	public List<int> markerValues = new List<int>();
	public int setPath;

	//maze passage block
	public mazePassage[] passagePrefab; //normal passage prefabs : static
	public GameObject[] mazeMarkers; //maze detail prefabs
	public GameObject[] pathMazeMarkers;
	public GameObject[] hubPathMazeMarkers;


	//Game Object Lists
	public List<mazePassage> allBlocks = new List<mazePassage>();
	public List<GameObject> trailBlocks = new List<GameObject> ();



	//Bot Objects
	public GameObject Bot;
	public BotScript botAction;
	public GameObject robit;

	//List block
	List<Transform> portals = new List<Transform> (); //all entrances to a block
	List<Transform> passgeWay = new List<Transform> (); //entrances to an in progress hallway 
	List<Vector3> blockCoordinates = new List<Vector3>(); //locations of each block
	List<Vector3> mainPath = new List<Vector3>(); //coordinates of main path
	List<Transform> passages = new List<Transform>(); //transforms of all blocks
	List<int> markValues = new List<int>(); // values for main path

	//Values for maze minus main path values and coordinates
	List<Vector3> otherCoordinates = new List<Vector3>(); //coordinates of everything else
	List<int> otherValues = new List<int>(); // values for every other block

	//special
	List<Vector3> allMarkerCoordinates = new List<Vector3>();
	List<int> allMarkers = new List<int>();



	public IEnumerator Generate(){
		WaitForSeconds timer = new WaitForSeconds (buildDelay);

	
		//passages = new mazePassage[mazeSize];
		for (int i = 0; i < mazeSize; i++) {
			for (int v = 0; v < hallwaySize; v++) {
				yield return timer;
				buildPassage (i, v);
			}
			
		}

		createObjective ();

	}



	private void createObjective(){

		print ("Starting objective route construction");
		double min = mazeSize * 2;
		double max = mazeSize * 4;
		int randPath = neuralMind.calculatePath (min,max,mazeSize);
		int changePath;
		Vector3 storageUnit;

		mainPath.Clear ();

		basisBlock = hubBlock.transform.position;
		mainPath.Add (basisBlock);

		if (sessionCheck) {
			neuralMind.firstSet = true;
		}
		if (!sessionCheck) {
			randPath = setPath;
		}


		for (int i = 0; i < randPath-1; i++) {
			storageUnit = basisBlock;
			changePath = Random.Range (1, 4);

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
					//basisBlock = new Vector3 (basisBlock.x + 2, 0f, basisBlock.z);
					mainPath.Add (basisBlock);
					//basisBlock = blockCoordinates [];
					//x = blockCoordinates.Count;

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
			
		}

		neuralMind.pathMarkerCoordinates = mainPath;
		createTrail ();


	}


	void createTrail(){

		GameObject wayPoint;
		allMarkerCoordinates.Clear ();
		allMarkers.Clear ();
 		print ("Maze Path: " + mainPath.Count);

		if (mainPath.Count < setPath) {

			createObjective ();

		} else {
			
				
			for (int i = 0; i < mainPath.Count; i++) {

				int womboCombo = neuralMind.calculateMarkers ();

				if (!sessionCheck) {
					womboCombo = (markerValues [i]);
				}


				if (i == 0) {
					wayPoint = Instantiate (hubPathMazeMarkers [womboCombo]) as GameObject;
					wayPoint.transform.position = mainPath [i];
					trailBlocks.Add (wayPoint);
					markValues.Add (womboCombo);


					allMarkerCoordinates.Add (mainPath [i]);
					allMarkers.Add (womboCombo);
				}


				else if (i < mainPath.Count - 1) {
						
				


						
					wayPoint = Instantiate (pathMazeMarkers [womboCombo]) as GameObject;
					wayPoint.transform.position = mainPath [i];
					trailBlocks.Add (wayPoint);
					markValues.Add (womboCombo);


					allMarkerCoordinates.Add (mainPath [i]);
					allMarkers.Add (womboCombo);
				
				} else {
					wayPoint = Instantiate (mazeMarkers [5]) as GameObject;
					wayPoint.transform.position = mainPath [i];
					trailBlocks.Add (wayPoint);
					markValues.Add (5);

				}



			}

			//print ("Block coordinates list:" + blockCoordinates.Count);
			for(int b = 0; b < blockCoordinates.Count; b++){
				int womboCombo = Random.Range (0, 5);

				if (mainPath.Contains (blockCoordinates [b])) {
					//do nothing
				} else {
					wayPoint = Instantiate (mazeMarkers [womboCombo]) as GameObject;
					wayPoint.transform.position = blockCoordinates [b];
					otherValues.Add (womboCombo);
					otherCoordinates.Add (blockCoordinates [b]);
					trailBlocks.Add (wayPoint);

					allMarkerCoordinates.Add (blockCoordinates[b]);
					allMarkers.Add (womboCombo);

				}
			}
				

			neuralMind.allMarkerCoordinates = allMarkerCoordinates;
			neuralMind.allMarkers = allMarkers;
			neuralMind.expectedPath = mainPath.Count;
			createApproach (markValues, mainPath.Count);


			

			botStart ();

		}
	}


	private void botStart(){

		robit = Instantiate (Bot) as GameObject;
		botAction.moveDelay = (float)botSpeed;
		botAction.doneOnce = true;
		StartCoroutine (botAction.movementScript (blockCoordinates, robit));

	}

	public void stopAll(){
		StopAllCoroutines ();
	}

	private void createApproach(List<int> x, int path){
		if (sessionCheck) {
			for (int i = 0; i < x.Count; i++) {
				markerValues.Add (x [i]);
			}
			setPath = path;
			sessionCheck = false;
		}

	}







	//Build Maze Section Below
	private void buildPassage(int a, int b){

		mazePassage newBlock;
		Transform connectedPortal;
			
		newBlock = Instantiate (passagePrefab[0]) as mazePassage; //place block
		allBlocks.Add(newBlock);


		//append array for block entrances
		for (int x = 0; x < newBlock.selfObject.entrances.Length; x++) {
			portals.Add(newBlock.selfObject.entrances [x].transform); //store all passages

			if(startHub){
			passgeWay.Add (recentBlock.selfObject.entrances [x].transform); //store passages of newest block
			}
		}

		//place hub : only activates once
		if (!startHub) {
			newBlock.transform.localPosition = new Vector3 (0f, 0f, 0f);
			blockCoordinates.Add (newBlock.transform.localPosition);

			rndPosition = Random.Range (0, portals.Count); //select random passage from previous block
			pickDirection = Random.Range(0,4);
			direction = newBlock.selfObject.entrances [pickDirection].transform.rotation; //store rotation quanterion using rndposition to select passage.
			hubBlock = newBlock;
		}
			
		else {

			if (b == 0) { //initiate new passageway

				rndPosition = Random.Range (0, portals.Count); //select random passage from previous block
				nextSpot = portals [rndPosition]; //position of previous block facing the same direction
				pickDirection = Random.Range(0,4);


				direction = nextSpot.transform.rotation; //store rotation quanterion using rndposition to select passage.
				newBlock.transform.localPosition = nextSpot.position;

				if (direction.eulerAngles.y == 0) {
					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x+1, 0f, newBlock.transform.position.z);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x+2, 0f, newBlock.transform.position.z);
							i = 0;

						}
					}

					connectedPortal = newBlock.selfObject.entrances [2].transform;
					portals.Remove (connectedPortal);

				} if (direction.eulerAngles.y == 90 || direction.eulerAngles.y == -270) {

					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z - 1);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z - 2);
							i = 0;

						}
					}
					connectedPortal = newBlock.selfObject.entrances [1].transform;
					portals.Remove (connectedPortal);

				} if (direction.eulerAngles.y == 180 || direction.eulerAngles.y == -180) {

					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x-1, 0f, newBlock.transform.position.z);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x-2, 0f, newBlock.transform.position.z);
							i = 0;
						}
					}

					connectedPortal = newBlock.selfObject.entrances [0].transform;
					portals.Remove (connectedPortal);

				} if (direction.eulerAngles.y == 270 || direction.eulerAngles.y == -90) {

					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z + 1);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z + 2);
							i = 0;
						}
					}

					connectedPortal = newBlock.selfObject.entrances [3].transform;
					portals.Remove (connectedPortal);

				}

				blockCoordinates.Add (newBlock.transform.localPosition);
				portals.Remove (nextSpot);




			} else { //continue passageway

				if (direction.eulerAngles.y == 0) {

					nextSpot = passgeWay [0]; //position of previous block facing the same direction
					newBlock.transform.localPosition = nextSpot.position;
					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x + 1, 0f, newBlock.transform.position.z);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x+2, 0f, newBlock.transform.position.z);
							i = 0;
						}
					}

					connectedPortal = newBlock.selfObject.entrances [2].transform;
					portals.Remove (connectedPortal);

				}  if (direction.eulerAngles.y == 90 || direction.eulerAngles.y == -270) {

					nextSpot = passgeWay [3]; 
					newBlock.transform.localPosition = nextSpot.position;
					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z - 1);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z - 2);
							i = 0;
						}
					}

					connectedPortal = newBlock.selfObject.entrances [3].transform;
					portals.Remove (connectedPortal);

				} if (direction.eulerAngles.y == 180 || direction.eulerAngles.y == -180) {

					nextSpot = passgeWay [2]; 
					newBlock.transform.localPosition = nextSpot.position;
					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x - 1, 0f, newBlock.transform.position.z);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x - 2, 0f, newBlock.transform.position.z);
							i = 0;
						}
					}

					connectedPortal = newBlock.selfObject.entrances [0].transform;
					portals.Remove (connectedPortal);

				} if (direction.eulerAngles.y == 270 ||  direction.eulerAngles.y == -90) {

					nextSpot = passgeWay [1]; 
					newBlock.transform.localPosition = nextSpot.position;
					newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z + 1);

					for (int i = 0; i < blockCoordinates.Count; i++) {
						if (blockCoordinates [i] == newBlock.transform.localPosition) {
							newBlock.transform.localPosition = new Vector3 (newBlock.transform.position.x, 0f, newBlock.transform.position.z + 2);
							i = 0;
						}
					}

					connectedPortal = newBlock.selfObject.entrances [1].transform;
					portals.Remove (connectedPortal);

				}

				blockCoordinates.Add (newBlock.transform.localPosition);
				portals.Remove (recentBlock.selfObject.entrances[pickDirection].transform);

			}
		}

		if (startHub) {
			passgeWay.RemoveRange (0, passgeWay.Count);
		}

		recentBlock = newBlock;
		passages.Add(newBlock.transform);
		startHub = true;
	} 

}
