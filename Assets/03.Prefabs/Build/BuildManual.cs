
using UnityEngine;

[System.Serializable] // 직렬화
public class Building
{
    public string buildName; //이름
    public GameObject go_Prefabs; // 설치물
    public GameObject go_PreviewPrefabs; // 미리보기
}

public class BuildManual : MonoBehaviour
{
    private bool isActivated = false;
    private bool isPreviewActivated = false;
    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private Building[] builds; // 설치물 저장

    private GameObject go_Preview; // 미리보기 프리팹을 담을 변수
    private GameObject go_Prefab; // 실제 생성 프리팹을 담을 변수
    [SerializeField] private Transform tf_Player; // Player 위치에 생성

    private RaycastHit hitInfo;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayRange;

    // [SerializeField] private BuildData[] buildList; // 건축물(SO) 리스트

    public void SlotClick(int _slotNumber)
    {
        go_Preview = Instantiate(
            builds[_slotNumber].go_PreviewPrefabs,
            tf_Player.position + tf_Player.forward,
            Quaternion.identity
        );

        go_Prefab = builds[_slotNumber].go_Prefabs; // 테스트용

        // if (_slotNumber < buildList.Length)
        // {
        //     BuildData selectedBuilding = buildList[_slotNumber];

        //     go_Preview = Instantiate(
        //         selectedBuilding.previewPrefab,
        //         tf_Player.position + tf_Player.forward,
        //         Quaternion.identity
        //     );
        // }

        isPreviewActivated = true; // 미리보기 활성화 상태
        go_BaseUI.SetActive(false); // 제작탭 종료
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            Window();
            Debug.Log("Tab키 작동");
        }

        if (isPreviewActivated)
        {
            PreviewPosUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isPreviewActivated)
        {
            Debug.Log("마우스 클릭: 건설 시작");
            Build();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void Build()
    {
        Quaternion lockRot = Quaternion.Euler(0, tf_Player.rotation.eulerAngles.y, 0); // 건축물 회전 잠금

        if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        {
            if (go_Prefab != null)
            {
                Instantiate(go_Prefab, hitInfo.point, lockRot);
                Destroy(go_Preview);
            }
            else
            {
                Debug.LogError("오류: go_Prefab >> null");
            }

            isActivated = false;
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
        }
    }

    private void Window()
    {
        if (!isActivated)
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();
        }
    }

    private void PreviewPosUpdate()
    {
        if (Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, rayRange, layerMask))
        {

            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                go_Preview.transform.position = _location;
                go_Preview.transform.rotation = Quaternion.Euler(0, tf_Player.rotation.eulerAngles.y, 0); // 미리보기 회전 잠금
            }
        }
    }

    private void Cancel() // 제작탭 지우기
    {
        if (isPreviewActivated)
        {
            Destroy(go_Preview);
        }

        isActivated = false;
        isPreviewActivated = false;
        go_Preview = null;
        go_Prefab = null;

        go_BaseUI.SetActive(false);
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }

}
