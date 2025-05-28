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
    private bool isActivated = false; //
    
    [SerializeField] private GameObject go_BaseUI; // 건축 UI
    [SerializeField] private Building[] builds; // 설치물 저장

    // [SerializeField] private BuildData[] buildList; // 건축물(SO) 리스트
    // go_Preview, go_Prefab, tf_PlayerCam, isPreviewActivated
    
    public void SlotClick(int _slotNumber)
    {
        go_Preview = Instantiate(
            builds[_slotNumber].go_PreviewPrefabs,
            tf_PlayerCam.position + tf_PlayerCam.forward,
            Quaternion.identity
        );

        go_Prefab = builds[_slotNumber].go_Prefabs; // 테스트용

        //SO 전용
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
