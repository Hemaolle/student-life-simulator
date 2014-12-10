using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour {

	
	public Material DoorClosedMaterial;
	public Material DoorOpenMaterial;
	public GameObject personAtDoorObject;
	
	public Material jehovahsWitnessMaterial;
	public Material friendMaterial;
	
	public float doorBellIntervallMin;
	public float doorBellIntervallMax;
	
	public float answerTime;
	public float jehovahOpenDuration;
	public float friendOpenDuration;
	
	private float doorOpenIntervall;
	
	private float doorOpenTime;
	
	private float lastDoorBellTime;
	private float currentBellIntervall;
	
	public AudioClip witnessRing;
	public AudioClip friendsRing;
	
	private bool witnessAtDoor;
	private bool doorAnswerable;
	private bool doorOpen;
	
	public GameObject redFilter;
	public GameObject camera;
	public GameObject beer;
	public GameObject grumbleAudioSource;
	
	public Texture2D[] friendAnimation;
	int animationCounter;
	
	public float frameDuration;
	private float lastFrameTime;
	
	private float mainVolume;
	
	GameObject go;
	
	ScoreScript scoreSript;
	BeerBehavior beerScript;
	
	// Use this for initialization
	void Start () {
		currentBellIntervall = Random.Range (doorBellIntervallMin, doorBellIntervallMax);
		lastDoorBellTime = Time.timeSinceLevelLoad;
		doorAnswerable = false;
		doorOpen = false;
		
		go = GameObject.Find("GUIGameObject");		
		scoreSript = go.GetComponent<ScoreScript>();
		mainVolume = camera.audio.volume;
		beerScript = beer.GetComponent<BeerBehavior>();
	}
	
	void OnGUI() 
	{
		if(doorAnswerable)
			GUI.Label(new Rect(320,130,200,20),"Open door: press spacebar");
	}
		
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad - lastDoorBellTime > currentBellIntervall && doorOpen == false)
		{
			int random = Random.Range (1,3);
			//int random = 2;
			
			doorAnswerable = true;
			if(random == 1)
			{
				witnessAtDoor = true;
				audio.clip = witnessRing;
				doorOpenIntervall = jehovahOpenDuration;
			}
			else
			{
				witnessAtDoor = false;
				audio.clip = friendsRing;
				doorOpenIntervall = friendOpenDuration;
			}			
			
			audio.Play();
			lastDoorBellTime = Time.timeSinceLevelLoad;
			currentBellIntervall = Random.Range (doorBellIntervallMin, doorBellIntervallMax);
		}
		
		if (Time.timeSinceLevelLoad - lastDoorBellTime > answerTime)
		{
			doorAnswerable = false;	
		}
		
		  if (Input.GetKeyDown ("space") && doorAnswerable == true)
		{
			doorOpen = true;
			doorAnswerable = false;
			doorOpenTime = Time.timeSinceLevelLoad;
			renderer.material = DoorOpenMaterial;
			if(witnessAtDoor)
			{
				personAtDoorObject.renderer.material = jehovahsWitnessMaterial;
				GetComponent<Anger>().GetAngry();
				scoreSript.Score -= 400;
				scoreSript.CreateFloatingNumber("-400", Color.blue, 1.5f, new Rect(400,200,50,20), 50);
				scoreSript.CreateFloatingNumber("+5", Color.red,1.5f,new Rect(400,150,50,20),50);
				scoreSript.raiseHeartbeat(5);
				grumbleAudioSource.audio.Play();
			}
			else
			{
				personAtDoorObject.renderer.material = friendMaterial;
				
				camera.audio.volume = 0;
				personAtDoorObject.audio.Play();
				
				scoreSript.Score += 400;
				scoreSript.CreateFloatingNumber("400", Color.blue, 2f, new Rect(400,200,50,20), 50);
				scoreSript.CreateFloatingNumber("Your friend brings more beer!", Color.black, 2f, new Rect(300,250,200,20), 30,0);
				
				beerScript.addBottles(3);
				lastFrameTime = Time.timeSinceLevelLoad;
				animationCounter = 0;
			}
			
			personAtDoorObject.renderer.enabled = true;
			
		}
		
		if (doorOpen == true && witnessAtDoor == false)
			if(Time.timeSinceLevelLoad - lastFrameTime > frameDuration)
		{
			animationCounter++;
			lastFrameTime = Time.timeSinceLevelLoad;
			if (animationCounter > friendAnimation.Length - 1)
				animationCounter = 0;
			personAtDoorObject.renderer.material.mainTexture = friendAnimation[animationCounter];
				
		}
		
		
		if (doorOpen == true && Time.timeSinceLevelLoad - doorOpenTime > doorOpenIntervall)
		{
			doorOpen = false;
			personAtDoorObject.renderer.enabled = false;
			renderer.material = DoorClosedMaterial;
			lastDoorBellTime = Time.timeSinceLevelLoad;
			camera.audio.volume = mainVolume;
		}
	}
	
	
}
