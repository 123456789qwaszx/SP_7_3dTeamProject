using UnityEngine;

public class EularFix : MonoBehaviour
{
    public float lockedXAngle = 0f; // 고정할 회전값
    public float lockedHeight = 0f; // 고정할 높이(Y값)

    void LateUpdate()
    {
        Vector3 currentAngles = transform.rotation.eulerAngles;
        currentAngles.x = lockedXAngle;
        transform.rotation = Quaternion.Euler(currentAngles);

        Vector3 currentPos = transform.position;
        currentPos.y = lockedHeight;
        transform.position = currentPos;
    }
}
