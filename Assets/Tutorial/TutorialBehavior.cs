using UnityEngine;
using System.Collections;

public class TutorialBehavior : MonoBehaviour {
	
	
	private float fadeInCounter;

	private Vector2 mousePosition;
	
	private int tutorialPage;
	
	
	// Use this for initialization
	void Start () {
		
		fadeInCounter = 0;
		
		tutorialPage = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		mousePosition = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));		
		
		if (fadeInCounter >= 1.5f) {
		
			if (Input.GetKey (KeyCode.Return)) {
			
				fadeInCounter = 0.0f;
				
				tutorialPage++;
		
				if (tutorialPage > 2) {
				
					Application.LoadLevel ("MenuScene");
					
				}
				
			}
			
		}
				
	}
	
	public void OnGUI () 
	{	
				
		// render tutorial

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
				
		
		GUIStyle buttonTextStyle = new GUIStyle("label2");		
		buttonTextStyle.fontStyle = FontStyle.Bold;
		buttonTextStyle.fontSize = 16;
		
		GUI.color = Color.black;				
		
		Color colPreviousGUIColor = GUI.color;

   		GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);				
							
				
		// elements
		
		if (tutorialPage == 1) {
			
			int helpTextX = 200;
			int helpTextY = 200;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 250, 20), "The main goal of the game is to manage different", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 250, 20), "tasks in your apartment. The screen is divided to", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 250, 20), "a six different locations, like kitchen stove and sink,", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 250, 20), "door, window, student fund application, and", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 250, 20), "your beer reserve.", buttonTextStyle);				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 250, 20), "In each of these locations you can perform one or more ", buttonTextStyle);	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 120, 250, 20), "actions to earn points and keep your vital meters running.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 140, 250, 20), "If you fail to manage all the tasks your heartrate will rise.", buttonTextStyle);	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 160, 250, 20), "If your heartrate reaches 200 you die and the game is over.", buttonTextStyle);		
						
			
			
		}
		else if (tutorialPage == 2) {
			
			int helpTextX = 30;
			int helpTextY = 30;

			buttonTextStyle.fontSize = 10;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 250, 20), "The Kitchen Stove allows you to cook meals", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 250, 20), "and reduce your Hunger-meter, located in", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 250, 20), "the bottom of the screen. You dont have to", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 250, 20), "cook all the time, but you can start cooking", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 250, 20), "from Cook-button and stop it from Stop-", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 250, 20), "button.", buttonTextStyle);				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 130, 250, 20), "Remember, if you burn out your meal, you", buttonTextStyle);	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 150, 250, 20), "have to start over with nothing. ", buttonTextStyle);					
			
			
			helpTextX = 290;
			helpTextY = 30;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 250, 20), "Someone might come to visit you. If they do,", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 250, 20), "you will hear the doorbell ringing. You have", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 250, 20), "to react and press Spacebar to open the door,", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 250, 20), "but within 3 seconds from the ringing.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 90, 250, 20), "Remember, you have no idea who's coming to visit,", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 110, 250, 20), "so think carefully, because you don't want to", buttonTextStyle);				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 130, 250, 20), "let in any unwelcomed guests, now do you?", buttonTextStyle);				
			
			helpTextX = 560;
			helpTextY = 30;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 250, 20), "The Student Fund Application Form has some", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 250, 20), "nasty questions and requires some effort.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 250, 20), "Still you have to keep filling it, because", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 250, 20), "otherwise you won't get money.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 90, 250, 20), "Whenver you are not doing anything else, you", buttonTextStyle);		
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 110, 250, 20), "should click the Checkboxes in the form or ", buttonTextStyle);				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 130, 250, 20), "write any text into the textfields to earn", buttonTextStyle);	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 150, 250, 20), "more money.", buttonTextStyle);				
			
			helpTextX = 30;
			helpTextY = 330;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 250, 20), "This is your Window. Outside you can see trash", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 250, 20), "cans and sometimes perhaps someone messing", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 250, 20), "around. You really dont like people yelling", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 250, 20), "outside your apartment, so you can try throwing", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 250, 20), "a can or a transbag to chase them away.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 110, 250, 20), "To throw a object, use mouse to drag and", buttonTextStyle);				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 130, 250, 20), "drop either a beer can or Kitchen Sink's", buttonTextStyle);	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 150, 250, 20), "stench cloud to the window.", buttonTextStyle);				
	
			helpTextX = 290;
			helpTextY = 330;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 250, 20), "Here is your Beer Reserve. You need to monitor", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 250, 20), "your Intoxication-level at the bottom of the screen", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 250, 20), "and if it gets too low, you can have another beer.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 70, 250, 20), "Just click the bottles and you'll see a beer bottle", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 90, 250, 20), "and (your) head appear. Use Arrow-keys to move the", buttonTextStyle);				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 110, 250, 20), "head right under where the bottle is moving.", buttonTextStyle);	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 140, 250, 20), "Your buddies might sometimes bring you more beer.", buttonTextStyle);
			
			helpTextX = 560;
			helpTextY = 330;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 250, 20), "Your Kitchen Table and dishes. When you get", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 250, 20), "loads of dishes, you have to wash them. You'll", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 250, 20), "see three keys you can press to do the washing.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 250, 20), "You might have to do this several times to get", buttonTextStyle);					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 250, 20), "all cleaned up.", buttonTextStyle);
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 110, 250, 20), "When your garbages start to stink, you need", buttonTextStyle);				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 130, 250, 20), "to thrown them out by dragging the stench cloud", buttonTextStyle);	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 150, 250, 20), "to your Window. Aim at the trash cans.", buttonTextStyle);		
			
		}
		else if (tutorialPage == 3) {
			
			int helpTextX = 30;
			int helpTextY = 30;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 180, 20), " a");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 180, 20), " ");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 180, 20), "");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 180, 20), "");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 180, 20), " ");				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 180, 20), " ");	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 120, 180, 20), " ");					
			
			
			helpTextX = 300;
			helpTextY = 30;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 180, 20), " a");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 180, 20), " ");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 180, 20), "");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 180, 20), "");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 180, 20), " ");				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 180, 20), " ");	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 120, 180, 20), " ");				
			
			helpTextX = 600;
			helpTextY = 30;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 180, 20), " a");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 180, 20), " ");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 180, 20), "");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 180, 20), "");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 180, 20), " ");				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 180, 20), " ");	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 120, 180, 20), " ");				
			
			helpTextX = 30;
			helpTextY = 330;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 180, 20), " a");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 180, 20), " ");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 180, 20), "");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 180, 20), "");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 180, 20), " ");				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 180, 20), " ");	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 120, 180, 20), " ");				
	
			helpTextX = 300;
			helpTextY = 330;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 180, 20), " a");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 180, 20), " ");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 180, 20), "");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 180, 20), "");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 180, 20), " ");				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 180, 20), " ");	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 120, 180, 20), " ");
			
			helpTextX = 600;
			helpTextY = 330;
			
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 0, 180, 20), " a");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 20, 180, 20), " ");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 40, 180, 20), "");
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 60, 180, 20), "");					
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 80, 180, 20), " ");				
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 100, 180, 20), " ");	
			GUI.Label (new Rect(helpTextX + 0, helpTextY + 120, 180, 20), " ");				
			
			
		}
				
		// restore normal color
		GUI.color = colPreviousGUIColor;
				
		
	}
	

	
	void OnMouseDown() {
	
		
	}
	
	void OnMouseUp() {
		
		
	}
	
}
