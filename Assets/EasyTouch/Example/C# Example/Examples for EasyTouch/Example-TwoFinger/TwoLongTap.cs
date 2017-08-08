using UnityEngine;
using System.Collections;

public class TwoLongTap : MonoBehaviour {

	private TextMesh textMesh;
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_LongTapStart2Fingers += On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers += On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers += On_LongTapEnd2Fingers;
		EasyTouch.On_Cancel2Fingers += On_Cancel2Fingers;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_LongTapStart2Fingers -= On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= On_LongTapEnd2Fingers;
		EasyTouch.On_Cancel2Fingers -= On_Cancel2Fingers;
	}
	
	void Start(){
		textMesh = transform.Find("TextLongTap").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	// At the long tap beginning
	void On_LongTapStart2Fingers( Gesture gesture){
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		}
	}
	
	// During the long tap
	void On_LongTap2Fingers( Gesture gesture){
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){		
			textMesh.text = gesture.actionTime.ToString("f2");
		}
	}
	
	// At the long tap end
	void On_LongTapEnd2Fingers( Gesture gesture){
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			gameObject.renderer.material.color = new Color(1f,1f,1f);
			textMesh.text="Long tap";
		}
	}
	
	// If the two finger gesture is finished
	void On_Cancel2Fingers(Gesture gesture){
		gameObject.renderer.material.color = new Color(1f,1f,1f);
		textMesh.text="Long tap";
	}
}
