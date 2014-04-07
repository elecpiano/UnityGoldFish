using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishBehavior2 : MonoBehaviour
{
    const float TAIL_SOFTNESS = 0.3f; /* the smaller, the softer */
    //const float TAIL_SOFTNESS_MIN = 0.2f; 
    //const float TAIL_SOFTNESS_MAX = 0.4f; 

    //const float INTERVAL_MIN = 0.1f;
    //const float INTERVAL_MAX = 0.2f;
    //float Interval = 0.15f;
    const float INTERVAL = 0.2f;

    const float WONDER_RANGE_MIN = 10f;
    const float WONDER_RANGE_MAX = 20f;

    float DelayAccumulation = 0f;
    List<FishTailBehavior2> tailControllers = new List<FishTailBehavior2>();
    Transform body = null;

    Vector3[] rotationVectors = new Vector3[11];
    float[] rotationDeltas = new float[11];
    float[] rotations = new float[11];
    float[] rotationCaches = new float[11];
    List<float> RotationPool = new List<float>();

    // Use this for initialization
    void Start()
    {
        var tails = gameObject.GetComponentsInChildren<FishTailBehavior2>();
        for (int i = 0; i < tails.Length; i++)
        {
            tailControllers.Add(tails[i]);
        }

        body = transform.FindChild("fishbody2");
    }

    void Update()
    {
        FollowUp();
        HandleKeyboardInput();
    }

    private void FollowUp()
    {
        float delta = Time.deltaTime;
        float portion = delta / INTERVAL;
        float rDelta = rotationCaches[0] * portion;
        rotations[0] += rDelta;
        //rotate body
        body.Rotate(0f, rDelta, 0f);

        //rotate tails
        //float tailSoftness = Random.Range(TAIL_SOFTNESS_MIN, TAIL_SOFTNESS_MAX);
        for (int i = 10; i > 0; i--)
        {
            rDelta = (rotations[i - 1] - rotations[i]) * TAIL_SOFTNESS;
            rotationDeltas[i] = rDelta;
            rotations[i] += rDelta;
        }

        foreach (var tailController in tailControllers)
        {
            tailController.FollowUp(rotationDeltas);
        }

        DelayAccumulation += delta;
        if (DelayAccumulation >= INTERVAL)
        {
            DelayAccumulation = 0f;
            rotationCaches[0] = Wander();
            //Interval = Random.Range(INTERVAL_MIN, INTERVAL_MAX);
        }
    }

    private float Wander()
    {
        if (RotationPool.Count == 0)
        {
            float wonderRange = Random.Range(WONDER_RANGE_MIN, WONDER_RANGE_MAX);
            RotationPool.Add(wonderRange);
            RotationPool.Add(0 - wonderRange);
            RotationPool.Add(0 - wonderRange);
            RotationPool.Add(wonderRange);

            //int intervalSpan = Random.Range(3, 9);
            //int upOrDown = Random.Range(0, 2);
            //for (int i = 0; i < intervalSpan; i++)
            //{
            //    RotationPool.Add((upOrDown == 0 ? 1f : -1f) * WONDER_UNIT);
            //}
            //for (int i = 0; i < intervalSpan; i++)
            //{
            //    RotationPool.Add((upOrDown == 0 ? -1f : 1f) * WONDER_UNIT);
            //}
        }

        float result = RotationPool[0];
        RotationPool.RemoveAt(0);
        return result;
    }

    public void ChangeDirection(float angle)
    {
        bool upOrDown = angle > 0;
        while (upOrDown ? (angle > 0) : (angle < 0))
        {
            var delta = (upOrDown ? 1f : -1f) * WONDER_RANGE_MAX;
            RotationPool.Add(delta);
            angle -= delta;
        }
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeDirection(45f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeDirection(-45f);
        }
    }

}
