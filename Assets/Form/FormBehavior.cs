using UnityEngine;
using System.Collections;

public class FormBehavior : MonoBehaviour {
	
	public string textAreaString;
	public bool boolToggle;
	
	private int reactMode;
	
	private int formPosX = 620;
	private int formPosY = 20;
	
	private Rect textFieldRect;
	private Rect boolToggleRect;
	
	private int reactTick;
	
	private float moneyRate = 0.05f;
	
	ScoreScript scoreSript;
	StatusBars statusBars;
	
	
	// Use this for initialization
	void Start () {
	
		textAreaString = "";
		boolToggle = false;
		
		reactMode = 0;
		reactTick = 0;
		
		textFieldRect.width = 75;
		textFieldRect.height = 20;
		
		boolToggleRect.width = 75;
		boolToggleRect.height = 20;
		
		GameObject go = GameObject.Find("GUIGameObject");
		scoreSript = go.GetComponent<ScoreScript>();
		statusBars = go.GetComponent<StatusBars>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnGUI () 
	{	
		
		
		if (reactMode == 1) {
			
    		textAreaString = GUI.TextField (textFieldRect, textAreaString);
			
			// full text check
			if (textAreaString.Length > 10) {
				
				reactMode = 0;
				
				giveScoreAndMoney();
			}
		
		}
		else if (reactMode > 1) {
		
			GUI.color = Color.black;
			boolToggle = GUI.Toggle (boolToggleRect, boolToggle, "Click here");
		
			// toggle check
			if (boolToggle == true) {
				
				reactMode = 0;
				
				giveScoreAndMoney();
				
			}
			
		}
		else {
		
			reactTick++;
			
			if (reactTick > 50) {
			
				reactMode = getRandomElement();
				
			}
			
		}
		
	}
	
	private void giveScoreAndMoney()
	{
		float money = Random.Range(0.1f, 5f);
		scoreSript.CreateFloatingNumber(((int)(80*money)).ToString(), Color.blue,1, new Rect(formPosX+30,formPosY+70,50,20));
		scoreSript.CreateFloatingNumber(money.ToString("0.00"), Color.yellow,1, new Rect(formPosX+30,formPosY+90,50,20));
		scoreSript.Score += (int)(80*money);
		statusBars.money += money*moneyRate;
		if(statusBars.money > 1)
			statusBars.money = 1;
		audio.Play();
	}
	
	private int getRandomElement() {
		
		int newState = Random.Range (1, 5);
		Rect newRect = getRandomPosition ();

		textFieldRect.x = newRect.x;
		textFieldRect.y = newRect.y;
		
		boolToggleRect.x = newRect.x;
		boolToggleRect.y = newRect.y;
		
		textAreaString = "";
		boolToggle = false;
		reactTick = -1 * Random.Range(1, 35);
		
		return newState;	
	}
	
	private Rect getRandomPosition() {
	
		Rect randomRect = new Rect(10, 10, 10, 10);
		int randomPos = Random.Range(1, 4);
		
		if (randomPos == 1) {
		
			randomRect.x = formPosX;
			randomRect.y = formPosY + 10;
			
		}
		else if (randomPos == 2) {
			
			randomRect.x = formPosX;
			randomRect.y = formPosY + 80;			
			
		}
		else if (randomPos == 3) {
			
			randomRect.x = formPosX;
			randomRect.y = formPosY + 160;
			
		}
		
		return randomRect;
	}
	
}
