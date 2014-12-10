using UnityEngine;
using System.Collections;

public class BeerBehavior : MonoBehaviour {
		
	private int beerPosX = 300;
	private int beerPosY = 320;
	
	private float bottlePosX;
	private float bottlePosY;
	
	private float headPosX;
	private float headPosY;
	
	private float bottleTargetX;
	private float bottleTargetY;
	
	private float beerLeft;
	private float drunkMeter;
	
	public Texture2D beerBottle;
	
	public Texture2D drinkingBottle;
	public Texture2D head;
	
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;	
	
	private float hitBoxCounter = 0.0f;
	
	private Rect beerBottleRect;
	private Rect headRect;
	private Rect drinkingBottleRect;
	
	private float beerPointInterval = 0.2f;
	private float lastBeerPointTime;
	private bool beerPointIntervalWaiting;
	
	public GameObject glugAudioSource;
	public GameObject bottleOpenAudioSource;
	
	private Vector2 mousePosition;
	
	private int bottles;
	
	int beerMode;
	
	float drunkMinusStart;
	bool drunkMinus = false;
	
	ScoreScript scoreScript;
	StatusBars statusBars;
	
	public GUIStyle titleStyle;
	
	
	// Use this for initialization
	void Start () {
		
		bottles = 6;
		beerMode = 1;
		
		beerBottleRect = new Rect(10, 10, 64, 64);
		headRect = new Rect(10, 10, 180, 180);
		drinkingBottleRect = new Rect(10, 10, 180, 180);
		
		headPosX = 50;
		headPosY = 50;
		
		bottlePosX = 50;
		bottlePosY = 25;
		bottleTargetX = 50;
		bottleTargetY = 25;
		
		drunkMeter = 20.0f;
		
		beerPointIntervalWaiting = false;
		
		GameObject go = GameObject.Find("GUIGameObject");		
		scoreScript = go.GetComponent<ScoreScript>();
		statusBars = go.GetComponent<StatusBars>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		mousePosition = new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y));		
		
		if (beerMode == 2) {
		
			if (Input.GetKey (KeyCode.UpArrow)) {
				
				if (headPosY > -75.0f) {
					headPosY -= (Time.deltaTime * 80.0f);
				}
				
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				
				if (headPosY < 50.0f) {
					headPosY += Time.deltaTime * 80.0f;
				}
				
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				
				if (headPosX > -60.0f){
					headPosX -= Time.deltaTime * 80.0f;	
				}
				
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				
				if (headPosX < 60.0f) {
					headPosX += Time.deltaTime * 80.0f;				
				}
				
			}			
			
		}
		if(Input.GetKey(KeyCode.KeypadEnter))
		
			Debug.Log (headPosX + ", " + headPosY + ", " + bottlePosX + ", " + bottlePosY);	
		
		// drunkness lowers
			drunkMeter -= Time.deltaTime * 0.6f;
		statusBars.checkVariable(ref drunkMeter,ref drunkMinus,ref drunkMinusStart,new Rect(beerPosX + 50, beerPosY + 230,50,20),true, Color.magenta, StatusBars.Stats.Intoxication);
		
	}
	
	public void OnGUI () 
	{	
				
		// render beer bottles
		
		int posX = 50;
		int posY = 20;
		int posXBonus = 20;
		
		beerBottleRect.width = 60;
		beerBottleRect.height = 80;	
		
		// show bottles mode
		if (beerMode == 1) {
		
			for (int i=0;i<bottles;i++) {
				
				beerBottleRect.x = beerPosX + posX;
				beerBottleRect.y = beerPosY + posY;
	
				GUI.DrawTexture (beerBottleRect, beerBottle);
			
				posX += posXBonus;
				
				if (i == 2) {
					
					posX = 30;
					posY += 22;
					
					beerBottleRect.width = 80;
					beerBottleRect.height = 100;
					
					posXBonus = 28;
					
				}
				
			}
			
			
			
			
		}
		// drinking mode
		else if (beerMode == 2) {
			
			headRect.x = beerPosX + headPosX;
			headRect.y = beerPosY + headPosY;
			
			GUI.DrawTexture (headRect, head);
				
			
			drinkingBottleRect.x = beerPosX + bottlePosX;
			drinkingBottleRect.y = beerPosY + bottlePosY;
			
			GUI.DrawTexture (drinkingBottleRect, drinkingBottle);
			
			// bottle movement
			
			if (Mathf.Abs (bottlePosX - bottleTargetX) < 5 && Mathf.Abs (bottlePosY - bottleTargetY) < 5) {
			
				bottleTargetX = (float)(Random.Range (1, 101) - 50);
				bottleTargetY = (float)(Random.Range (1, 101) - 80);
			
			}
			else {
				
				float moveBonus = 12.0f;
				
				if (bottleTargetX < bottlePosX - 2) {
				
					bottlePosX -= Time.deltaTime * moveBonus;
					
				}
				else if (bottleTargetX > bottlePosX + 2) {
				
					bottlePosX += Time.deltaTime * moveBonus;
					
				}
				
				if (bottleTargetY < bottlePosY - 2) {
					
					bottlePosY -= Time.deltaTime * moveBonus;
					
				}
				else if (bottleTargetY > bottlePosY + 2) {
				
					bottlePosY += Time.deltaTime * moveBonus;
					
				}
				
			}
			
			// hit check
			if (headPosX + 29 > bottlePosX - 15 && headPosX + 29 < bottlePosX + 15) {
				
				if (headPosY > bottlePosY + 25 - 15 && headPosY < bottlePosY + 25 + 15) {
			
					drunkMeter += Time.deltaTime * 1.5f;
					
					if(!beerPointIntervalWaiting)
					{
						lastBeerPointTime = Time.timeSinceLevelLoad;
						beerPointIntervalWaiting = true;
						int score = 1;
						scoreScript.Score += score;
						scoreScript.CreateFloatingNumber(score.ToString(),Color.magenta,1f,new Rect(370+headPosX,380+headPosY,20,20));
						glugAudioSource.audio.Play();
					}
					
					if(beerPointIntervalWaiting && Time.timeSinceLevelLoad - lastBeerPointTime > beerPointInterval)
						beerPointIntervalWaiting = false;
					
					if (drunkMeter > 100.0f) {
						drunkMeter = 100.0f;	
					}
					
				}
				
			}
			
			
			beerLeft -= Time.deltaTime;
			
			if (beerLeft < 0.0f) {
			
				beerMode = 1;
				
			}

		}
		
		// meters
		GUI.DrawTexture(new Rect(beerPosX + 20, beerPosY + 202, 200.0f, 20.0f), progressBarEmpty);
	    GUI.DrawTexture(new Rect(beerPosX + 20, beerPosY + 202, drunkMeter * 2f, 20.0f), progressBarFull);
		GUI.Label(new Rect(beerPosX + 40, beerPosY + 202, 150.0f, 20.0f),"Intoxication",titleStyle);
			
	}
	
	void OnMouseDown() {
		
		if (beerMode == 1 && bottles > 0) {
			
			bottles--;
			beerLeft = 30.0f;
			beerMode = 2;
			
			headPosX = 50;
			headPosY = 50;
			
			bottlePosX = 50;
			bottlePosY = 25;
			bottleTargetX = 50;
			bottleTargetY = 25;			
			
			bottleOpenAudioSource.audio.Play();
		}
	
	}
	
	void OnMouseUp() {	
		
	
	}
	
	public int addBottles(int quantity) {
	
		bottles += quantity;
		if (bottles > 6) {
			bottles = 6;	
		}
		
		return bottles;
	}
	
}
