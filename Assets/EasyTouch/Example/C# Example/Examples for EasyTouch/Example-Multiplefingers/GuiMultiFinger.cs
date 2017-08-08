using UnityEngine;
using System.Collections;

public class GuiMultiFinger : MonoBehaviour {

	void OnGUI() {
	            
		GUI.matrix = Matrix4x4.Scale( new Vector3( Screen.width / 1024.0f, Screen.height / 768.0f, 1 ) );
		
		GUI.Box( new Rect( 0, -4, 1024, 30 ), "" );
		GUILayout.Label("Example of multiple fingers (10 max) :  touch, tap, long tap, pinch : ctrl or alt key to simulate the second finger");
		GUILayout.Space(10);
		GUILayout.Label( "Touch => Creation of a circle under each touches");
		GUILayout.Label( "Tap on circle => Add ring under each touches ");
		GUILayout.Label( "Long tap on circle => Change the ring speed under each touches");
		GUILayout.Label( "Drag => move the circles under the touches");
		GUILayout.Label( "Pinch on circle => Size change");
		
		// Back to menu menu
		if (GUI.Button( new Rect(412,700,200,50),"Main menu")){
			Application.LoadLevel("StartMenu");
		}	
	}
}
