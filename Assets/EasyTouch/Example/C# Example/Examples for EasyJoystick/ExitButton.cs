using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
	
	void OnEnable(){
		EasyButton.On_ButtonUp += On_ButtonUp;	
	}

	void OnDisable(){
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}

	void OnDestroy(){
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
	
	void On_ButtonUp (string buttonName)
	{
		Application.LoadLevel("StartMenu");	
	}

}
