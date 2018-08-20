
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour {

	public networkCalculation netScript;
	public GameObject startCanvas;
	public GameObject duringCanvas;
	public Button deleteButton;
	public Text mazeSizebox;
	public Text mazespeedbox;
	public Text botspeedbox;

	//text items
	public Text pathL;
	public Text botPathL;
	public Text avgWeight;
	public Text solvePercentage;

	string ar  = "Path Length: ";
	string br  = "Bot Path Length: ";
	string cr  = "Bot Avg. Weight: ";
	string dr  = "Solve %:";


	//Maze Size
	Button smallMazeButton;
	int mazeSizeSetting = 40;
	int mazeCorridorSetting = 6;
	string a = "default";

	//Maze Speed
	double mazeSpeedSetting = .1;
	string b = "default";

	//bot Speed
	public double botSpeedSetting = 1;
	string c = "default";


	public MazeConstructor mazePrefab;
	private MazeConstructor mazeInstance;
	//List<mazePassage> blockList = new List<mazePassage> ();

	//Neural net inputs
	List<int> markerValues = new List<int>();
	bool sessionCheck = true;
	int mazeLength;

	// Use this for initialization
	void Start () {

		 mazeSizeSetting = 40;
		 mazeCorridorSetting = 6;
		 mazeSpeedSetting = .1;
		 botSpeedSetting = 1;

		//duringCanvas.SetActive (true);
		//startCanvas.SetActive (true);


		duringCanvas.GetComponent<Canvas> ().enabled = false;
		startCanvas.GetComponent<Canvas> ().enabled = true;


		
	}
	
	// Update is called once per frame
	void Update () {

		mazeSizebox.text = a;
		mazespeedbox.text = b;
		botspeedbox.text = c;

		pathL.text = ar;
		botPathL.text = br;
		avgWeight.text =  cr;
		solvePercentage.text = dr;

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}



	}

	public void loadScene(){
		SceneManager.LoadScene(0);
	}
		
	public void beginSession(){

		deleteButton.enabled = false;

		duringCanvas.GetComponent<Canvas> ().enabled = true;
		startCanvas.GetComponent<Canvas> ().enabled = false;





		mazeInstance = Instantiate (mazePrefab) as MazeConstructor;

		mazeInstance.mazeSize = mazeSizeSetting;
		mazeInstance.hallwaySize = mazeCorridorSetting;
		mazeInstance.buildDelay = (float)mazeSpeedSetting;
		mazeInstance.botSpeed = botSpeedSetting;


		if (sessionCheck) {
			establishSession ();
		} else {
			establishNewInstance ();
		}
		StartCoroutine(mazeInstance.Generate ());

		deleteButton.enabled = true;



	}

		//May eventually be renamed 'Next Session"
	public void restartSession(){

		if (sessionCheck) {
			mazeLength = mazeInstance.setPath;

			for (int i = 0; i < mazeInstance.markerValues.Count; i++) {
				markerValues.Add (mazeInstance.markerValues [i]);
			}
			sessionCheck = false;

		}

		StopAllCoroutines ();
		for (int i = 0; i < mazeInstance.allBlocks.Count; i++) {
			DestroyObject (mazeInstance.allBlocks [i].gameObject);
		}

		for (int i = 0; i < mazeInstance.trailBlocks.Count; i++) {
			DestroyObject (mazeInstance.trailBlocks [i].gameObject);
		}

		mazeInstance.stopAll ();
		DestroyObject (mazeInstance.robit.gameObject);


		print (markerValues.Count);

		Destroy (mazeInstance.gameObject);
		beginSession ();
		
	}
		
	void establishSession(){
		if (sessionCheck) {
			mazeInstance.sessionCheck = true;
		}
			
	}

	void establishNewInstance(){
		mazeInstance.setPath = mazeLength;
		mazeInstance.markerValues = markerValues;
		
	}


	public void smallMaze (){
		mazeSizeSetting = 40;
		mazeCorridorSetting = 6;
		a = "Small";
	}

	public void averageMaze (){
		mazeSizeSetting = 80;
		mazeCorridorSetting = 7;
		a = "Average";


	}

	public void largeMaze (){
		mazeSizeSetting = 120;
		mazeCorridorSetting = 9;
		a = "Large";


	}

	public void crazeMaze (){
		mazeSizeSetting = 300;
		mazeCorridorSetting = 10;
		a = "Ridiculous";


	}


	public void slowMazeChange (){
		mazeSpeedSetting = .5;
		b = "Slow";
	}

	public void averageSpeedMaze (){
		mazeSpeedSetting = .2;
		b = "Average";

	}

	public void fastMaze (){
		mazeSpeedSetting = .1;
		b = "Fast";

	}

	public void superFastMaze (){
		mazeSpeedSetting = 0;
		b = "Very Fast";

	}


	public void slowBotChange (){
		botSpeedSetting = 2;
		c = "Slow";
		
	}
	public void AverageBot (){
		botSpeedSetting = 1;
		c = "Average";

		
	}
	public void FastBot (){
		botSpeedSetting = .5;
		c = "Fast";

		
	}
	public void VeryFastBot (){
		botSpeedSetting = .2;
		c = "Very Fast";

		
	}

	public void setText(){
		
		double fraction = 0;
		double bb = 0;
		int bbMark2 = 0;
		int counterbb = 0;
		fraction = (netScript.rndMarkerAmnt / netScript.expectedPath) * 100;

		ar = "Path Length: " + netScript.expectedPath;

		br = "Bot Path Length: " + netScript.botOutputPath.ToString();

		for (int i = 0; i < netScript.markerWeights.Count; i++) {
			bb = bb + netScript.markerWeights [i];
			counterbb += 1;
		}

		bb = bb / counterbb;
		print (bb);

		bb = bb * 100;

		cr = "Bot Avg. Weight: ." + (int)bb;

		dr = "Bot Solve %: " + (int)fraction + "%";

		 

	}




}
