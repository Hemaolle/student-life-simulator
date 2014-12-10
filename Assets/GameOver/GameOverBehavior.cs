using UnityEngine;
using System.Collections;

public class GameOverBehavior : MonoBehaviour {
	
	
	private float shakeCounter;

	private Vector2 mousePosition;
	
	private int finalScore;
	
	
	// Use this for initialization
	void Start () {
		
		shakeCounter = 0.0f;
		
		finalScore = PlayerPrefs.GetInt ("Score");
		
	}
	
	// Update is called once per frame
	void Update () {
	
		mousePosition = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));		
		
		if (shakeCounter > 5.0f) {
		
			if (Input.GetKey (KeyCode.Return)) {
			
				Application.LoadLevel ("MenuScene");
				
			}
			
		}
				
	}
	
	public void OnGUI () 
	{	
				
		// render game over scene
		int bonusX = 0;
		int bonusY = 0;
		

		if (shakeCounter < 5.0f) {
			
			shakeCounter += Time.deltaTime;
			
			bonusX = Random.Range (1, 15) - 7;
			bonusY = Random.Range (1, 15) - 7;
			
		}

		
		GUIStyle buttonTextStyle = new GUIStyle("label");
		buttonTextStyle.alignment = TextAnchor.MiddleCenter;		
		buttonTextStyle.fontStyle = FontStyle.Bold;
		buttonTextStyle.fontSize = 80;
		
		GUI.color = Color.red;
		
				
		// elements

		int textX = 100;
		int textY = 200;
			
		GUI.Label (new Rect(textX + bonusX, textY + bonusY, 600, 120), "GAME OVER", buttonTextStyle);
			
		if (shakeCounter >= 5.0f) {
			
			textX = 200;
			textY = 380;
				
			buttonTextStyle.fontSize = 30;
			GUI.color = Color.white;
			
			GUI.Label (new Rect(textX + 0, textY + 0, 400, 80), "Final Score: " + finalScore, buttonTextStyle);			
				
			GUI.Label (new Rect(textX + 0, textY + 60, 400, 80), "(Press Enter to return to Main Menu)", buttonTextStyle);
			
		}
					
	}
	
	void OnMouseDown() {
	
		
	}
	
	void OnMouseUp() {
		
		
	}
	
}
