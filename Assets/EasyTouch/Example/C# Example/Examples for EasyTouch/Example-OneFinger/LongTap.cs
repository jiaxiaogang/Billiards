using UnityEngine;
using System.Collections;

public class LongTap : MonoBehaviour {

	private TextMesh textMesh;
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_LongTapStart += On_LongTapStart;
		EasyTouch.On_LongTap += On_LongTap;
		EasyTouch.On_LongTapEnd += On_LongTapEnd;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_LongTapStart -= On_LongTapStart;
		EasyTouch.On_LongTap -= On_LongTap;
		EasyTouch.On_LongTapEnd -= On_LongTapEnd;
	}
	
	void Start(){
		
		textMesh = transform.Find("TextLongTap").transform.gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	// At the long tap beginning 
	private void On_LongTapStart( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject==gameObject){
			gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
		}
	}
	
	// During the long tap 
	private void On_LongTap( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject==gameObject){
			textMesh.text = gesture.actionTime.ToString("f2");
		}
	
	}
	
	// At the long tap end
	private void On_LongTapEnd( Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject==gameObject){
			gameObject.renderer.material.color = Color.white;
			textMesh.text="Long tap";
		}
	
	}

}
