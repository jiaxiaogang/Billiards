using UnityEngine;
using System.Collections;

public class Photo : MonoBehaviour {
	
	private Vector3 deltaPosition;
	private Vector3 rotation;
	private bool newPivot=false;
	
		// Subscribe to events
	void OnEnable(){
		EasyTouch.On_DragStart += On_DragStart;
		EasyTouch.On_Drag += On_Drag;
		EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += On_TouchDown2Fingers;
		EasyTouch.On_PinchIn += On_PinchIn;
		EasyTouch.On_PinchOut += On_PinchOut;
		EasyTouch.On_Twist += On_Twist;
		EasyTouch.On_Cancel2Fingers += On_Cancel2Fingers;
	}
	
	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_DragStart -= On_DragStart;
		EasyTouch.On_Drag -= On_Drag;
		EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= On_TouchDown2Fingers;
		EasyTouch.On_PinchIn -= On_PinchIn;
		EasyTouch.On_PinchOut -= On_PinchOut;
		EasyTouch.On_Twist -= On_Twist;	
		EasyTouch.On_Cancel2Fingers -= On_Cancel2Fingers;
	}
	
	void On_Cancel2Fingers( Gesture gesture){
		if (gesture.touchCount>0){
			newPivot=true;	
		}
	}

	// One finger drag
	void On_DragStart( Gesture gesture){
	
	 	// restricted when there is only one touch 
		if (gesture.touchCount==1){
			// Calculate the delta position between touch and photo center position
			Vector3 position = gesture.GetTouchToWordlPoint(1);
			deltaPosition = position - transform.position;
		}
	}
	
	void On_Drag( Gesture gesture){
	
		if (gesture.touchCount==1){
			Vector3 position = gesture.GetTouchToWordlPoint(1);	
			if (newPivot){
				deltaPosition = position - transform.position;
				newPivot = false;
			}
				
			transform.position = position - deltaPosition;
		}

	}
	
	
	// when a two finger gesture begining
	void On_TouchStart2Fingers(Gesture gesture){
	
		// Calculate the delta position between touch and photo center position
		Vector3 position = gesture.GetTouchToWordlPoint(1);
		deltaPosition = position - transform.position;
	}
	
	void On_TouchDown2Fingers(Gesture gesture){

		// Moving during pinch & twist
		Vector3 position = gesture.GetTouchToWordlPoint(1);
		transform.position = position - deltaPosition;
	}
	
	
	// Zoom in and zoom out with pinch
	void On_PinchIn(Gesture gesture){
	
		float zoom = Time.deltaTime * gesture.deltaPinch/25;
		Vector3 scale = transform.localScale ;
	
		if ( scale.x - zoom>0.1)
			transform.localScale = new Vector3( scale.x - zoom, scale.y -zoom,1f);
	}
	
	void On_PinchOut(Gesture gesture){
	
		float zoom = Time.deltaTime * gesture.deltaPinch/25;
		Vector3 scale = transform.localScale ;
		
		if ( scale.x + zoom<3 )
			transform.localScale = new Vector3( scale.x + zoom, scale.y +zoom,1f);
	}
	
	// Twist
	void  On_Twist( Gesture gesture){
	
		transform.Rotate(new Vector3(0,0,gesture.twistAngle));
	}
	
	 

}
