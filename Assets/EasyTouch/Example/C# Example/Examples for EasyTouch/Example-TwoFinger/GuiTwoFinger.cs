using UnityEngine;
using System.Collections;

public class GuiTwoFinger : MonoBehaviour {

	void OnGUI() {
	            
		GUI.matrix = Matrix4x4.Scale( new Vector3( Screen.width / 1024.0f, Screen.height / 768.0f, 1f ) );
		
		GUI.Box( new Rect( 0, -4, 1024, 30 ), "" );
		GUILayout.Label("Examples with two fingers : ctrl key to swipe and  alt key to Twist and pinch to simulate the second finger");
			
		GUILayout.Label("For the 3 last balls, you need first positioning the orange target on the sphere, with the CTRL key, and next, press the ALT key, and do the action.");
		// Back to menu menu
		if (GUI.Button( new Rect(412,700,200,50),"Main menu")){
			Application.LoadLevel("StartMenu");
		}	
	}
}
