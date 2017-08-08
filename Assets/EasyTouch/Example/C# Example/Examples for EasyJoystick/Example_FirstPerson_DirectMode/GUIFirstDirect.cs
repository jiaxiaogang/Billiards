using UnityEngine;
using System.Collections;

public class GUIFirstDirect : MonoBehaviour {

	
	private bool showProperties = true;
	private EasyJoystick joystick;
	
	void Start(){
		joystick = GameObject.Find("Move_Turn_Joystick").GetComponent<EasyJoystick>();
	}
	
	void OnGUI(){
		
		showProperties =  GUI.Toggle( new Rect(5,5,320,20),showProperties, "Show some properties for example for left joystick");
		if (showProperties){
			GUI.Box( new Rect(5,25,260,200),"");
			
			joystick.enableInertia = GUI.Toggle( new Rect(10,30,200,20),joystick.enableInertia,"Activated inertia");
			
			if (joystick.enableInertia){
				GUI.Label( new Rect(10,50,200,25),"X inertia : " + joystick.inertia.x.ToString("F1")); 
				joystick.inertia.x =  GUI.HorizontalSlider( new Rect(130,55,125,20), joystick.inertia.x,0,200);

				GUI.Label( new Rect(10,75,200,25),"Y inertia : " + joystick.inertia.y.ToString("F1")); 
				joystick.inertia.y =  GUI.HorizontalSlider( new Rect(130,80,125,20), joystick.inertia.y,0,200);
			}
			
			GUI.Label( new Rect(10,105,200,25),"x axis speed : " + joystick.speed.x.ToString("F1")); 
			joystick.speed.x =  GUI.HorizontalSlider( new Rect(130,110,125,20), joystick.speed.x,0,200);
			
			GUI.Label( new Rect(10,130,200,25),"Y axis speed : " + joystick.speed.y.ToString("F1")); 
			joystick.speed.y =  GUI.HorizontalSlider( new Rect(130,135,125,20), joystick.speed.y,0,20);

			joystick.inverseXAxis = GUI.Toggle( new Rect(10,160,200,20),joystick.inverseXAxis,"Inverse X axis");
			joystick.inverseYAxis = GUI.Toggle( new Rect(10,185,200,20),joystick.inverseYAxis,"Inverse Y axis");
			
		}
	}
}
