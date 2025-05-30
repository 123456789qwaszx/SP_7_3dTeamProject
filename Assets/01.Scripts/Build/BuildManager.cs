using System;
using UnityEngine;

// 미리보기 및 생성 
public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    public bool isPreviewActivated = false; // 미리보기 상태: BuildManual에서 사용하기 위해 전체 접근으로.
    private GameObject go_Preview; // 미리보기 프리팹을 담을 변수
    private GameObject go_Prefab; // 실제 생성 프리팹을 담을 변수

    [SerializeField] private Transform tf_PlayerCam; // Player Camera 위치로 생성

    // Ray로 설치 지점을 지정.
    private RaycastHit hitInfo;
    [SerializeField] private LayerMask layerMask; // 설치 가능or불가능 필터링할 레이어
    [SerializeField] private float rayRange;

    // 강언덕 추가
    // 아마 직접 연결해줘야할듯
    public ItemSlot[] _slots;
    [SerializeField]
    BuildData _selectedBuildData;
    
    bool canBuild = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        _selectedBuildData = Managers.Player.Player.SelectedBuildData;
        
        if (isPreviewActivated)
        {
            PreviewPosUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isPreviewActivated)
        {
            Build(_selectedBuildData, _slots);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPreviewActivated)
        {
            Cancel();
        }
    }

    public void SelectedBuilding(BuildData buildList)
    {
        go_Preview = Instantiate(
            buildList.previewPrefab,
            
            tf_PlayerCam.position + tf_PlayerCam.forward,
            Quaternion.identity
        );

        go_Prefab = buildList.prefab;
        isPreviewActivated = true;
    }
    private void Build(BuildData buildData, ItemSlot[] slots)
    {
        Quaternion lockRot = Quaternion.Euler(0, tf_PlayerCam.rotation.eulerAngles.y, 0); // 건축물 회전 잠금
        
        ResourceType costType = buildData.costType;
        int needCost= buildData.cost;

        for (int i = 0; i < slots.Length; i++)
        {
            ResourceType InvenItemType = slots[i].resourceType;
            int capacity = slots[i].quantity;
            Debug.Log($"가지고있는타입{InvenItemType}" );
            Debug.Log($"소모자원{costType}" );
            Debug.Log($"인벤수량{capacity}" );
            Debug.Log($"필요한양{needCost}");

            if (InvenItemType == costType && needCost < capacity)
            {
                canBuild = true;
            }
        }
        //미리보기 활성화 되고 미리보기 오브젝트가 설치 가능한 상태일 때
        if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().IsBuildable() && canBuild)
            {
                Debug.Log("건축 완료");

                Instantiate(go_Prefab, hitInfo.point, lockRot);
                Destroy(go_Preview);

                isPreviewActivated = false;
                go_Preview = null;
                go_Prefab = null;

            }
        else if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().IsBuildable())
        {
            Debug.Log("건축 불가: 자원 부족");
        }
        else
        {
            Debug.Log("건축 불가: 장애물 확인");
        }
    }

    private void PreviewPosUpdate()
    {
        if (Physics.Raycast(tf_PlayerCam.position, tf_PlayerCam.forward, out hitInfo, rayRange, layerMask))
        {

            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                go_Preview.transform.position = _location;
                go_Preview.transform.rotation = Quaternion.Euler(0, tf_PlayerCam.rotation.eulerAngles.y, 0); // 미리보기 회전 잠금
            }
        }
    }

    private void Cancel() // 미리보기 상태 취소
    {
        if (isPreviewActivated)
        {
            Destroy(go_Preview);
        }

        isPreviewActivated = false;
        go_Preview = null;
        go_Prefab = null;
    }
}
