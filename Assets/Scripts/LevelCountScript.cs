using UnityEngine;
using System.Collections;

public class LevelCountScript : MonoBehaviour {

	public int levelnumber = 1;

	// Use this for initialization
	void Start () {
		levelnumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextlevel(){
		levelnumber += 1;
		Debug.Log ("New Level");
	}
}
