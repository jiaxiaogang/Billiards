using UnityEngine;
using System.Collections;

public class Pinch : MonoBehaviour {

	private TextMesh textMesh;
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
		EasyTouch.On_PinchIn += On_PinchIn;
		EasyTouch.On_PinchOut += On_PinchOut;
		EasyTouch.On_PinchEnd += On_PinchEnd;
		EasyTouch.On_Cancel2Fingers += On_Cancel2Fingers;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	// Unsubscribe to events
	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
		EasyTouch.On_PinchIn -= On_PinchIn;
		EasyTouch.On_PinchOut -= On_PinchOut;
		EasyTouch.On_PinchEnd -= On_PinchEnd;
		EasyTouch.On_Cancel2Fingers -= On_Cancel2Fingers;
	}
	
	void Start(){
		textMesh = transform.Find("TextPinch").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	// At the 2 fingers touch beginning
	private void On_TouchStart2Fingers( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){		
			// disable twist gesture recognize for a real pinch end
			EasyTouch.SetEnableTwist( false);
			EasyTouch.SetEnablePinch( true);
		}
	}
	
	// At the pinch in
	private void On_PinchIn(Gesture gesture){
	
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			
			float zoom = Time.deltaTime * gesture.deltaPinch;
		
			Vector3 scale = transform.localScale ;
			transform.localScale = new Vector3( scale.x - zoom, scale.y -zoom, scale.z-zoom);
			
			textMesh.text = "Delta pinch : " + gesture.deltaPinch.ToString();
		}
		
	}
	
	// At the pinch out
	private void On_PinchOut(Gesture gesture){
	
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			float zoom = Time.deltaTime * gesture.deltaPinch;
		
			Vector3  scale = transform.localScale ;
			transform.localScale = new Vector3( scale.x + zoom, scale.y +zoom,scale.z+zoom);
			
			textMesh.text = "Delta pinch : " + gesture.deltaPinch.ToString();
		}
	}
	
	// At the pinch end
	private void On_PinchEnd(Gesture gesture){
		
		if (gesture.pickObject == gameObject){
			transform.localScale =new Vector3(1.7f,1.7f,1.7f);
			EasyTouch.SetEnableTwist( true);
			textMesh.text="Pinch me";
		}
		
	}
	
	
	// If the two finger gesture is finished
	private void On_Cancel2Fingers(Gesture gesture){
		/*
		transform.localScale =new Vector3(1.7f,1.7f,1.7f);
		EasyTouch.SetEnableTwist( true);
		textMesh.text="Pinch me";
		*/
	}
	
	
}
