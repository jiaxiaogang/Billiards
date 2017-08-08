using UnityEngine;
using System.Collections;

public class TwoTap : MonoBehaviour {
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_SimpleTap2Fingers += On_SimpleTap2Fingers;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_SimpleTap2Fingers -= On_SimpleTap2Fingers;
	}
	
	// Simple 2 fingers tap
	void On_SimpleTap2Fingers( Gesture gesture){
		
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){	

			gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		}
	}
}
