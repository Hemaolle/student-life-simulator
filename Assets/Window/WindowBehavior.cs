using UnityEngine;
using System.Collections;

public class WindowBehavior : MonoBehaviour {
		
	public GameObject throwAudioSource;
	public GameObject hitAudioSource;
	
	private int screamerCounter = 0;
	private int screamerPosX;
	private int screamerPosY;
	private Rect screamerRect;
	
	private int screamerMoveX;
	private int screamerMoveY;
	
	private int thrownMode;
	private int thrownPosX;
	private float thrownPosY;
	private float thrownSize;
	
	private int windowPosX = 50;
	private int windowPosY = 250;
		
	private float reactTick;
	private float screamerTick;
	
	public Texture2D screamer;
	
	public Texture2D beerCan;
	public Texture2D garbageBag;
	
	public Texture2D hit;
	private float hitBoxCounter = 0.0f;
	
	private float beerCanCounter = 1.0f;
	private int beerCanPositionX = -25;
	private Rect beerCanRect;
	
	private Rect windowRect;
	
	private Vector2 mousePosition;
	
	private int pickedUpMode;
	
	private ScoreScript scoreScript;
	
	public GameObject bonusAudioSource;
	
	// Use this for initialization
	void Start () {
		
		screamerRect = new Rect(10, 10, 64, 64);
		beerCanRect = new Rect(10,10, 64, 64);
		
		windowRect = new Rect(windowPosX - 50, windowPosY - 30, 250, 220);
		
		screamerCounter = 101;
		screamerMoveX = 1;
		screamerMoveY = 1;
		
		pickedUpMode = 0;
		
		screamerPosX = 85;
		screamerPosY = 70;		
		
		GameObject go = GameObject.Find("GUIGameObject");		
		scoreScript = go.GetComponent<ScoreScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
		mousePosition = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));		
		
	}
	
	public void OnGUI () 
	{	
				
		// render screamer
		if (screamerCounter > 100) {
			
			screamerRect.x = windowPosX + screamerPosX;
			screamerRect.y = windowPosY + screamerPosY;
			
			GUI.DrawTexture(screamerRect, screamer);
			
		}
		
		if (beerCanCounter > 0.0f) {
			
			beerCanRect.x = windowPosX + beerCanPositionX;
			beerCanRect.y = windowPosY + 150;
			beerCanRect.width = 64;
			beerCanRect.height = 64;
			
			GUI.DrawTexture (beerCanRect, beerCan);
			
		}
		
		// throw beer can
		if (thrownMode == 1) {
		
			// add thrown beer can
			
			GUI.DrawTexture (new Rect(thrownPosX, thrownPosY, (int)thrownSize, (int)thrownSize), beerCan);
			
			thrownSize -= (Time.deltaTime * 40.0f);
			if (thrownSize < 10.0f) {
				
				checkHit(thrownMode, thrownPosX, (int)thrownPosY);				
				
			}
			
		}
		// thrown garbage bag
		else if (thrownMode == 2) {
			
			GUI.DrawTexture (new Rect(thrownPosX, (int)thrownPosY, (int)thrownSize, (int)thrownSize), garbageBag);
			
			thrownSize -= (Time.deltaTime * 25.0f);
			thrownPosY += Time.deltaTime * (-5.0f + (64.0f - thrownSize) / 1.25f); // trash bag drops over time
			
			if (thrownSize < 10.0f) {
				
				checkHit(thrownMode, thrownPosX, (int)thrownPosY);
				
			}			
			
		}
		// show hit box
		else if (thrownMode == 3) {
		
			GUI.DrawTexture (new Rect(thrownPosX + Random.Range (1, 7) - 3, thrownPosY + Random.Range (1, 7) - 3, 24, 24), hit);
			
			hitBoxCounter -= (Time.deltaTime);
			
			if (hitBoxCounter < 0.0f) {
			
				thrownMode = 0;
			}
			
		}
		else {
		
			if (beerCanCounter < 1.0f) beerCanCounter+=Time.deltaTime;
			
		}
		
		
		reactTick += Time.deltaTime;
		screamerTick += Time.deltaTime;

		if (reactTick > 3.0f) {
		
			// add garbage and dishes

			screamerCounter+=Random.Range (1, 4);

			reactTick -= 3.0f;

			// random screamer movement
			screamerMoveX = (Random.Range (1, 11) - 5) * 2;
			screamerMoveY = (Random.Range (1, 7) - 3) * 2;
			
		}
		
		if (screamerTick > 1.0f) {
			
			screamerPosX += screamerMoveX;
			screamerPosY += screamerMoveY;
			
			if (screamerPosX > 100) screamerPosX = 100;
			else if (screamerPosX < -100) screamerPosX = -100;
			
			if (screamerPosY > 120) screamerPosY = 120;
			else if (screamerPosY < 70) screamerPosY = 70;
		
			screamerTick -= 1.0f;
		}

		
	}
	
	private int checkHit(int mode, int x, int y) {
	
		bool objectHit = false;
		
		x -= windowPosX;
		y -= windowPosY;
		
		x -= 30;
		y -= 30;		
		
		Debug.Log ("X: " + x +", Y: " + y + ", ScX: " + screamerPosX + ", ScY: " + screamerPosY);
		
		// check screamer hit
		if (screamerCounter > 100){
			
			if (x > screamerPosX - 20 && x < screamerPosX + 20) {
				
				if (y > screamerPosY - 20 && y < screamerPosY + 20) {
			
					// Add sound here
					hitAudioSource.audio.Play();
					// thrownMode 2 roskat, 1 tölkki
					int score = Random.Range(200,300);
					scoreScript.Score += score;
					scoreScript.CreateFloatingNumber(score.ToString(),Color.blue,1.5f,new Rect(150,400,50,20),30);
					
					
					screamerCounter = 0;
					
					hitBoxCounter = 2.0f;
					thrownMode = 3; // display hit box
					objectHit = true;
					
					return 0;
					
				}
				
			}
			
		}
		
		// trash can throw
		if (!objectHit) {
			
			// check here
			if (x > -30 && x < 30 && y > 70 && y < 95 && thrownMode == 2) {
			
				// play extra sound
				
				bonusAudioSource.audio.Play();
				
					int score = Random.Range(100,200);
					scoreScript.Score += score;
					scoreScript.CreateFloatingNumber(score.ToString(),Color.blue,1.5f,new Rect(120,400,50,20),30);
				
				Debug.Log ("Trash hit!");
				
			}
			
		}
		
		thrownMode = 0;
		
		return 0;
		
	}
	
	public void setThrownMode(int mode) {
	
		thrownMode = mode;
		
		Debug.Log ("Set:" + mode);
		
	}
	
	public void setPickedUpMode(int mode) {
	
		Debug.Log ("Pick up: " + mode);
		
		pickedUpMode = mode;
		
	}
	
	void throwObject(int objectMode, int x, int y, float size) {
		
		thrownMode = objectMode;
		thrownPosX = x - 25;
		thrownPosY = (float)y - 25;
		thrownSize = size;
		throwAudioSource.audio.Play();
	}
	
	void OnMouseDown() {
		
		Debug.Log ("Mouse down");

		if (thrownMode == 0) {
			
			if (beerCanRect.Contains (mousePosition)) {
		
				pickedUpMode = 1;
				
			}
			
		}
		
	}
	
	void OnMouseUp() {	
		
		mouseUpEvent();
	
	}
	
	public int mouseUpEvent() {
		
		Debug.Log ("Mouse up!");
		
		if (pickedUpMode == 1) {
			
			if (windowRect.Contains (mousePosition)) {
			
				throwObject(1, (int)mousePosition.x, (int)mousePosition.y, 64.0f);
				
				beerCanCounter = -8.0f;
				
				beerCanPositionX = Random.Range (1, 100) - 50;
				
				pickedUpMode = 0;
				
				return 1;
			}
			
		}
		else if (pickedUpMode == 2) {
			
			if (windowRect.Contains (mousePosition)) {
			
				throwObject(2, (int)mousePosition.x, (int)mousePosition.y, 64.0f);	
		
				pickedUpMode = 0;
				
				return 1;
			}
			
		}
		
		pickedUpMode = 0;
		
		return 0;
	}
	
}
