using UnityEngine;
using System.Collections;

public class HeadBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float move_x = 0;
		float move_y = 0;
		float move_z = 0;

		float speedRatio = 0.2f;
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			move_x =-1;
				}
		if (Input.GetKey(KeyCode.RightArrow)) {
			move_x = 1;
				}
		if (Input.GetKey(KeyCode.UpArrow)) {
			move_y =1;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			move_y =-1;
		}

		move_x *= speedRatio;
		move_y *= speedRatio;
		
		this.transform.Translate(new Vector3(move_x,move_y,0));
		
	}
}
