using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {
	
	Vector3 originPosition;
	Quaternion originRotation;
 
	float shake_decay;
	float shake_intensity;
 
	void OnGUI () {
    	/*if (GUI.Button (new Rect (20,400,80,20), "Shake")) {
        	Shake();
    	}*/
	} 
 
	void Update(){
	    if(shake_intensity > 0){
	        transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
	        /*transform.rotation =  Quaternion(
	                        originRotation.x + Random.Range(-shake_intensity,shake_intensity)*.2,
	                        originRotation.y + Random.Range(-shake_intensity,shake_intensity)*.2,
	                        originRotation.z + Random.Range(-shake_intensity,shake_intensity)*.2,
	                        originRotation.w + Random.Range(-shake_intensity,shake_intensity)*.2);*/
	        shake_intensity -= shake_decay;
	    }
	}
 
	void Shake(){
	    originPosition = transform.position;
	    originRotation = transform.rotation;
	    shake_intensity = 1f;
	    shake_decay = 0.002f;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	
}
