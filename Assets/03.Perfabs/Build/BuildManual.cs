using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[System.Serializable] // 직렬화
public class Build
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
    [SerializeField] private Build[] builds; // 설치물 저장

    private GameObject go_Preview; // 미리보기 프리팹을 담을 변수수
    [SerializeField] private Transform tf_Player;

    public void SlotClick(int _slotNumber)
    {
        go_Preview = Instantiate(
            builds[_slotNumber].go_PreviewPrefabs,
            tf_Player.forward,
            Quaternion.identity
        );

        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Window();
            Debug.Log("Tab키 작동 중");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void Window()
    {
        if (!isActivated) OpenWindow();
        else CloseWindow();
    }

    private void Cancel()
    {
        if (isPreviewActivated)
        {
            Destroy(go_Preview);
        }

        isActivated = false;
        isPreviewActivated = false;

        go_Preview = null;
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
