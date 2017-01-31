using UnityEngine;
using System.Collections;

// Really the initial load spawn manager for the level
public class GameManager : MonoBehaviour {

	public LevelManager LController;
	// Objects that can be instantiated
	public GameObject main; // 1
	public GameObject enemy; // 2
	public GameObject obstacle; // 3
	public GameObject door; // 4
	public GameObject key; // 5
	public GameObject hatl; // 6
	public GameObject hatr; // 7
	public GameObject goal; // 9

	// Found with tag "picasso", which is really a canvas not a gameobject
	private GameObject canvas;

	// Mutable object that is looped in the for loop
	private GameObject newobject;

	// Origin, and the new position where it will be placed
	private Vector2 origin;
	private Vector2 newPosition;

	// For hidden level number use;
	private int levelnumber;
	private GameObject LevelCounterConstant;
	private LevelCountScript LCS;


	// Use this for initialization
	void Start () {
		LevelCounterConstant = GameObject.FindWithTag ("constant");
		// Find canvas, "gameobject" with "picasso"
		canvas = GameObject.FindWithTag ("picasso");
		LCS = LevelCounterConstant.GetComponent<LevelCountScript> ();
		levelnumber = LCS.levelnumber;
		LoadLevel (levelnumber);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void LoadLevel(int levelnumber){
		origin = new Vector2 (0, 0);
		Debug.Log ("levelnumber according to the GM is " + levelnumber);
		Debug.Log (LController.ldata [levelnumber].leveldesign.Length + " is the level length");
		// Get the level from the serialized level manager array that has the level name and the level design
		for (int i = 0; i < LController.ldata[levelnumber].leveldesign.Length; ++i) {
			// For loop goes through and places each of the object corresponding to 0,1,2,3...9
			int objectsplaced = LController.ldata [levelnumber].leveldesign [i];
			// Update the position of the new item
			newPosition = origin + new Vector2(1, 1) * i;
			// 0 means blank space
			if (objectsplaced == 0) {
			// 1 means the main character
			} else if (objectsplaced == 1) {
				GameObject newobject = Instantiate (main) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			// 2 means the enemy
			} else if (objectsplaced == 2) {
				GameObject newobject = Instantiate (enemy) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			// 3 means the obstacle
			} else if (objectsplaced == 3) {
				GameObject newobject = Instantiate (obstacle) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			// 9 means the goal
			} else if (objectsplaced == 4) {
				GameObject newobject = Instantiate (door) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 5) {
				GameObject newobject = Instantiate (key) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 6) {
				GameObject newobject = Instantiate (hatl) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 7) {
				GameObject newobject = Instantiate (hatr) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 9) {
				GameObject newobject = Instantiate (goal) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			}
		}
	}

	public void LoadLevelbyArray(int[] lvl, GameObject canvas){
		origin = new Vector2 (0, 0);
		Debug.Log (lvl.Length + " is the level length");
		// Get the level from the inputted array, which is different
		for (int i = 0; i < lvl.Length; ++i) {
			// For loop goes through and places each of the object corresponding to 0,1,2,3...9
			int objectsplaced = lvl [i];
			// Update the position of the new item
			newPosition = origin + new Vector2(1, 1) * i;
			// 0 means blank space
			if (objectsplaced == 0) {
				// 1 means the main character
			} else if (objectsplaced == 1) {
				GameObject newobject = Instantiate (main) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
				// 2 means the enemy
			} else if (objectsplaced == 2) {
				GameObject newobject = Instantiate (enemy) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
				// 3 means the obstacle
			} else if (objectsplaced == 3) {
				GameObject newobject = Instantiate (obstacle) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
				// 9 means the goal
			} else if (objectsplaced == 4) {
				GameObject newobject = Instantiate (door) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 5) {
				GameObject newobject = Instantiate (key) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 6) {
				GameObject newobject = Instantiate (hatl) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 7) {
				GameObject newobject = Instantiate (hatr) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			} else if (objectsplaced == 9) {
				GameObject newobject = Instantiate (goal) as GameObject;
				newobject.tag =  "e" + i;
				newobject.transform.localPosition = newPosition;
				newobject.transform.SetParent(canvas.transform);
			}
		}
	}
}
