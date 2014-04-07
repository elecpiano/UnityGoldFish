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

    const float WONDER_RANGE_MIN = 5f;
    const float WONDER_RANGE_MAX = 10f;

    public const int MESH_ROW_COUNT = 10;
    public const int TAIL_ROW_INDEX_MIN = 2;
    public const int TAIL_ROW_INDEX_MAX = 9;

    float DelayAccumulation = 0f;
    List<FishTailBehavior2> tailControllers = new List<FishTailBehavior2>();
    Transform body = null;

    float[] rotationDeltas = new float[MESH_ROW_COUNT];
    float[] rotations = new float[MESH_ROW_COUNT];
    float bodyRotation;
    float rotationTarget;
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
        float rDelta = rotationTarget * portion;
        bodyRotation += rDelta;
        //rotate body
        body.Rotate(0f, rDelta, 0f);

        //rotate tails
        for (int i = TAIL_ROW_INDEX_MAX; i > TAIL_ROW_INDEX_MIN; i--)
        {
            rDelta = (rotations[i - 1] - rotations[i]) * TAIL_SOFTNESS;
            rotationDeltas[i] = rDelta;
            rotations[i] += rDelta;
        }

        rDelta = (bodyRotation - rotations[TAIL_ROW_INDEX_MIN]) * TAIL_SOFTNESS;
        rotationDeltas[TAIL_ROW_INDEX_MIN] = rDelta;
        rotations[TAIL_ROW_INDEX_MIN] += rDelta;

        foreach (var tailController in tailControllers)
        {
            tailController.FollowUp(rotationDeltas);
        }

        DelayAccumulation += delta;
        if (DelayAccumulation >= INTERVAL)
        {
            DelayAccumulation = 0f;
            rotationTarget = Wander();
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
