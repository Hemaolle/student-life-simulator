using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	
	public int Score;
	public GUIStyle ScoreStyle;
	public GameObject floatingNumber;
	StatusBars statusBars;
	
	// Use this for initialization
	void Start () {
		Score = 0;
		statusBars = GetComponent<StatusBars>();
	}
	
	public void raiseHeartbeat(float raise)
	{
		statusBars.heartbeat += raise;	
	}
	
	public void OnGUI()
	{
		GUI.Label(new Rect(680,520,100,20),"Score", ScoreStyle);
		GUI.Label(new Rect(680,545,100,20),Score.ToString(), ScoreStyle);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void CreateFloatingNumber(string text, Color color, float lifetime, Rect location) 
	{
		CreateFloatingNumber( text,  color,  lifetime,  location, 20);
	}
	
	public void CreateFloatingNumber(string text, Color color, float lifetime, Rect location, int fontSize) 
	{
		CreateFloatingNumber( text,  color,  lifetime,  location, fontSize, 1f);		
	}
	
	public void CreateFloatingNumber(string text, Color color, float lifetime, Rect location, int fontSize, float speed) 
	{
		GameObject fn = (GameObject)Instantiate(floatingNumber);
		FloatingNumbers fnScript = fn.GetComponent<FloatingNumbers>();
		fnScript.text = text;
		fnScript.style.normal.textColor = color;
		fnScript.style.fontSize = fontSize;
		fnScript.lifetime = lifetime;
		fnScript.location = location;
		fnScript.speed = speed;
	}
}
