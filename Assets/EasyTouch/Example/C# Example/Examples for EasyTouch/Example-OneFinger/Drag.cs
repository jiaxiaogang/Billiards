using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

	private TextMesh textMesh;
	private Vector3 deltaPosition;

	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_Drag += On_Drag;
		EasyTouch.On_DragStart += On_DragStart;
		EasyTouch.On_DragEnd += On_DragEnd;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_Drag -= On_Drag;
		EasyTouch.On_DragStart -= On_DragStart;
		EasyTouch.On_DragEnd -= On_DragEnd;
	}	
	
	
	void Start(){
		textMesh = transform.Find("TextDrag").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	// At the drag beginning 
	void On_DragStart( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		
			// the world coordinate from touch for z=5
			Vector3 position = gesture.GetTouchToWordlPoint(5);
			
			
			deltaPosition = position - transform.position;
		}	
	}
	
	// During the drag
	void On_Drag(Gesture gesture){
	
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			
			// the world coordinate from touch for z=5
			Vector3 position = gesture.GetTouchToWordlPoint(5);
			
			transform.position = position - deltaPosition;
			
			// Get the drag angle
			float angle = gesture.GetSwipeOrDragAngle();
			
			textMesh.text = gesture.swipe.ToString() + " / angle :" + angle.ToString("f2");
		}
	}
	
	// At the drag end
	void On_DragEnd(Gesture gesture){
	
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			transform.position= new Vector3(3f,1.8f,-5f);
			gameObject.renderer.material.color = Color.white;
			textMesh.text="Drag me";
		}
	}
}
