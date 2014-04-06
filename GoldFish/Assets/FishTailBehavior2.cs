using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishTailBehavior2 : MonoBehaviour
{
    Transform[] joints = new Transform[10];

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            joints[i] = transform.Find("RootJoint" + (i + 1).ToString());

        }
    }

    void Update()
    {
    }

    public void FollowUp(float[] angles)
    {
        for (int i = 0; i < 10; i++)
        {
            joints[i].Rotate(0f, angles[i + 1], 0f);
        }
    }

}
