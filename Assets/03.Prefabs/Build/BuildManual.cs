using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
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

    private GameObject go_Preview; // 미리보기 프리팹을 담을 변수수
    private GameObject go_Prefab; // 실제 생성 프리팹을 담을 변수
    [SerializeField] private Transform tf_Player; // Player 위치에 생성

    private RaycastHit hitInfo;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayRange;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            Window();
            Debug.Log("Tab키 작동 중");
        }

        if (isPreviewActivated)
        {
            PreviewPosUpdate();
        }

        if (Input.GetButtonDown("@마우스 좌클릭"))
        {
            Build();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void Build()
    {
        if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        {
            Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
            Destroy(go_Preview);

            isActivated = false;
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
        }
    }

    private void Window()
    {
        if (!isActivated) OpenWindow();
        else CloseWindow();
    }

    private void PreviewPosUpdate()
    {
        if (Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, rayRange, layerMask))
        {
            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                go_Preview.transform.position = _location;
            }
        }
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

    public void SlotClick(int _slotNumber)
    {
        go_Preview = Instantiate(
            builds[_slotNumber].go_PreviewPrefabs,
            tf_Player.forward,
            Quaternion.identity
        );
        go_Prefab = builds[_slotNumber].go_Prefabs;

        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }
}
