using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EularFix : MonoBehaviour
{
    public float lockedXAngle = 0f;

    void LateUpdate()
    {
        Vector3 currentAngles = transform.rotation.eulerAngles;
        currentAngles.x = lockedXAngle;
        transform.rotation = Quaternion.Euler(currentAngles);
    }
}
