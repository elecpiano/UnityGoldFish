using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TailBehavior : MonoBehaviour
{
    Transform[] joints = new Transform[FishBehavior.TAIL_ROW_INDEX_MAX + 1];
    List<float> RotationPool = new List<float>();
    public float DynamicRange = 0f;

    void Start()
    {
        for (int i = 0; i <= FishBehavior.TAIL_ROW_INDEX_MAX; i++)
        {
            joints[i] = transform.Find("RootJoint" + (i + 1).ToString());
        }
    }

    void Update()
    {
    }

    public void FollowUp(float[] angles)
    {
        for (int i = FishBehavior.TAIL_ROW_INDEX_MIN; i <= FishBehavior.TAIL_ROW_INDEX_MAX; i++)
        {
            joints[i].Rotate(0f, angles[i], 0f);
        }
    }

    public void AddRotationPool(float angle)
    {

    }

}
