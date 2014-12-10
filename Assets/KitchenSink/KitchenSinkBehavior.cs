using UnityEngine;
using System.Collections;

public class KitchenSinkBehavior : MonoBehaviour {
	
	private int dishesCounter;
	private int garbageCounter;
	
	private int reactModeDishes;
	private int reachModeGarbage;
	
	
	
	private int kitchenSinkPosX = 540;
	private int kitchenSinkPosY = 250;
	
	private int kitchenSinkWidth = 260;
	private int kitchenSinkHeight = 250;
	
	private Rect textFieldRect;
	private Rect boolToggleRect;
	private Rect toxicCloudRect;
	private Rect keyRect;
	
	private float reactTick;
	private float keyCounter;
	
	public Texture2D toxicCloud;
	
	public Texture2D sink1;
	public Texture2D sink2;
	public Texture2D sink3;
	public Texture2D sink4;
	
	private Vector2 mousePosition;
	
	public WindowBehavior windowScript;
	
	private ScoreScript scoreScript;
	
	private char key1 = 'a';
	private char key2 = 'b';
	private char key3 = 'c';
	
	
	// Use this for initialization
	void Start () {
	
		toxicCloudRect = new Rect(10, 10, 10, 10);
		
		keyRect = new Rect(10, 10, 200, 20);
		
		dishesCounter = 50;
		//dishesCounter = 110;
		garbageCounter = 60;
		
		reactModeDishes = 0;
		reachModeGarbage = 0;
		reactTick = 0;
		
		textFieldRect.width = 100;
		textFieldRect.height = 20;
		
		boolToggleRect.width = 100;
		boolToggleRect.height = 20;
		
		switchRandomKey(1);
		switchRandomKey(2);
		switchRandomKey(3);
		
		keyCounter = 0.0f;
		
		keyRect.x = kitchenSinkPosX + 15;
		keyRect.y = kitchenSinkPosY + 15;
		
		GameObject go = GameObject.Find("GUIGameObject");		
		scoreScript = go.GetComponent<ScoreScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
		mousePosition = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));		
		
		// dishwashing event
		
		if (key1 != ' ' && key2 != ' ' && key3 != ' ') {
			
			if (Input.GetKeyUp("" + key1)) {
				washDishes();
			}
			else if (Input.GetKeyUp("" + key2)) {
				washDishes();
			}
			else if (Input.GetKeyUp("" + key3)) {
				washDishes();
			}
			
		}
		
		if (Input.GetKeyDown(KeyCode.F12)) {
		
			PlayerPrefs.SetInt("Score", 198);
			
			Application.LoadLevel ("GameOverScene");
			
		}
		
	}
	
	public void OnGUI () 
	{	
				
		// render dishes
		if (dishesCounter > 100) {
		
			GUI.DrawTexture(new Rect(kitchenSinkPosX, kitchenSinkPosY, kitchenSinkWidth, kitchenSinkHeight), sink4);
			
		}
		else if (dishesCounter > 66) {
		
			GUI.DrawTexture(new Rect(kitchenSinkPosX, kitchenSinkPosY, kitchenSinkWidth, kitchenSinkHeight), sink3);
			
		}
		else if (dishesCounter > 33) {
		
			GUI.DrawTexture(new Rect(kitchenSinkPosX, kitchenSinkPosY, kitchenSinkWidth, kitchenSinkHeight), sink2);
			
		}
		else if (dishesCounter >= 0) {
		
			GUI.DrawTexture(new Rect(kitchenSinkPosX, kitchenSinkPosY, kitchenSinkWidth, kitchenSinkHeight), sink1);
			
		}		
		
		// render toxic cloud
		if (garbageCounter > 100) {
			
			float fAlpha = (float)(garbageCounter - 75);
			
			if (fAlpha > 50.0f)
			{
				fAlpha = 50.0f;	
			}
			
			fAlpha /= 50.0f;
			
			toxicCloudRect = new Rect(kitchenSinkPosX + 75, kitchenSinkPosY + 150, 128, 128);
			
			Color colPreviousGUIColor = GUI.color;
 
       		GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
			
			GUI.DrawTexture(toxicCloudRect, toxicCloud);

       		GUI.color = colPreviousGUIColor;					
			
		}		
		
		// render keys
		if (dishesCounter > 33) {
		
			string keyString = "Wash dishes: press '" + key1 + "', '" + key2 + "', or '" + key3 + "'";
			
			if (key1 != ' ') {
				GUI.color = Color.black;
			
				GUI.Label (keyRect, keyString);
			}
		}		
		
		// counters
		reactTick += Time.deltaTime;

		if (reactTick > 3.0f) {
		
			// add garbage and dishes

			addGarbage(Random.Range (3, 5));
			
			if (key1 != ' ') {
				addDishes(Random.Range (1, 3));
			}

			reactTick = 0.0f;
		
		}
		
		// key counter
		if (key1 == ' ') {
		
			keyCounter += Time.deltaTime;
			
			if (keyCounter >= 0.0f) {
			
				switchRandomKey (1);
				switchRandomKey (2);
				switchRandomKey (3);
				
			}
			
		}
		
	}
	
	public int addDishes(int dishesQuantity) {
	
		dishesCounter += dishesQuantity;
		
		if (dishesCounter > 110) {
			dishesCounter = 110;
			
			scoreScript.CreateFloatingNumber("+1",Color.red,1.5f,new Rect(650,400,50,20),25);
			scoreScript.CreateFloatingNumber("Too much dishes\nyour pulse is rising",Color.black,1.5f,new Rect(650,320,50,20),15,0);
			scoreScript.raiseHeartbeat(1);
		}
		
		return dishesCounter;
	}
	
	public int addGarbage(int garbageQuantity) {
		
		garbageCounter += garbageQuantity;
		if (garbageCounter > 130)
		{
			scoreScript.CreateFloatingNumber("+1",Color.red,1.5f,new Rect(650,450,50,20),25);
			scoreScript.CreateFloatingNumber("Throw the trash out the window! \nyour pulse is rising",Color.black,1.5f,new Rect(650,400,50,20),15,0);
			scoreScript.raiseHeartbeat(1);
		}
		
		return garbageCounter;
	}
	
	void OnMouseDown() {
	
		Debug.Log ("Mouse down kitchen!");
		
		if (garbageCounter > 100) {
			
			
			
			Debug.Log ("X: " + toxicCloudRect.x + ", Y:" + toxicCloudRect.y + ", W:" + toxicCloudRect.width + ", H: " + toxicCloudRect.height + ", MX:" + mousePosition.x + ", MY:" + mousePosition.y);
			
			if (toxicCloudRect.Contains(mousePosition)) {
			
				Debug.Log ("OK!");
				
				// mark pickup object to 2
				
				windowScript.setPickedUpMode(2);
					
			}
				
		}
		
	}
	
	void OnMouseUp() {
		
		int returnValue = windowScript.mouseUpEvent();
		
		// succeeded throw
		if (returnValue > 0) {
			// nullify trash
			garbageCounter = 0;
			
		}
		
	}
	
	private void switchRandomKey(int key)
	{
		bool newFound = false;
		char newKey = (char)'a';
		
		while(!newFound) {
		
			newKey = (char)'a'; //  + (char)(Random.Range (1, 20));
			newKey += (char)(Random.Range (1, 20));
			
			if (key == 1 && key2 != newKey && key3 != newKey) {
				newFound = true;
				key1 = newKey;
				
			}
			else if (key == 2 && key1 != newKey && key3 != newKey) {
				newFound = true;
				key2 = newKey;
				
			}
			else if (key == 3 && key1 != newKey && key2 != newKey) {
				newFound = true;
				key3 = newKey;
				
			}				
				
		}
		
		Debug.Log ("New key " + key + ": " + newKey);
		
	}
	
	private void washDishes() {
	
		key1 = ' ';
		key2 = ' ';
		key3 = ' ';
		
		keyCounter = -3.0f;
		
		// dishes wash
		addDishes (-10);
			
		if (dishesCounter <= 33) {
		
			dishesCounter = 0;
				
		}
		
		int score = Random.Range(50,100);
		scoreScript.Score += score;
		scoreScript.CreateFloatingNumber(score.ToString(),Color.blue,1f,new Rect(650,400,50,20));
	}
	
}
