using UnityEngine;
using System.Collections;

public class Anger : MonoBehaviour {

	public float angerDuration;
	public bool isAngry;
	public GameObject redFilter;
	public float rednessSpeed;
	public GameObject camera;
	
	private float transparency;
	private float angerStartTime;
	
	Vector3 originPosition;
	Quaternion originRotation;
 
	float shake_decay;
	float shake_intensity;
	
	// Use this for initialization
	void Start () {
		isAngry = false;
		transparency = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(isAngry)
		{			
			
			transparency += rednessSpeed * Time.deltaTime;
			Color red = Color.red;
			red.a = transparency;
			redFilter.renderer.material.color = red;
			if (Time.timeSinceLevelLoad - angerStartTime > angerDuration)
			{
				isAngry = false;
				redFilter.renderer.material.color = Color.clear;
				transparency = 0;
			}
			
			 
	        camera.transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
	        /*transform.rotation =  Quaternion(
	                        originRotation.x + Random.Range(-shake_intensity,shake_intensity)*.2,
	                        originRotation.y + Random.Range(-shake_intensity,shake_intensity)*.2,
	                        originRotation.z + Random.Range(-shake_intensity,shake_intensity)*.2,
	                        originRotation.w + Random.Range(-shake_intensity,shake_intensity)*.2);*/
	        shake_intensity += shake_decay;
	   
		}
	}
	
	public void GetAngry()
	{
		isAngry = true;
		angerStartTime = Time.timeSinceLevelLoad;
		
		originPosition = camera.transform.position;
	    originRotation = camera.transform.rotation;
	    shake_intensity = 1f;
	    shake_decay = 0.002f;
	}
}
