using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishBehavior : MonoBehaviour
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

    public const int TAIL_ROW_INDEX_MIN = 0;
    public const int TAIL_ROW_INDEX_MAX = 19;

    float DelayAccumulation = 0f;
    List<TailBehavior> tailControllers = new List<TailBehavior>();
    Transform body = null;

    float[] rotationDeltas = new float[TAIL_ROW_INDEX_MAX + 1];
    float[] rotations = new float[TAIL_ROW_INDEX_MAX + 1];
    float bodyRotation;
    float rotationTarget;
    List<float> RotationPool = new List<float>();

    const float ROTATION_THRESHOLD = 0.1f;

    // Use this for initialization
    void Start()
    {
        var tails = gameObject.GetComponentsInChildren<TailBehavior>();
        for (int i = 0; i < tails.Length; i++)
        {
            tailControllers.Add(tails[i]);
        }

        body = transform.FindChild("fishbody");
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
            rotationTarget = GetNextAngle();
        }
    }

    private float GetNextAngle()
    {
        if (RotationPool.Count == 0)
        {
            float wonderRange = Random.Range(WONDER_RANGE_MIN, WONDER_RANGE_MAX);
            RotationPool.Add(wonderRange);
            RotationPool.Add(0 - wonderRange);
            RotationPool.Add(0 - wonderRange);
            RotationPool.Add(wonderRange);
        }

        float result = RotationPool[0];
        RotationPool.RemoveAt(0);
        return result;
    }

    public void ChangeDirection(float angle)
    {
        float upOrDown = angle > 0 ? 1f : -1f;
        float angle_abs = Mathf.Abs(angle);
        int max_count = (int)(angle_abs / WONDER_RANGE_MAX);
        float remainder = angle_abs % WONDER_RANGE_MAX;
        for (int i = 0; i < max_count; i++)
        {
            RotationPool.Add(upOrDown * WONDER_RANGE_MAX);
        }
        RotationPool.Add(upOrDown * remainder);
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
