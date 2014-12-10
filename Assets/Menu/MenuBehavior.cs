using UnityEngine;
using System.Collections;

public class MenuBehavior : MonoBehaviour {
	
	
	private float menuSize;
	private float menuAddition;
	private float elementCounter;
	private float fadeInCounter;

	private Vector2 mousePosition;
	
	private Rect textRect;
	
	private string gameTitle;
	
	private string startGameText;
	private string tutorialText;
	
	private Rect startGameRect;
	private Rect tutorialRect;
	
	private bool startGamePressed;
	private bool tutorialPressed;
	
	private string creditsTitle;
	
	
	// Use this for initialization
	void Start () {
		
		menuSize = -300.0f;
		menuAddition = 0.1f;
		elementCounter = 0.0f;
		fadeInCounter = 0;
		
		textRect = new Rect(10, 50, 780, 160);
		
		gameTitle = "Student life simulator";
		
		startGameRect = new Rect(300, 280, 200, 40);
		tutorialRect =  new Rect(300, 360, 200, 40);
		
		startGameText = "Start New Game";
		tutorialText = "Tutorial";
		
	}
	
	// Update is called once per frame
	void Update () {
	
		mousePosition = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));		
		
	}
	
	public void OnGUI () 
	{	
				
		if (menuSize < 40.0f) {
		
			menuAddition += Time.deltaTime * 0.2f;
			menuSize += menuAddition;
			
			if (menuSize > 40.0f) {
				menuSize = 40.0f;	
			}
			
		}
		
		if (menuSize > -200.0f) {
			
			GUIStyle centeredTextStyle = new GUIStyle("label");
    		centeredTextStyle.alignment = TextAnchor.MiddleCenter;	
			
			if (menuSize > 1.5f) {
				centeredTextStyle.fontSize = (int)(menuSize * 1.75f);
			}
			else {
				centeredTextStyle.fontSize = 3;
			}
			
			centeredTextStyle.fontStyle = FontStyle.Bold;
				
			GUI.color = Color.black;
			GUI.Label (textRect, gameTitle, centeredTextStyle);
			
		}
		
		// render menu
		
		if (menuSize >= 40.0f) {
			
			if (elementCounter < 2.5f) {
				
				elementCounter += Time.deltaTime;
				
			}
			else {
				
				if (fadeInCounter < 1.5f) {
					fadeInCounter += Time.deltaTime;
				}
				
				// opacity and styles
				float fAlpha = fadeInCounter;
				
				if (fAlpha > 1.5f)
				{
					fAlpha = 1.5f;	
				}
				
				fAlpha /= 1.5f;
				
				
				GUIStyle buttonTextStyle = new GUIStyle("button");
    			buttonTextStyle.alignment = TextAnchor.MiddleCenter;		
				buttonTextStyle.fontStyle = FontStyle.Bold;
				buttonTextStyle.fontSize = 10;
				
				GUI.color = Color.black;				
				
				Color colPreviousGUIColor = GUI.color;
	 
	       		GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);				
									
				
				// elements
				
				startGamePressed = GUI.Button (startGameRect, startGameText);
				
				tutorialPressed = GUI.Button (tutorialRect, tutorialText);
				
				int creditsX = 650;
				int creditsY = 400;
				
				GUI.Label (new Rect(creditsX + 0, creditsY + 0, 180, 20), "Sounds & Music: ");
				GUI.Label (new Rect(creditsX + 0, creditsY + 20, 180, 20), " Sami Singh");					
				GUI.Label (new Rect(creditsX + 0, creditsY + 50, 180, 20), "Graphics: ");
				GUI.Label (new Rect(creditsX + 0, creditsY + 70, 180, 20), " Maikki Leppaaho");					
				GUI.Label (new Rect(creditsX + 0, creditsY + 100, 180, 20), "Programming: ");				
				GUI.Label (new Rect(creditsX + 0, creditsY + 120, 180, 20), " Janne Kohvakka");	
				GUI.Label (new Rect(creditsX + 0, creditsY + 140, 180, 20), " Oskari Leppaaho");
				GUI.Label (new Rect(creditsX + 0, creditsY + 170, 180, 20), "Global Game Jam 2013");	
				
				// key check
				
				if (startGamePressed) {
					
					Application.LoadLevel("Main");
					
				}
				else if (tutorialPressed) {
					
					Application.LoadLevel ("TutorialScene");	
					
				}
				
				// restore normal color
       			GUI.color = colPreviousGUIColor;
				
				
			}
			
		}
		
	}
	

	
	void OnMouseDown() {
	
		
	}
	
	void OnMouseUp() {
		
		
	}
	
}
