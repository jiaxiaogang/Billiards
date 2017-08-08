using UnityEngine;
using System.Collections;

[System.Serializable]
public class ECamera{

	public Camera camera;
	public bool guiCamera;
	
	public ECamera( Camera cam,bool gui){
		this.camera = cam;
		this.guiCamera = gui;
	}
	
}
