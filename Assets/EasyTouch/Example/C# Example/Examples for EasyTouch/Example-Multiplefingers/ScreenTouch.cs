using UnityEngine;
using System.Collections;

public class ScreenTouch : MonoBehaviour {
	
	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;	
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart;		
	}
		
	// Simple tap message
	void On_TouchStart(Gesture gesture){
		
		if (gesture.pickObject==null){
			// Transforms 2D coordinate tap position in 3D world position
			Vector3 position = gesture.GetTouchToWordlPoint(8);
			
			// ...
			GameObject sphere = Instantiate(Resources.Load("GlowDisk01"), position , Quaternion.identity) as GameObject;
			float size = Random.Range(0.5f,0.8f);
			sphere.transform.localScale = new Vector3(size,size,size);
			
			GameObject spot= Instantiate(Resources.Load("Spot"), position , Quaternion.identity) as GameObject;
			spot.transform.localScale = sphere.transform.localScale/2;
			spot.transform.parent = sphere.transform;
			
			// Random color
			int rndColor = Random.Range(1,6);
		
			Color color = Color.white;
			switch (rndColor){
				case 1:
					color = new Color(1, Random.Range(0.0f,0.8f),Random.Range(0.0f,0.8f), Random.Range(0.3f,0.9f));
					break;
				case 2:
					color = new Color(Random.Range(0.0f,0.8f),1,Random.Range(0.0f,0.8f), Random.Range(0.3f,0.9f));
					break;
				case 3:
					color = new Color(Random.Range(0.0f,0.8f),1,1, Random.Range(0.3f,0.9f));
					break;
				case 4:
					color = new Color(1,Random.Range(0.0f,0.8f),1, Random.Range(0.3f,0.9f));
					break;
				case 5:
					color = new Color(1,Random.Range(0.0f,0.8f),Random.Range(0.0f,0.8f), Random.Range(0.3f,0.9f));
					break;
				case 6:
					color = new Color(Random.Range(0.0f,0.8f),Random.Range(0.0f,0.8f),1, Random.Range(0.3f,0.9f));
					break;
			
			}     
			sphere.renderer.material.SetColor ("_TintColor", color);
			spot.renderer.material.SetColor ("_TintColor",color);
			
			// assign the layer for auto detection
			sphere.layer=8;
			
			// Add a script to react with the touchs
			sphere.AddComponent("ObjectTouch");
			
			sphere.rigidbody.mass = size;
		}
	}

}
