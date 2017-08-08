using UnityEngine;
using System.Collections;

public class DoubleTap : MonoBehaviour {
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_DoubleTap += On_DoubleTap;	
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_DoubleTap -= On_DoubleTap;	
	}	
	
	// Double tap  
	private void On_DoubleTap( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			
	 		gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		}
	}
}
