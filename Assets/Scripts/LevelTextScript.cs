using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTextScript : MonoBehaviour {

	public GameObject GameManager;

	public Text leveltext;

	private GameManager GM;
	private LevelManager LM;

	// Level Number
	private LevelCountScript LCS;
	private GameObject LevelCounterConstant;

	void Start(){
		loadleveltext ();
	}

	void loadleveltext(){
		GM = GameManager.GetComponent<GameManager>();
		LevelCounterConstant = GameObject.FindWithTag ("constant");
		LCS = LevelCounterConstant.GetComponent<LevelCountScript>();
		int levelnumber = LCS.levelnumber;
		string levelname = GM.LController.ldata [levelnumber].LevelName;
		Debug.Log(levelnumber + " is the textlevel");
		Debug.Log(levelname + " is the textlevelname");
		leveltext.text  = "Level: " + levelnumber + " " + levelname;
	}
}
