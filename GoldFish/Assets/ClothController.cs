using UnityEngine;
using System.Collections;

public class ClothController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    float speedRatio = 0.2f;
	
	// Update is called once per frame
    void Update()
    {
        float move_x = 0;
        float move_y = 0;
        float move_z = 0;

        var trans1 = transform.Find("Cube1");
        var trans2 = transform.Find("Cube2");

        float speedRatio = 0.2f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move_x = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move_x = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move_y = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move_y = -1;
        }

        move_x *= speedRatio;
        move_y *= speedRatio;

        trans1.transform.Translate(new Vector3(move_x * speedRatio, move_y * speedRatio, 0));
        trans2.transform.Translate(new Vector3(move_x * speedRatio, move_y * speedRatio, 0));

    }
}
