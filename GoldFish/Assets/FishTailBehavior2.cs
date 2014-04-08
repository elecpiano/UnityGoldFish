using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishTailBehavior2 : MonoBehaviour
{
    Transform[] joints = new Transform[FishBehavior2.TAIL_ROW_INDEX_MAX + 1];

    void Start()
    {
        for (int i = 0; i <= FishBehavior2.TAIL_ROW_INDEX_MAX; i++)
        {
            joints[i] = transform.Find("RootJoint" + (i + 1).ToString());

        }
    }

    void Update()
    {
    }

    public void FollowUp(float[] angles)
    {
        for (int i = FishBehavior2.TAIL_ROW_INDEX_MIN; i <= FishBehavior2.TAIL_ROW_INDEX_MAX; i++)
        {
            joints[i].Rotate(0f, angles[i], 0f);
        }
    }

}
