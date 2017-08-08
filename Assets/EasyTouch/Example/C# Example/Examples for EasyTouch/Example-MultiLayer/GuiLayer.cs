using UnityEngine;
using System.Collections;

public class GuiLayer : MonoBehaviour {

	
	void OnGUI() {
	            
		GUI.matrix = Matrix4x4.Scale( new Vector3( Screen.width / 1024.0f, Screen.height / 768.0f, 1f ) );
		
		GUI.Box( new Rect( 0, -4, 1024, 30 ), "" );
		GUILayout.Label("Multi layers/camera example and reserved area");
		
		// Back to menu menu
		if (GUI.Button( new Rect(412,700,200,50),"Main menu")){
			Application.LoadLevel("StartMenu");
		}	
	}

}
