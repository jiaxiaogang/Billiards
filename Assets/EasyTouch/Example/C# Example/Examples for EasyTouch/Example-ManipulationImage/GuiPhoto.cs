using UnityEngine;
using System.Collections;

public class GuiPhoto : MonoBehaviour {

	private bool bTwist=true;
	private bool bPinch=true;
	
	void OnGUI() {
	            
		GUI.matrix = Matrix4x4.Scale( new Vector3( Screen.width / 1024.0f, Screen.height / 768.0f, 1f ) );
		
		GUI.Box( new Rect( 0, -4, 1024, 30 ), "" );
		GUILayout.Label("Manipulation of an image : Twist, Pinch, Drag  with 1 or 2 fingers, ctrl key to swipe and  alt key to Twist and pinch to simulate the second finger");
		
		GUILayout.Space(15);
		
		bTwist = GUILayout.Toggle(bTwist,"Enable Twist");
		EasyTouch.SetEnableTwist( bTwist);
		
		GUILayout.Space(15);
		
		bPinch = GUILayout.Toggle(bPinch,"Enable Pinch");
		EasyTouch.SetEnablePinch( bPinch);
		
		// Back to menu menu
		if (GUI.Button( new Rect(412,700,200,50),"Main menu")){
			Application.LoadLevel("StartMenu");
		}	
	}

}
