using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishTailBehavior : MonoBehaviour
{
    Transform[] joints = new Transform[11];

    void Start()
    {
        joints[0] = transform.Find("Joint1");
        joints[1] = transform.Find("Joint2");
        joints[2] = transform.Find("Joint3");
        joints[3] = transform.Find("Joint4");
        joints[4] = transform.Find("Joint5");
        joints[5] = transform.Find("Joint6");
        joints[6] = transform.Find("Joint7");
        joints[7] = transform.Find("Joint8");
        joints[8] = transform.Find("Joint9");
        joints[9] = transform.Find("Joint10");
        joints[10] = transform.Find("Joint11");
    }

    void Update()
    {
    }

    public void FollowUp(float[] angles)
    {
        for (int i = 0; i < 11; i++)
        {
            joints[i].Rotate(0f, angles[i + 1], 0f);
        }
    }

    public void FollowUp2(Vector3[] rotations)
    {
        for (int i = 0; i < 11; i++)
        {
            joints[i].Rotate(rotations[i + 1]);
        }
    }

    /*
  * 
     * 
     *     const float DELAY_INTERVAL = 0.05f;
    const float ROTATION_UNIT = 1f;
    float DelayAccumulation = 0f;

    Transform joint1, joint2, joint3, joint4, joint5, joint6, joint7, joint8, joint9, joint10, joint11;
    float rotation1, rotation2, rotation3, rotation4, rotation5, rotation6, rotation7, rotation8, rotation9, rotation10, rotation11;
    float rotationTemp_1, rotationTemp_2, rotationTemp_3, rotationTemp_4, rotationTemp_5, rotationTemp_6, rotationTemp_7, rotationTemp_8, rotationTemp_9, rotationTemp_10, rotationTemp_11;
    List<float> RotationPool = new List<float>();
     * 
 private void FollowUp2()
 {
     float delta = Time.smoothDeltaTime;
     float portion = delta / DELAY_INTERVAL;

     joint1.Rotate(Vector3.up * rotation1 * portion);
     joint2.Rotate(Vector3.up * rotation2 * portion);
     joint3.Rotate(Vector3.up * rotation3 * portion);
     joint4.Rotate(Vector3.up * rotation4 * portion);
     joint5.Rotate(Vector3.up * rotation5 * portion);
     joint6.Rotate(Vector3.up * rotation6 * portion);
     joint7.Rotate(Vector3.up * rotation7 * portion);
     joint8.Rotate(Vector3.up * rotation8 * portion);
     joint9.Rotate(Vector3.up * rotation9 * portion);
     joint10.Rotate(Vector3.up * rotation10 * portion);
     joint11.Rotate(Vector3.up * rotation11 * portion);

     rotation1 -= rotation1 * portion;
     rotation2 -= rotation2 * portion;
     rotation3 -= rotation3 * portion;
     rotation4 -= rotation4 * portion;
     rotation5 -= rotation5 * portion;
     rotation6 -= rotation6 * portion;
     rotation7 -= rotation7 * portion;
     rotation8 -= rotation8 * portion;
     rotation9 -= rotation9 * portion;
     rotation10 -= rotation10 * portion;
     rotation11 -= rotation11 * portion;

     DelayAccumulation += delta;

     if (DelayAccumulation >= DELAY_INTERVAL)
     {
         DelayAccumulation = 0f;

         rotation11 = rotationTemp_11 = rotationTemp_10;
         rotation10 = rotationTemp_10 = rotationTemp_9;
         rotation9 = rotationTemp_9 = rotationTemp_8;
         rotation8 = rotationTemp_8 = rotationTemp_7;
         rotation7 = rotationTemp_7 = rotationTemp_6;
         rotation6 = rotationTemp_6 = rotationTemp_5;
         rotation5 = rotationTemp_5 = rotationTemp_4;
         rotation4 = rotationTemp_4 = rotationTemp_3;
         rotation3 = rotationTemp_3 = rotationTemp_2;
         rotation2 = rotationTemp_2 = rotationTemp_1;
         rotation1 = rotationTemp_1 = GetNextRotation();
     }
 }

 private float GetNextRotation()
 {
     if (RotationPool.Count == 0)
     {
         int intervalSpan = Random.Range(3, 9);
         int upOrDown = Random.Range(0, 2);
         for (int i = 0; i < intervalSpan; i++)
         {
             RotationPool.Add((upOrDown == 0 ? 1f : -1f) * ROTATION_UNIT);
         }
     }

     float result = RotationPool[0];
     RotationPool.RemoveAt(0);
     return result;
 }

 
     float RotationSpeed = 1f;
 float FollowUpSpeed = 0.1f; 
    
 void Update()
 {
     if (Input.GetKeyDown(KeyCode.RightArrow))
     {
         RotateTail(true);
     }
     if (Input.GetKeyDown(KeyCode.LeftArrow))
     {
         RotateTail(false);
     }

     FollowUp();
 }

 private void RotateTail(bool clockwise)
 {
     rotation1 += (clockwise ? 1 : -1) * RotationSpeed;
     joint1.Rotate((clockwise ? Vector3.up : Vector3.down) * RotationSpeed);
 }
    
 private void RotateTail(bool clockwise)
 {
     joint1.Rotate((clockwise ? Vector3.up : Vector3.down) * RotationSpeed);
     joint2.Rotate((clockwise ? Vector3.up : Vector3.down) * RotationSpeed * 0.8f);
     joint3.Rotate((clockwise ? Vector3.up : Vector3.down) * RotationSpeed * 0.6f);
     joint4.Rotate((clockwise ? Vector3.up : Vector3.down) * RotationSpeed * 0.4f);
     joint5.Rotate((clockwise ? Vector3.up : Vector3.down) * RotationSpeed * 0.2f);
     joint6.Rotate((clockwise ? Vector3.up : Vector3.down) * RotationSpeed * 0.0f);

     rotation2 += (clockwise ? 1 : -1) * RotationSpeed * 0.2f;
     rotation3 += (clockwise ? 1 : -1) * RotationSpeed * 0.4f;
     rotation4 += (clockwise ? 1 : -1) * RotationSpeed * 0.6f;
     rotation5 += (clockwise ? 1 : -1) * RotationSpeed * 0.8f;
     rotation6 += (clockwise ? 1 : -1) * RotationSpeed * 1.0f;
 }
     
 private void FollowUp()
 {
     float delta = Time.time;

     joint2.Rotate(Vector3.up * FollowUpSpeed * rotation2 * delta);
     joint3.Rotate(Vector3.up * FollowUpSpeed * rotation3 * delta);
     joint4.Rotate(Vector3.up * FollowUpSpeed * rotation4 * delta);
     joint5.Rotate(Vector3.up * FollowUpSpeed * rotation5 * delta);
     joint6.Rotate(Vector3.up * FollowUpSpeed * rotation6 * delta);

     rotation2 -= FollowUpSpeed * rotation2 * delta;
     rotation3 -= FollowUpSpeed * rotation3 * delta;
     rotation4 -= FollowUpSpeed * rotation4 * delta;
     rotation5 -= FollowUpSpeed * rotation5 * delta;
     rotation6 -= FollowUpSpeed * rotation6 * delta;
 }

  * 
  * 
  */

}
