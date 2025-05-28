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
        if (colliderList.Count > 0)
        {
            SetColor(red);
        }
        else
        {
            SetColor(green);
        }
    }

    private void SetColor(Material mat) // 장애물에 따라 material 컬러를 변경하는 함수.
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material[] newMaterials = new Material[renderer.materials.Length]; // 미리보기 프리팹은 단일 오브젝트 
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mat;
            }
            renderer.materials = newMaterials;
        }
    }

    // 미리보기 프리팹을 통해 설치 가능여부 판정.
    // 충돌 리스트에 장애물을 저장하고 리스트가 비어있으면 건축이 가능하게 만드는 로직.

    private void OnTriggerEnter(Collider other) // 장애물에 닿으면 작동
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other) // 장애물에서 떨어지면 작동.
    {
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
