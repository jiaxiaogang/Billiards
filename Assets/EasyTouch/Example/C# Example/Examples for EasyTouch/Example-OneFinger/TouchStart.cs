using UnityEngine;
using System.Collections;

public class TouchStart : MonoBehaviour {
	
	private TextMesh textMesh;
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_TouchUp += On_TouchUp;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_TouchUp -= On_TouchUp;
	}
	
	void Start () {
		textMesh =(TextMesh) transform.Find("TexttouchStart").transform.gameObject.GetComponent("TextMesh");
	}
	
	// At the touch beginning 
	public void On_TouchStart(Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject)
			gameObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
	}
	
	// During the touch is down
	public void On_TouchDown(Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject)
			textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
	}
	
	// At the touch end
	public void On_TouchUp(Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject == gameObject){
			gameObject.renderer.material.color = new Color( 1f,1f,1f);
			textMesh.text ="Touch Start/Up";
		}
	}
}
