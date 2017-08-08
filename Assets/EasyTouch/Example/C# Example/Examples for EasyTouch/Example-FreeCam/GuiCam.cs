using UnityEngine;
using System.Collections;

public class GuiCam : MonoBehaviour {
	
	void OnGUI() {
		GUI.matrix = Matrix4x4.Scale( new Vector3( Screen.width / 1024.0f, Screen.height / 768.0f, 1f ) );
		
		GUI.Box( new Rect( 0, -4, 1024, 30 ), "" );
		GUILayout.Label("Free camera ctrl or alt key to simulate the second finger");
		GUILayout.Space(15);
		GUILayout.Label( "1 finger => Look around");
		GUILayout.Label( "2 fingers => Move forward");
		GUILayout.Label( "3 fingers => Move backward");
		
		// Back to menu menu
		if (GUI.Button( new Rect(412,700,200,50),"Main menu")){
			Application.LoadLevel("StartMenu");
		}
	}
}
