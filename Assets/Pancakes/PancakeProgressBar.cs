using UnityEngine;
using System.Collections;

public class PancakeProgressBar : MonoBehaviour {
	
	public float progress = 0;
	Vector2 pos = new Vector2(0,0);
	Vector2 size = new Vector2(200,20);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public bool cooking = false;
	public float cookingSpeed = 5f;
	
	public ParticleSystem smoke;	
	public bool smoking;
	public float smokingTime = 5f;
	
	private float smokingStartTime;
	
	public GameObject alarmSource;
	
	// Use this for initialization
	void Start () {
		cooking = false;
	}	
		 
	public void OnGUI()
	{
	    GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), progressBarEmpty);
	    GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(progress), size.y), progressBarFull);
		
		if(GUI.Button(new Rect(200,0,75,20), "Cook"))
			cooking = true;
		if(GUI.Button(new Rect(200,20,75,20), "Stop"))
		{
			if(!smoking)
			{
				cooking = false;
				progress = 0;
			}
		}
			
		
		GUI.Box(new Rect(0,250,1 ,1), " ");
		GUI.Box(new Rect(266,250,10,10), " ");
		GUI.Box(new Rect(533,250,10,10), " ");
		GUI.Box(new Rect(790,250,10,10), " ");
		GUI.Box(new Rect(0,500,1 ,1), " ");
		GUI.Box(new Rect(266,500,10,10), " ");
		GUI.Box(new Rect(533,500,10,10), " ");
		GUI.Box(new Rect(790,500,10,10), " ");
		
		
		//GUI.Label(new Rect(20,400,100,20), progress.ToString());	
	} 
	 
	void Update()
	{
		if (cooking)
	    	progress += Time.deltaTime * cookingSpeed;
		if(progress > 1 && smoking == false)
		{
			smoke.Play();	
			smoking = true;	
			smokingStartTime = Time.timeSinceLevelLoad;
			alarmSource.audio.Play();
		}
		if(smoking && Time.timeSinceLevelLoad - smokingStartTime > smokingTime)
		{
			progress = 0;
			smoking = false;
			smoke.Stop();
			cooking = false;
			alarmSource.audio.Stop();
		}
		
	}
	
}
