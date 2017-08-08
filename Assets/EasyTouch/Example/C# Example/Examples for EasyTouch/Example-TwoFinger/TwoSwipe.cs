using UnityEngine;
using System.Collections;

public class TwoSwipe : MonoBehaviour {

	private TextMesh textMesh;
	private GameObject trail;	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_SwipeStart2Fingers += On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers += On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers += On_SwipeEnd2Fingers;
		EasyTouch.On_Cancel2Fingers += On_Cancel2Fingers;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_SwipeStart2Fingers -= On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= On_SwipeEnd2Fingers;
		EasyTouch.On_Cancel2Fingers -= On_Cancel2Fingers;
	}
	
	void Start(){
		textMesh = GameObject.Find("LastSwipeText").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	
	// At the swipe beginning
	void On_SwipeStart2Fingers( Gesture gesture){
		
		if ( trail==null){ // Only the first finger
			Vector3 position = gesture.GetTouchToWordlPoint( 5);
			trail = Instantiate( Resources.Load("Trail"),position,Quaternion.identity) as GameObject;
			
			// For real swipe End
			EasyTouch.SetEnableTwist( false);
			EasyTouch.SetEnablePinch( false);
		}
	}
	
	// during the swipe
	void  On_Swipe2Fingers(Gesture gesture){
		
		if (trail!=null){
			Vector3 position = gesture.GetTouchToWordlPoint(5);
			trail.transform.position = position;
		}
	}
	
	// At the swipe end
	void  On_SwipeEnd2Fingers( Gesture gesture){
		
	
		if (trail!=null){
			
			Destroy(trail);
			float  angles = gesture.GetSwipeOrDragAngle(); 
			
			
			textMesh.text = "Last swipe : " + gesture.swipe.ToString() + " /  vector : " + gesture.swipeVector.normalized + " / angle : " + angles.ToString("f2");
			EasyTouch.SetEnableTwist( true);
			EasyTouch.SetEnablePinch( true);	
				
		}
	}
	
	// If the two finger gesture is finished
	void On_Cancel2Fingers(Gesture gesture){
	
		if (trail!=null){
			Destroy(trail);
			//textMesh.text = "Last swipe : cancel";
			EasyTouch.SetEnableTwist( true);
			EasyTouch.SetEnablePinch( true);		
		}
	}
	
}
