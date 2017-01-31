using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovementCharacter : MonoBehaviour {

	public GameObject character;
	public GameObject GameManagerObject;
	public GameObject CompleteLevelController;

	// canvas that is a gameobject
	private GameObject canvas;

	// Maybe put a findorigin to find where 1 is in the array, but requires overhaul of the arrays
	private int start = 0;
	// where the character wants to move to
	private int newpos;
	// current position of the character
	public int position;
	private bool valid;
	// Used for finding the length of the level
	private int levelnumber;
	private int levellength;

	// Find the level layout
	private GameObject LevelCounterConstant;


	private LevelManager LController;
	private GameManager GM;
	private CompleteLevel CLC;
	private LevelCountScript LCS;

	// Use this for initialization
	void Start () {
		// Find canvas, "gameobject" with "picasso"
		canvas = GameObject.FindWithTag ("picasso");
		// Restart at the beginning so 0
		position = start;
		newpos = start;
		// Get the length of the array from the GM
		GM = GameManagerObject.GetComponent<GameManager>();
		// Get the checkvalid function from the CompleteLevel Script
		CompleteLevelController = GameObject.FindWithTag ("mutate");
		CLC = CompleteLevelController.GetComponent<CompleteLevel> ();
		// Finding the level number to find the length of the level
		LevelCounterConstant = GameObject.FindWithTag ("constant");
		LCS = LevelCounterConstant.GetComponent<LevelCountScript> ();
		levelnumber = LCS.levelnumber;
		levellength = GM.LController.ldata [levelnumber].leveldesign.Length;
		Debug.Log (levellength + " is the MC levellength");

		// Instantiate the character
		startcharacter(GM.LController.ldata[levelnumber].mainstartindex);


		// To get the checkvalid function
		// valid = CLC.checkvalid (newpos);
	}
	
	// Update is called once per frame
	void Update () {
		moveplayer ();
		cheat ();
		restart ();
	}

	private void cheat(){
		if (Input.GetKeyDown ("j")) {
			loadLevelnext ();
		}
	}

	private void restart(){
		if (Input.GetKeyDown ("r")) {
			restartlevel ();
		}
	}

	void loadLevelnext(){
		LCS.nextlevel ();
		DontDestroyOnLoad(LevelCounterConstant);
		CLC.beginningstuff ();
		SceneManager.LoadScene("NewLevel");	
	}

	void restartlevel(){
		DontDestroyOnLoad(LevelCounterConstant);
		CLC.beginningstuff ();
		SceneManager.LoadScene("NewLevel");
	}

	void startcharacter(int start){
		character = (GameObject)Instantiate (character);
		character.transform.localPosition = new Vector3(start, start, 0);
		character.transform.SetParent(canvas.transform);
		position = start;
	}

	// Moves the character without the 
	public void safemovement(int amount, int position, GameObject character){
		if (character == null)
			Debug.Log ("NULL Character");
		Vector2 newPosition = (Vector2) (character.transform.position) +  new Vector2 (1, 1) * amount;
		Debug.Log(newPosition + " is the new vector2 pos");
		// Find the CLC obj
		CompleteLevelController = GameObject.FindGameObjectWithTag ("mutate");
		CLC = CompleteLevelController.GetComponent<CompleteLevel> ();
		// If the levellength is 9, it cannot be past 8
		int ll = CLC.levelm.Length - 1;
		// Check if the overflow of the array rotation occurs
		if ((newPosition.x >= ll) || (newPosition.y >= ll)) {
			newPosition = new Vector2 (0, 0);
			Debug.Log (newPosition + " is the new vector2 pos, because of overflow");
			character.transform.position = newPosition;
		// Check if the underflow of the array rotation occurs
		} else if ((newPosition.x < 0) || (newPosition.y < 0)) {
			newPosition = new Vector2 (1, 1) * levellength;
			Debug.Log (newPosition + " is the new vector2 pos, because of underflow");
			character.transform.position = newPosition;
		}
		character.transform.position = newPosition;
	}

	// The main movement function for the character controlled with 1,2,q,w
	void movement(int amount, int newpos){
		// Remove bombs if needed, remove doors if needed
		CLC.removebombfromjump (position, newpos);
		CLC.keycontact (position, newpos);
		Vector2 newPosition = (Vector2) character.transform.position + new Vector2 (1, 1) * amount;
		character.transform.position = newPosition;
		// Check that the current pos is a hat shift, then it needs to move another time
		if ((CLC.levelm[newpos] == 6) ||
			(CLC.levelm[newpos] == 7)) {
			if (CLC.hatlcontact (position, newpos) == true) {
				position -= 1;
			} else if (CLC.hatrcontact (position, newpos) == true) {
				position += 1;
			}
		}
		// movement takes place after
		position += amount;
		// Check the position to check for underflow or overflow
		if (position >= levellength) {
			position = 0;
		} else if (position < 0) {
			position = levellength;
		}
		Debug.Log("position is now " + position);
	}

	void moveplayer(){
		// Forwards
		if (Input.GetKeyDown ("1")) {
			newpos = position + 1;
			valid = CLC.checkvalid (newpos, CLC.levelm);
			CLC.printarray (CLC.levelm);
			Debug.Log (valid + " is the valid value");
			if ((position + 1 >= levellength) || (valid == false)) {
				Debug.Log ("Cannot move");
			} else {
				// this moves +1
				movement (1, newpos);
			}

		}
		if (Input.GetKeyDown ("2")) {
			newpos = position + 2;
			// Check if the new space has NO bomb, or obstacle
			valid = CLC.checkvalid (newpos, CLC.levelm);
			CLC.printarray (CLC.levelm);
			Debug.Log (valid + " is the valid value");
			if ((position + 2 >= levellength) || (valid == false)) {
				Debug.Log ("Cannot move");
			} else {
				// this moves +2
				movement (2, newpos);
			}
		}
		// Backwards
		if (Input.GetKeyDown ("q")) {
			newpos = position - 1;
			valid = CLC.checkvalid (newpos, CLC.levelm);
			if ((position -1 < 0) || (valid == false)) {
				Debug.Log ("Cannot move");
			} else {
				//this moves -1
				movement (-1, newpos);
			}
		}
		if (Input.GetKeyDown ("w")) {
			newpos = position - 2;
			valid = CLC.checkvalid (newpos, CLC.levelm);
			if ((position - 2 < 0) || (valid == false)) {
				Debug.Log ("Cannot move");
			} else {
				// this moves -2
				movement (-2,newpos);
			}
		}
		// Check if it is the end of the level after the movement
		if (CLC.checkendgoal(newpos) == true){
			loadLevelnext ();
		}
	}
}
