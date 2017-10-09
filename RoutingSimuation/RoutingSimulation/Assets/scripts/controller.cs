using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	public GameObject taxiPrefab; // pointer to instance of taxi
	public GameObject riderPrefab; // pointer ot instance of person
	public GameObject ground;
	public Button resetButton;
	public int taxiCount=1;

	List<GameObject> carlist; // list of all the cars in the simulation.
	List<GameObject> lineList; // list of all lines.
	List<float> distanceList; // list of distance from the rider
	GameObject rider; // rider instance
	int selectedCarIndex =-1;
	public static float bound = 0.0f;

	void Start () {
		bound = ground.GetComponent<MeshFilter>().mesh.bounds.size.x -0.5f;
		carlist = new List<GameObject> ();
		lineList = new List<GameObject> ();
		distanceList = new List<float> ();
		resetButton.onClick.AddListener (onClickResetSimulation);
		CreateSimulation ();
	}

	void Update(){
		computeDistance ();

	}

	GameObject instantiateTaxi(){
		GameObject car = Instantiate (taxiPrefab, new Vector3(Random.Range(-bound, bound),0,Random.Range(-bound, bound)),Quaternion.Euler(-90,Random.Range(-180, 180.0f),0));
		return car;
	}

	GameObject DrawLine(Vector3 start, Vector3 end, Color color)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.startColor = color;
		lr.endColor = color;
		lr.startWidth = 0.1f;
		lr.endWidth = 0.1f;
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		return myLine;
	}

	void CreateSimulation(){
		Destroy (rider);
		//destroy cars
		foreach(GameObject car in carlist)
			Destroy(car);
		carlist.Clear();

		// create random cars on the scene
		for (int i = 0; i < taxiCount; i++) {
			carlist.Add(instantiateTaxi ()); // add new car in the linked list.
		}
		rider = Instantiate (riderPrefab, new Vector3 (Random.Range (-bound, bound), -0.2f, Random.Range (-bound, bound)), Quaternion.Euler (0, 0, 0)); // create the user.
		computeDistance ();
	}

	void onClickResetSimulation(){

		CreateSimulation ();
	}

	void computeDistance(){
		foreach (GameObject lr in lineList)
			Destroy (lr);
		lineList.Clear ();
		distanceList.Clear ();

		// compute distance between rider and all cars
		foreach (GameObject car in carlist) {
			distanceList.Add (Vector3.Distance (rider.transform.position, car.transform.position));
		}

		// get the shortest distance index
		selectedCarIndex = distanceList.IndexOf(Mathf.Min(distanceList.ToArray()));
		
		for (int i=0; i<carlist.Count;i++){
			if(i == selectedCarIndex)
				lineList.Add( DrawLine(rider.transform.position, carlist[i].transform.position, Color.green));
			else
				lineList.Add( DrawLine(rider.transform.position, carlist[i].transform.position, Color.red));
		}
	}
		
}
