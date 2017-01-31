using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelData {

	// Consider Data hiding where the final element index is stored so it doesn't need to use a for loop
	public string LevelName;
	public int mainstartindex;
	// 0 is empty, 1 is main character, 2 is enemy, 3 is block, 9 is goal
	public int[] leveldesign;
}
