using UnityEngine;
using System.Collections;

public class TouchLayer : MonoBehaviour {
	
	private TextMesh textMesh;
	
	private Rect rect1 = new Rect(0,50,200,200);
	private Rect rect2 = new Rect(300,50,200,135);
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;
	}
	
	void OnDisable(){
		EasyTouch.On_TouchStart -= On_TouchStart;
	}
	
	void OnDestroy(){
		EasyTouch.On_TouchStart -= On_TouchStart;
	}
	

	void Start () {
		textMesh =(TextMesh)GameObject.Find("TouchOnLayer").transform.gameObject.GetComponent("TextMesh");
		EasyTouch.AddReservedGuiArea( rect1 );
		EasyTouch.AddReservedGuiArea( rect2 );
	}
	
	void OnGUI(){
		GUI.Box( rect1,"Reserved area");
		GUI.Box( rect2,"Reserved area");	
	}
	
	// At the touch beginning 
	public void On_TouchStart(Gesture gesture){
		
		// Verification that the action on the object
		if (gesture.pickObject !=null && !gesture.isHoverReservedArea){
			gesture.pickObject.renderer.material.color = new Color( Random.Range(0.0f,1.0f),  Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
			
			if (gesture.pickCamera == null){
				textMesh.text = "Touch on layer :" + LayerMask.LayerToName( gesture.pickObject.layer);
			}
			else{
				textMesh.text = "Touch on layer :" + LayerMask.LayerToName( gesture.pickObject.layer) + " / Camera : " + gesture.pickCamera.name;
			}
		}
		else{
			if (gesture.isHoverReservedArea){
				textMesh.text = "You touch a reserved area";	
			}
			else{
				textMesh.text = "Yout touch a free zone";	
			}
		}
			
	}
}
