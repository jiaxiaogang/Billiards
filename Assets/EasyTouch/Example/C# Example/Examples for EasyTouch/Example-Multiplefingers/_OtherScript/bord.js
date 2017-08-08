#pragma strict

var type:int;

private var cam:Camera;

function Start(){

	// Getting the main camera
	cam = GameObject.FindGameObjectWithTag("MainCamera").camera;
	
	if (type==1)
		transform.position = cam.ScreenToWorldPoint( Vector3( 0, 0,8));
	else if (type==2)
		transform.position = cam.ScreenToWorldPoint( Vector3( Screen.width, 0,8));
}