using UnityEngine;

public class EularFix : MonoBehaviour
{
    // 모델링마다 회전값과 높이 조정이 필요한 경우 사용.
    // 사용법: 오브젝트에 넣어서 X 회전값과 높이를 조정하면 됨.
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
