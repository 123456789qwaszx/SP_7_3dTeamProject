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
    private bool isActivated = false; // UI 활성 상태

    [SerializeField] private BuildData[] buildList; // 건축물(SO) 리스트

    void Update()
    {
        // 미리보기 프리팹 비활성 상태일 때 Tab키를 누르면 제작 메뉴 작동.
        if (Input.GetKeyDown(KeyCode.Tab) && !BuildManager.Instance.isPreviewActivated)
        {
            Window();
            Debug.Log("Tab키 작동");
        }
    }

    public void SlotClick(int _slotNumber)
    {
        BuildManager.Instance.SelectedBuilding(buildList[_slotNumber]);
        CloseWindow();
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
        GameManager.Instance.DisableGameCamLook(); // 카메라 잠금
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow() // 제작탭 닫기
    {
        GameManager.Instance.EnableGameCamLook(); // 카메라 잠금 해제
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}
