using UnityEngine;
using System.Collections;

public class TwoDrag : MonoBehaviour {

	private TextMesh textMesh;
	private Vector3 deltaPosition;	
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_DragStart2Fingers += On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers += On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers += On_DragEnd2Fingers;
		EasyTouch.On_Cancel2Fingers += On_Cancel2Fingers;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_DragStart2Fingers -= On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= On_DragEnd2Fingers;
		EasyTouch.On_Cancel2Fingers -= On_Cancel2Fingers;
	}
	
	void Start(){
		textMesh = transform.Find("TextDrag").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	// At the drag beginning
	void On_DragStart2Fingers(Gesture gesture){

		// Verification that the action on the object
		if (gesture.pickObject == gameObject){	
			gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		
			Vector3 position =  gesture.GetTouchToWordlPoint(  5);
			deltaPosition = position - transform.position;
		}
	}
	
	// During the drag
	void On_Drag2Fingers(Gesture gesture){

		// Verification that the action on the object
		if (gesture.pickObject == gameObject){	
			Vector3 position = gesture.GetTouchToWordlPoint(  5);
			
			transform.position = position - deltaPosition;
			
			float angles =  gesture.GetSwipeOrDragAngle(); 
			
			textMesh.text = gesture.swipe.ToString() + " / angle :" + angles.ToString("f2");
		}
	}
	
	// At the drag end
	void On_DragEnd2Fingers(Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){			
			transform.position=new Vector3(2.5f,-0.5f,-5f);
			gameObject.renderer.material.color = new Color(1f,1f,1f);
			textMesh.text="Drag me";
		}
	}
	
	
	// If the two finger gesture is finished
	void On_Cancel2Fingers(Gesture gesture){
		
		transform.position=new Vector3(2.5f,-0.5f,-5f);
		gameObject.renderer.material.color = new Color(1f,1f,1f);
		textMesh.text="Drag me";
		
	}
}
