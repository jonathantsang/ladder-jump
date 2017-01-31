using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour {

	public GameObject GameManagerObject;
	public GameObject MovementCharacterObject;
	// levelnumber is now private and must be accessed from LevelCountScript
	private GameObject LevelCounterConstant;
	private LevelCountScript LCS;

	private GameManager GM;
	private LevelManager LM;
	private MovementCharacter MC;
	private int levelnumber;
	private int levellength;
	// Used in destroying the corresponding objects when jumping or reaching a key respectively
	private GameObject bomb;
	private GameObject door;
	private GameObject key;

	// Used in moving the character when the array is rotated
	private GameObject character;

	// Used in deleting the items to move them
	private GameObject toDelete;
	// Reinitializing the items in the Canvas
	private GameObject canvas;

	// Try traditional
	// Mutatatable level design to keep track of the level bombs, obstacles, etc.
	// if levelmutate doesn't work, i.e. keeps reverting to empty array, just hard-use the getcomponent array?
	public int[] levelm;

	// End of Level
	private int final;

	// NOT called when new scene is opened. Only when the scene is first called.
	void Start(){
		// When the scene is loaded, call the function
		beginningstuff();
	}

	public void beginningstuff(){
		Debug.Log("beginningstuff executed");
		// Load the GameManager Script Object
		GM = GameManagerObject.GetComponent<GameManager>();
		MC = MovementCharacterObject.GetComponent<MovementCharacter> ();
		FindLevelNumber ();
		// levelmutate = GM.LController.ldata[levelnumber].leveldesign;
		// Pseudo copy array
		levellength = GM.LController.ldata[levelnumber].leveldesign.Length;
		// Allocate enough space for copying the array
		levelm = new int[levellength];
		Array.Copy(GM.LController.ldata [levelnumber].leveldesign, levelm, levellength);
		printarray (levelm);
		levellength = levelm.Length;
		// Find end goal
		final = findendgoal();
		// Find canvas, "gameobject" with "picasso"
		canvas = GameObject.FindWithTag ("picasso");
		// Find main character "gameobject" with "Character"
		character = GameObject.FindWithTag ("main");
	}

	public bool checkvalid(int newpos, int[] levellayout){
		int ll = levellayout.Length;
		// If the position is outside of the array it cannot move there
		if ((newpos >= ll) || (newpos < 0)) {
			Debug.Log (ll + " is the levellength");
			Debug.Log (newpos + " is the newpos");
			Debug.Log ("outside");
			printarray (levellayout);
			return false;
		}
		// Check if the space is either blank or the goal, then you can move there
		// As well, it can be a key, hatl, or hatr
		if ((levellayout [newpos] == 0) || (levellayout [newpos] == 5) || (levellayout [newpos] == 9) ||
			(levellayout[newpos] == 6) || (levellayout [newpos] == 7)) {
			Debug.Log ("zero or nine");
			printarray (levellayout);
			return true;
		// Otherwise, you cannot move there
		} else {
			Debug.Log ("no");
			return false;
		}
	}

	public void removebombfromjump (int originpos, int newpos){
		// Start checking from one past the original position
		int counter = originpos + 1;
		while (counter < newpos) {
			if (levelm [counter] == 2) {
				levelm [counter] = 0;
				string tagrem = "e" + counter;
				bomb = GameObject.FindWithTag (tagrem);
				Destroy (bomb);
			}
			++counter;
		}
		counter = originpos - 1;
		if (newpos < originpos) {
			Debug.Log("reached less than");
			while (counter > newpos) {
				if (levelm [counter] == 2) {
					levelm [counter] = 0;
					string tagrem = "e" + counter;
					bomb = GameObject.FindWithTag (tagrem);
					Destroy (bomb);
				}
				--counter;
			}
		}
	}

	public void keycontact(int originpos, int newpos){
		// 5 is used because that is the int for the key object
		if (levelm [newpos] == 5) {
			for (int i = 0; i < levelm.Length; ++i) {
				// 4 is used because that is the int for the door object
				if (levelm [i] == 4) {
					string tagrem = "e" + i;
					door = GameObject.FindWithTag (tagrem);
					Destroy (door);
					levelm [i] = 0;
				// Also delete the key when it is reached.
				} else if (levelm [i] == 5) {
					string tagrem = "e" + i;
					key = GameObject.FindWithTag (tagrem);
					Destroy (key);
					levelm [i] = 0;
				}
			}
		}
	}

	public bool hatlcontact(int originpos, int newpos){
		// 6 is used because that is the hatl
		// newpos means the character goes to that position
		if(levelm[newpos] == 6){
			Debug.Log (6 + " is reached");
			// Delete the old objects
			for (int i = 0; i < levelm.Length; ++i) {
				string tagtofind = "e" + i;
				toDelete = GameObject.FindWithTag (tagtofind);
				Destroy (toDelete);
			}
			// Shift the array to the left
			shiftl (levelm);
			printarray (levelm);
			// Make sure the user is moved to the left one as well.
			int pos = MC.position;
			// Find main character "gameobject" with "Character"
			character = GameObject.FindWithTag ("main");
			MC.safemovement(-1, pos, character);
			--MC.position;
			Debug.Log (MC.position + "according to CL");
			// Load the new level
			GM.LoadLevelbyArray (levelm, canvas);
			return true;
		}
		return false;
	}

	// Checks if the hatr is in contact with the character after the movement to the newpos
	public bool hatrcontact(int originpos, int newpos){
		// 7 is used because that is the hatr
		// newpos means the character goes to that position
		if(levelm[newpos] == 7){
			Debug.Log (7 + " reached");
			// Delete the old objects
			for (int i = 0; i < levelm.Length; ++i) {
				string tagtofind = "e" + i;
				toDelete = GameObject.FindWithTag (tagtofind);
				Destroy (toDelete);
			}
			// Shift the array to the right
			shiftr (levelm);
			printarray (levelm);
			// Make sure the user is moved to the left one as well.
			int pos = MC.position;
			// Find main character "gameobject" with "Character"
			character = GameObject.FindWithTag ("main");
			// Make sure the user is moved to the right one as well.
			MC.safemovement(1, pos, character);
			Debug.Log (MC.position + " according to CL");
			// Load the new level
			GM.LoadLevelbyArray (levelm, canvas);
			return true;
		}
		return false;
	}

	private void shiftr(int[] arr){
		int finalvalue = arr [arr.Length - 1];
		// Start at the last element in the array
		for (int i = arr.Length - 1; i > 0; --i) {
			arr [i] = arr[i - 1];
		}
		arr [0] = finalvalue;
	}

	private void shiftl(int[] arr){
		int finalvalue = arr [0];
		// Start at the beginning of the array
		for(int i = 0; i + 1 < arr.Length; ++i){
			arr [i] = arr [i + 1];
		}
		arr [arr.Length - 1] = finalvalue;
	}

	public bool checklevelcomplete (){
		for(int i = 0; i < levellength; ++i){
			// Value is what the level element is at the index i
			int value = levelm [i];
			// No bombs (and doors?, tentative) are allowed, everything else is fine
			if((value == 2) || (value == 4)){
				return false;
			}
		}
		return true;
	}

	public bool checkendgoal(int currentpos){
		findendgoal ();
		if((final == currentpos) && (checklevelcomplete() == true)){
			return true;
		} else {
			return false;
		}
	}

	private int findendgoal(){
		final = -1;
		for (int i = 0; i < levelm.Length; ++i) {
			// Check which array index has the end goal
			if (levelm [i] == 9) {
				final = i;
			}
		}
		return final;
	}

	public void printarray(int[] arr){
		int len = arr.Length;
		string text = "[";
		for(int i = 0; i < len; ++i){
			text = text + arr[i] + ",";
		}
		text = text + "]";
		print (text);
	}

	private void FindLevelNumber(){
		// Load the LevelCounter Object to find the levelnumber that is constant
		LevelCounterConstant = GameObject.FindWithTag ("constant");
		LCS = LevelCounterConstant.GetComponent<LevelCountScript>();
		levelnumber = LCS.levelnumber;
	}
}
