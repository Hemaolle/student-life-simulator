using UnityEngine;
using System.Collections;

public class StatusBars : MonoBehaviour {
	
	public float hunger = 0.6f;
	public float money = 0.6f;
	public float heartbeat = 60.9f;
	public float maxHeartbeat = 200;
	
	Vector2 pos = new Vector2(30,520);
	Vector2 size = new Vector2(200,20);
	public Texture2D progressBarEmpty;
	public Texture2D moneyBarFull;
	public Texture2D progressBarFull;
	public float hungerSpeed = 0.05f;
	public float moneySpeed = 0.02f;
	public float heartBeatRestoreSpeed = 2f;
	
	public GameObject pointLossAudioSource;
	public GameObject heartBeatAudioSource;
	
	bool heartBeating;
	
	float hungerMinusStart;
	bool hungerMinus = false;
	
	float moneyMinusStart;
	bool moneyMinus = false;
	
	float minusTime = 2;
	
	public enum Stats {Hunger, Money, Intoxication};
	
	public GUIStyle HungerStyle;
	
	private ScoreScript scoreScript;
	// Use this for initialization
	void Start () {
		scoreScript = gameObject.GetComponent<ScoreScript>();
		heartBeating = false;
	}
	
	public void OnGUI()
	{
		GUI.depth = 2;
	    GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), progressBarEmpty);
	    GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(hunger), size.y), progressBarFull);
		
		GUI.Label(new Rect(50,520,100,20),"Hunger", HungerStyle);
		
		GUI.DrawTexture(new Rect(pos.x, pos.y+30, size.x, size.y), progressBarEmpty);
	    GUI.DrawTexture(new Rect(pos.x, pos.y+30, size.x * Mathf.Clamp01(money), size.y), moneyBarFull);
		
		GUI.Label(new Rect(50,550,100,20),"Money", HungerStyle);
		
		GUI.DrawTexture(new Rect(pos.x+290, pos.y+30, size.x, size.y), progressBarEmpty);
	    GUI.DrawTexture(new Rect(pos.x+290, pos.y+30, size.x * Mathf.Clamp01(heartbeat/maxHeartbeat), size.y), progressBarFull);
		
		GUI.Label(new Rect(pos.x+310,pos.y+30,100,20),"Pulse: " + (int)heartbeat, HungerStyle);
	}
	
	// Update is called once per frame
	void Update () {
		hunger -= Time.deltaTime * hungerSpeed;
		money -= Time.deltaTime * moneySpeed;
		heartbeat -= Time.deltaTime * heartBeatRestoreSpeed;
		if(heartbeat < 60.2)
			heartbeat = 60.2f;
		if(heartbeat > 170 && heartBeating == false)
		{
			heartBeating = true;
			heartBeatAudioSource.audio.Play();
		}
		
		if(heartbeat < 165 && heartBeating == true)
		{
			heartBeating = false;
			heartBeatAudioSource.audio.Stop();
		}
			
		
		if(heartbeat > 200)
		{
			PlayerPrefs.SetInt("Score",scoreScript.Score);
			Application.LoadLevel("GameOverScene");
		}
		
		checkVariable(ref hunger, ref hungerMinus, ref hungerMinusStart, new Rect(pos.x, pos.y+30, size.x, size.y),true, Color.green, Stats.Hunger);
		checkVariable(ref money, ref moneyMinus, ref moneyMinusStart, new Rect(pos.x, pos.y+60, size.x, size.y),true, Color.yellow, Stats.Money);
			
	}
	
	public void checkVariable(ref float originalVariable, ref bool variableMinus, ref float variableMinusStart, Rect location, bool limitMin, Color color, Stats statsType)
	{
		if (originalVariable > 1 && !limitMin)			
			originalVariable = 1;
		if (originalVariable < 0 && limitMin)
			originalVariable = 0;
		
		float variable;
		if(limitMin)
			variable = 1 - originalVariable;
		else
			variable = originalVariable;	
		
		if(variable >= 1 && variableMinus == false)
		{
			variableMinus = true;
			variableMinusStart = Time.timeSinceLevelLoad;
			scoreScript.CreateFloatingNumber("-50", color,1,location);
			scoreScript.CreateFloatingNumber("+1", Color.red,1,new Rect(location.x, location.y-20, location.width, location.height));
			scoreScript.Score -= 20;
			pointLossAudioSource.audio.Play();
			if(statsType == Stats.Hunger)
					scoreScript.CreateFloatingNumber("You are hungry, make some food!", Color.white, 2f, new Rect(50,150,200,20), 15,0);
			if(statsType == Stats.Money)
					scoreScript.CreateFloatingNumber("You have no money, fill some\nincome support applications", Color.black, 2f, new Rect(450,150,400,15), 15,0);
			if(statsType == Stats.Intoxication)
					scoreScript.CreateFloatingNumber("You are not intoxicated enough\ndrink some beer", Color.black, 2f, new Rect(300,300,200,20), 15,0);
			heartbeat += 1;
		}
		
		if(variableMinus == true && Time.timeSinceLevelLoad - variableMinusStart > minusTime)
		{			
			if(variable < 1)
				variableMinus = false;
			else
			{
				variableMinusStart = Time.timeSinceLevelLoad;
				scoreScript.CreateFloatingNumber("-50", color,1,location);	
				scoreScript.CreateFloatingNumber("+1", Color.red,1,new Rect(location.x, location.y-20, location.width, location.height));
				scoreScript.Score -= 20;
				pointLossAudioSource.audio.Play();
				if(statsType == Stats.Hunger)
					scoreScript.CreateFloatingNumber("You are hungry, make some food!", Color.white, 2f, new Rect(50,150,200,20), 15,0);
				if(statsType == Stats.Money)
					scoreScript.CreateFloatingNumber("You have no money, fill some\nincome support applications", Color.black, 2f, new Rect(465,150,400,15), 15,0);
				if(statsType == Stats.Intoxication)
					scoreScript.CreateFloatingNumber("You are not intoxicated enough\ndrink some beer", Color.black, 2f, new Rect(300,300,200,20), 15,0);
				heartbeat += 1;
			}
		}
		
		/*if (hunger > 1)
			hunger = 1;
		
		if(hunger >= 1 && hungerMinus == false)
		{
			hungerMinus = true;
			hungerMinusStart = Time.timeSinceLevelLoad;
			scoreScript.CreateFloatingNumber("-50", Color.red,1,new Rect(pos.x, pos.y+30, size.x, size.y));
		}
		
		if(hungerMinus == true && Time.timeSinceLevelLoad - hungerMinusStart > minusTime)
		{			
			if(hunger < 1)
				hungerMinus = false;
			else
			{
				hungerMinusStart = Time.timeSinceLevelLoad;
				scoreScript.CreateFloatingNumber("-50", Color.red,1,new Rect(pos.x, pos.y+30, size.x, size.y));				
			}
			
				
		}*/
	}
}
