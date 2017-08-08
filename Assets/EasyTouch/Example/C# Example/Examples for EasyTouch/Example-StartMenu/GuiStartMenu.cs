using UnityEngine;
using System.Collections;

public class GuiStartMenu : MonoBehaviour {
	
	void OnEnable(){	
		EasyTouch.On_SimpleTap += On_SimpleTap;
	}
	
	void OnGUI() {
	            
		GUI.matrix = Matrix4x4.Scale( new Vector3( Screen.width / 1024.0f, Screen.height / 768.0f, 1 ) );
		
		GUI.Box( new Rect( 0, -4, 1024, 70 ), "" );
		
	}
	
	void On_SimpleTap( Gesture gesture){
	
		if ( gesture.pickObject!=null){
			string levelName= gesture.pickObject.name;

			if (levelName=="OneFinger")
				Application.LoadLevel("Onefinger");
			else if (levelName=="TwoFinger")		
				Application.LoadLevel("TwoFinger");
			else if (levelName=="MultipleFinger")		
				Application.LoadLevel("MultipleFingers");	
			else if (levelName=="MultiLayer")
				Application.LoadLevel("MultiLayers");
			else if (levelName=="GameController")
				Application.LoadLevel("GameController");
			else if (levelName=="FreeCamera")
				Application.LoadLevel("FreeCam");			
			else if (levelName=="ImageManipulation")
				Application.LoadLevel("ManipulationImage");
			else if (levelName=="Joystick1")
				Application.LoadLevel("FirstPerson-DirectMode-DoubleJoystick");		
			else if (levelName=="Joystick2")
				Application.LoadLevel("ThirdPerson-DirectEventMode-DoubleJoystick");
			else if (levelName=="Button")
				Application.LoadLevel("ButtonExample");			
			else if (levelName=="Exit")
				Application.Quit();						
		}
		
	}
}
