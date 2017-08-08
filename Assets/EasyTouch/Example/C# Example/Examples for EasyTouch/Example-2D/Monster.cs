using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public GameObject enemy;

	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;
	}


	void On_TouchStart (Gesture gesture){
	
		if (gesture.pickObject != null){
			Destroy(gesture.pickObject);
			CreateMonster();
		}
	}

	void CreateMonster(){
		if (enemy !=null){
			Instantiate( enemy, Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width),Screen.height,Camera.main.nearClipPlane*2)), Quaternion.identity);
		}
	}
}
