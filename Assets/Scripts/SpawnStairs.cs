using UnityEngine;
using System.Collections;

public class SpawnStairs : MonoBehaviour {

	public int MaxPlatforms = 9;
	public GameObject platformh;
	public GameObject platformv;
	public Canvas canvas;

	private Vector2 originPositionh;
	private Vector2 originPositionv;


	// Use this for initialization
	void Start () {
		originPositionh = new Vector2(-7.3f,-5.5f);
		originPositionv = new Vector2(-6.7f,-5.0f);
		Spawn ();
	}
	
	// Update is called once per frame
	void Spawn () {
		for (int i = 0; i < MaxPlatforms; i++) 
		{
			Vector2 newPosition = originPositionh + new Vector2 (1.0f, 1.0f);
			GameObject newTile = Instantiate (platformh);
			newTile.transform.localPosition = newPosition;
			newTile.transform.SetParent(canvas.transform);
			originPositionh = newPosition;
		}
		for (int i = 0; i < MaxPlatforms-1; i++) 
		{
			Vector2 newPosition = originPositionv + new Vector2 (1.0f, 1.0f);
			GameObject newTile = Instantiate(platformv);
			newTile.transform.localPosition = newPosition;
			newTile.transform.SetParent(canvas.transform);
			originPositionv = newPosition;
		}
	
	}
}
