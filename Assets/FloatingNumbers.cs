using UnityEngine;
using System.Collections;

public class FloatingNumbers : MonoBehaviour {

	public string text;
	public GUIStyle style;
	public float lifetime;
	public Rect location;
	private float startTime;
	public float speed;
	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
	}
	
	public void OnGUI()
	{
		GUI.depth = -1;
		GUI.Label(location,text,style);	
	}
	
	// Update is called once per frame
	void Update () {
		float timeSinceStart = Time.timeSinceLevelLoad - startTime;
		if (timeSinceStart > lifetime)
			Destroy(gameObject);
		location = new Rect(location.x, location.y - speed, location.width, location.height);
	}
}
