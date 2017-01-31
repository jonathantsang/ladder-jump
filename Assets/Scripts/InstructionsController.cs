using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
		if (Input.GetKeyDown("1")) {
			SceneManager.LoadScene("Game");
		}
	}
}
