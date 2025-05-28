using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    //충돌한 오브젝트의 정보가 담기는 리스트.
    private List<Collider> colliderList = new List<Collider>();
    [SerializeField] private int layerGround; //설치 가능한 레이어 넘버.
    private const int IGNORE_RAYCAST_LAYER = 2; // 무시할 레이어

    //충돌 시 설치 불가를 시각적으로 나타낼 변수.
    [SerializeField] private Material green;
    [SerializeField] private Material red;


    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (colliderList.Count > 0) {
            Debug.Log("설치 불가: 레드");
            SetColor(red);
        }
        else {
            SetColor(green);
        }
    }

    private void SetColor(Material mat) // 자식 렌더러에서 material 컬러를 불러오고 변경하는 함수. >> 그런데 현재 사용하는 모델링에는 자식 컴포넌트가 없음.
    {
        foreach (Transform tf_Obj in this.transform)
        {
            Debug.Log("설치 불가: 머테리얼 감지");
            var newMaterials = new Material[tf_Obj.GetComponent<Renderer>().materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                Debug.Log("머테리얼 변경 중");
                newMaterials[i] = mat;
            }
            Debug.Log("변경 완료.");
            tf_Obj.GetComponent<Renderer>().materials = newMaterials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌 확인: 건축 불가");
        // Ground(설치 가능)
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("충돌 없음: 건축 가능");
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Remove(other);
        }
    }

    public bool IsBuildable()
    {
        return colliderList.Count == 0;
    }
}
