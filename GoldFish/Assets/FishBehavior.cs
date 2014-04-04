using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishBehavior : MonoBehaviour
{
    const float TAIL_SOFTNESS = 0.15f; /* the smaller, the softer */

    const float INTERVAL_MIN = 0.05f;
    const float INTERVAL_MAX = 0.15f;
    float Interval = 0.1f;

    const float WONDER_UNIT = 3f;

    float DelayAccumulation = 0f;
    List<FishTailBehavior> tailControllers = new List<FishTailBehavior>();
    Transform body = null;

    Vector3[] rotationVectors = new Vector3[12];
    float[] rotationDeltas = new float[12];
    float[] rotations = new float[12];
    float[] rotationCaches = new float[12];
    List<float> RotationPool = new List<float>();

    float MovementSpeed = 0.01f;

    // Use this for initialization
    void Start()
    {
        var tails = gameObject.GetComponentsInChildren<FishTailBehavior>();
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
        float portion = delta / Interval;
        float rDelta = rotationCaches[0] * portion;
        rotations[0] += rDelta;
        //rotate body
        body.Rotate(0f, rDelta, 0f);

        //rotate tails
        for (int i = 1; i < 12; i++)
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
        if (DelayAccumulation >= Interval)
        {
            DelayAccumulation = 0f;
            rotationCaches[0] = Wander();
            Interval = Random.Range(INTERVAL_MIN, INTERVAL_MAX);
        }
    }

    private float Wander()
    {
        if (RotationPool.Count == 0)
        {
            int intervalSpan = Random.Range(3, 9);
            int upOrDown = Random.Range(0, 2);
            for (int i = 0; i < intervalSpan; i++)
            {
                RotationPool.Add((upOrDown == 0 ? 1f : -1f) * WONDER_UNIT);
            }

            // opposite rotation
            for (int i = 0; i < intervalSpan; i++)
            {
                RotationPool.Add((upOrDown == 0 ? -1f : 1f) * WONDER_UNIT);
            }
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
            var delta = (upOrDown ? 1f : -1f) * WONDER_UNIT;
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
