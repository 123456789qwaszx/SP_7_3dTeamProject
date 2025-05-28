using UnityEngine;

// 빌드관련 UI

[System.Serializable]
public class Building
{
    public string buildName; //이름
    public GameObject go_Prefabs; // 설치물 프리팹
    public GameObject go_PreviewPrefabs; // 미리보기 프리팹
}

public class BuildManual : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI; // 건축 UI
    [SerializeField] private Building[] builds; // 설치물 저장
    private bool isActivated = false; // UI 활성 상태

    // [SerializeField] private BuildData[] buildList; // 건축물(SO) 리스트
    // go_Preview, go_Prefab, tf_PlayerCam, isPreviewActivated

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !BuildManager.Instance.isPreviewActivated)
        {
            Window();
            Debug.Log("Tab키 작동");
        }
    }

    public void SlotClick(int _slotNumber)
    {
        BuildManager.Instance.SelectedBuilding(builds[_slotNumber]);
    }

    private void OnEnable()
    {
        if (BuildManager.Instance != null)
        {
            BuildManager.Instance.OnCancelBuild += CloseWindow;
        }
    }

    private void OnDisable()
    {
        if (BuildManager.Instance != null)
        {
            BuildManager.Instance.OnCancelBuild -= CloseWindow;
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

    private void OpenWindow() // 제작탭 열기
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow() // 제작탭 닫기
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}
