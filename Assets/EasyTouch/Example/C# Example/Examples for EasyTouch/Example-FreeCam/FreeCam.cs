using UnityEngine;
using System.Collections;

public class FreeCam : MonoBehaviour {

	private float rotationX;
	private float rotationY;
	private Camera cam;
	
	// Subscribe to events
	void OnEnable(){
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_Swipe += On_Swipe;
	}
	
	void OnDisable(){
		UnsubscribeEvent();
	}
	
	void OnDestroy(){
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_Swipe -= On_Swipe;	
	}
		
	void Start(){
	
		cam = Camera.main;
	}
	
	
	void On_TouchDown(Gesture gesture){
	
		if (gesture.touchCount==2){
			cam.transform.Translate(new  Vector3(0,0,1f) * Time.deltaTime);
		}
		
		 if (gesture.touchCount==3){
			cam.transform.Translate( new Vector3(0,0,-1f) * Time.deltaTime);
		}	
	}
	
	
	void On_Swipe( Gesture gesture){
	
		rotationX += gesture.deltaPosition.x;
		rotationY += gesture.deltaPosition.y;
			
		cam.transform.localRotation = Quaternion.AngleAxis (rotationX, Vector3.up); 
		
		cam.transform.localRotation *= Quaternion.AngleAxis (rotationY, Vector3.left);
		
	}
}
