using UnityEngine;
using System.Collections;

public class FishMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    float speedRatio = 0.01f;

    void Update()
    {
        float move_x = 0;
        float move_z = 0;

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    move_x = -1;
        //}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move_x = 1;
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    move_z = 1;
        //}
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move_z = -1;
        }

        move_x *= speedRatio;
        move_z *= speedRatio;

        transform.Translate(new Vector3(move_x * speedRatio, 0, move_z * speedRatio));
    }
}
