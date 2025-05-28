using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    //충돌한 오브젝트의 정보가 담기는 리스트.
    private List<Collider> colliderList = new List<Collider>();
    [SerializeField] private int layerGround; //땅 레이어(설치 가능)
    private const int IGNORE_RAYCAST_LAYER = 2; // 무시할 레이어

    //충돌 시 설치 불가를 시각적으로 나타낼 변수.
    [SerializeField] private Material green;
    [SerializeField] private Material red;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (colliderList.Count > 0) {
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
            var newMaterials = new Material[tf_Obj.GetComponent<Renderer>().materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mat;
            }

            tf_Obj.GetComponent<Renderer>().materials = newMaterials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ground(설치 가능)
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            colliderList.Remove(other);
        }
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }
}
