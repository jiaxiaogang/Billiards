using UnityEngine;
using System.Collections;

public class TwoTouchStart : MonoBehaviour {

	private TextMesh textMesh;
	
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += On_TouchUp2Fingers;
		EasyTouch.On_Cancel2Fingers += On_Cancel2Fingers;
	}
	
	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= On_TouchUp2Fingers;
		EasyTouch.On_Cancel2Fingers -= On_Cancel2Fingers;
	}
	
	void Start(){	
		textMesh = transform.Find("TexttouchStart").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	void On_TouchStart2Fingers( Gesture gesture){
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){	
			gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		}
	}
	
	void On_TouchDown2Fingers(Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){	
			textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
		}
	}
	
	void On_TouchUp2Fingers( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){	
			gameObject.renderer.material.color = Color.white;
			textMesh.text ="Touch Start/Up";
		}
	}

	void On_Cancel2Fingers( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){	
			gameObject.renderer.material.color = Color.white;
			textMesh.text ="Touch Start/Up";
		}
	}
}
