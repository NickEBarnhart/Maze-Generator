using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MathNet.Numerics.LinearAlgebra;

public class networkCalculation : MonoBehaviour {


	int goal; //expected output
	int mazeSize;
	double pathLength; //length of path used by bot
	List<int> markervalues = new List<int>(); // list of markers generated.
	bool createPath = true;

	//Comparison Values
	double specialWeight;
	public int expectedPath; // constant after first call
	public List<int> expectedMarkers = new List<int>(); //constant after first call

	//Bot variables
	public bool createWeights = true;
	public int botOutputPath;
	int markerCount;
	double errorMargin;
	double weight1, weight2, weight3; // weights for path length
	double weight4;
	List<int> botMarkerValues = new List<int>();
	public List<double> markerWeights = new List<double>(); // weights for marker path
	public int counter = 0;



	//special
	public List<Vector3> pathMarkerCoordinates = new List<Vector3>();
	public List<Vector3> allMarkerCoordinates = new List<Vector3>();
	public List<int> allMarkers = new List<int>();




	bool Old = true;
	bool decrementProgress = false;
	public bool firstSet = false;
	public bool firstSet2 = false;
	double oldWeight;
	public double rndMarkerAmnt;
	int oldMarkerValue;









	public int botPath(){

		double a,b;
		a = mazeSize;
		b = mazeSize * 3;



		if (createWeights) {

			weight1 = returnWeight ();
			weight2 = returnWeight ();
			weight3 = returnWeight ();

			a = a * weight1;
			b = b * weight2;


			pathLength = a + b;

			pathLength = pathLength * weight3;



			createWeights = false;

		} else {
			errorMargin = expectedPath - botOutputPath;

			if (errorMargin == 0) {
				a = a * weight1;
				b = b * weight2;

				pathLength = a + b;

				pathLength = pathLength * weight3;



			}
			else if (errorMargin < 0) {
				weight1 = weight1 * .9;
				weight2 = weight2 * .9;
				weight3 = weight3 * .9;

				a = a * weight1;
				b = b * weight2;

				pathLength = a + b;

				pathLength = pathLength * weight3;


				
			} else {
				
				weight1 = weight1 * 1.1;
				weight2 = weight2 * 1.1;
				weight3 = weight3 * 1.1;

				a = a * weight1;
				b = b * weight2;

				pathLength = a + b;

				pathLength = pathLength * weight3;


			}
				

		}
			

		botOutputPath = (int)pathLength;
		return botOutputPath;
		
	}

	public bool botCheckMove(Vector3 loc, int value){
		bool index = false;

		for (int i = 0; i < allMarkerCoordinates.Count; i++) {
			
			if (allMarkerCoordinates [i] == loc) {
				if (allMarkers [i] == value) {
					index = true;
					i = allMarkerCoordinates.Count;
				}

			} else {
				index = false;
			}
		}

		return index;
			
	}

	public void botDecrementProgres(){
		counter = counter - 1;
		if (decrementProgress) {
			botMarkerValues [counter] = oldMarkerValue;
			markerWeights [counter] = oldWeight;
		} else {
			botMarkerValues.Remove (botMarkerValues.Count - 1);
			markerWeights.Remove (markerWeights.Count - 1);

		}
			


	}

	public int incrementProgres(){
		int pathCnt = pathMarkerCoordinates.Count;
		print ("path marker co" + pathCnt);
		int createWeight;

		if (firstSet) {
			rndMarkerAmnt = Random.Range (1, pathCnt / 5);
			firstSet = false;
		}

		if (rndMarkerAmnt <= pathCnt) {
			rndMarkerAmnt = rndMarkerAmnt * 1.3;
		} else {

			rndMarkerAmnt = pathCnt;

		}

		createWeight = (int)rndMarkerAmnt;
		return createWeight;


	}

	public int botMarker(){
		
		double a;
		double weightMark;
		int path;

			
		a = mazeSize * specialWeight;


		if ((counter < botOutputPath) && (firstSet2)) {
			decrementProgress = true;
			if (counter > expectedMarkers.Count) {
				
				oldWeight = markerWeights [counter];
				markerWeights [counter] = markerWeights [counter] * 1;
				a = a * markerWeights [counter];

				
			} else if (counter >= expectedMarkers.Count) {
				
				if (botMarkerValues [counter] == expectedMarkers [counter]) {
					oldWeight = markerWeights [counter];
					markerWeights [counter] = markerWeights [counter] * 1;
					a = a * markerWeights [counter];

				} else if (botMarkerValues [counter] > expectedMarkers [counter]) {
					oldWeight = markerWeights [counter];
					markerWeights [counter] = markerWeights [counter] * .9;
					a = a * markerWeights [counter];

					
				} else if (botMarkerValues [counter] < expectedMarkers [counter]) {
					oldWeight = markerWeights [counter];
					markerWeights [counter] = markerWeights [counter] * 1.1;
					a = a * markerWeights [counter];


				}

			}
		
		} else {
			decrementProgress = false;
			weightMark = returnWeight ();
			markerWeights.Add (weightMark);
			a = a * weightMark;
			
		}



		//activation
		while (a >= 5) {
			a = a / 2; // returns marker value
		}



		path = (int)a; //convert value to integer

		if ((counter < botOutputPath) && (firstSet2) ) {
			print (counter);
			print (botOutputPath);
			print (botMarkerValues.Count);
			oldMarkerValue = botMarkerValues [counter];
			botMarkerValues [counter] = path;
			counter = counter + 1;



		} else {
			botMarkerValues.Add (path);
		}
				


		return path;

		
	}
	

	public int calculatePath(double a, double b, int c){ //20 , 80
		int path;
		mazeSize = c;


		a = a * returnWeight ();
		b = b * returnWeight ();

		pathLength = a + b;

		pathLength = pathLength * returnWeight ();


		path = (int)pathLength;

		if (createPath) {

			expectedPath = path;
			createWeights = true;
			createPath = false;
		}

		return path;
	}

	public int calculateMarkers(){
		double a;
		int path;

		if (Old) {
			specialWeight = returnWeight ();
			Old = false;
		}

		a = mazeSize * specialWeight;



		a = a * returnWeight ();

		while (a >= 5) {
			a = a / 2;
		}
			


		path = (int)a;

		if (markervalues.Count < expectedPath) {
			markervalues.Add (path);
		}
		

		return path;


	}

	public double returnWeight()
	{
		float a = 0;
		float b = 1;
		double weight = Random.Range (a, b);

		return weight + 0.1;
	}


}
