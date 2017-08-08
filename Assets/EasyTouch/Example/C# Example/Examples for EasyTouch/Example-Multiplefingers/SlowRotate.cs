using UnityEngine;
using System.Collections;

public class SlowRotate : MonoBehaviour {

	public float rotateSpeed=0;
	
	void Start(){
	
		rotateSpeed = Random.Range(-60.0f,60.0f);
	}
	
	void Update(){
	
		transform.Rotate(new Vector3(0,0,rotateSpeed)*Time.deltaTime);
	}

}
