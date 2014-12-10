using UnityEngine;
using System.Collections;

public class ReactToClick : MonoBehaviour {
	
	
	private float pancakeChange = -0.1f;
	
	private bool clicked;
	public GUIStyle pointStyle;
	public GUIStyle scoreStyle;
	private int hungerScore;
	private int scoreAddition;
	private float scoreHeight = 0;
	GameObject go;
	PancakeProgressBar pancakeProgressBar;
	StatusBars hungerBar;
	ScoreScript scoreSript;
	float originalHeight;
	// Use this for initialization
	void Start () {
		clicked = false;
		go = GameObject.Find("GUIGameObject");
		pancakeProgressBar = go.GetComponent<PancakeProgressBar>();
		hungerBar = go.GetComponent<StatusBars>();
		scoreSript = go.GetComponent<ScoreScript>();
		originalHeight = transform.localScale.z;
	}
	
	void Update() {
			if(clicked)
		{
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + pancakeChange);			
			
				if(transform.localScale.z <= -originalHeight)
			{
				clicked = false;
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -originalHeight);
				//transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, originalHeight);
				pancakeChange = - pancakeChange;
				//scoreHeight = 0;
			}
				if(transform.localScale.z >= originalHeight)
			{
				clicked = false;
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, originalHeight);		
				pancakeChange = - pancakeChange;
			}
		}
				
	}
	
	
	// Update is called once per frame
	public void OnGUI () {
		/*if(clicked)
		{
			GUI.Label(new Rect(50,170-scoreHeight,50,20), hungerScore.ToString(), pointStyle);	
			GUI.Label(new Rect(50,150-scoreHeight,50,20), scoreAddition.ToString(), scoreStyle);	
		}*/
	}
	
	void OnMouseDown()
	{
		if(pancakeProgressBar.cooking && clicked == false && pancakeProgressBar.smoking == false)
		{
			clicked = true;
			go = GameObject.Find("GUIGameObject");
			pancakeProgressBar = go.GetComponent<PancakeProgressBar>();
			//hungerScore = (int)(pancakeProgressBar.progress * 100);
			scoreSript.CreateFloatingNumber(((int)(pancakeProgressBar.progress * 100)).ToString(), Color.green,1, new Rect(50,170,50,20));
			scoreSript.CreateFloatingNumber(((int)(pancakeProgressBar.progress * 300)).ToString(), Color.blue,1, new Rect(50,150,50,20));
			scoreSript.Score += (int)(pancakeProgressBar.progress * 300);
			hungerBar.hunger += pancakeProgressBar.progress * 2;
			if (hungerBar.hunger > 1)
				hungerBar.hunger = 1;
			pancakeProgressBar.progress = 0;
			audio.Play();
		}
		
		//scoreSript.CreateFloatingNumber("50", Color.green,1, new Rect(50,170-scoreHeight,50,20));
			
		//transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + 10);
		
	}
}
