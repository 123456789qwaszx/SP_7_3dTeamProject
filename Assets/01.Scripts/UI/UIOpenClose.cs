using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIOpenClose : MonoBehaviour
{
    public GameObject inven;
    public GameObject option;
    public GameObject gameOver;
    bool isActive = false;


    void Start()
    {

        // if (inven == null)
        //     inven = GameObject.Find("UI_Inventory");

        if (option == null)
        {
            GameObject foundInven = GameObject.Find("Option");
            if (foundInven != null)
            {
                option = foundInven;
            }
            else
                Debug.LogWarning("Option 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && gameOver != null)
        {
            OCInven();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameOver != null)
        {
            if (!BuildManager.Instance.isPreviewActivated)
            {
                OCOption();
            }
        }
    }

    public void OCInven()
    {
        isActive = !isActive;
        inven.SetActive(!inven.activeSelf);

        if (isActive) // 카메라 제어만.
        {
            GameManager.Instance.DisableGameCamLook();
        }
        else
        {
            GameManager.Instance.EnableGameCamLook();
        }
    }

    // 옵션 버튼 누르면 작동:
    public void OCOption()
    {
        if (option != null) // 카메라 제어 + 시간 정지까지.
        {
            option.SetActive(!option.activeSelf);
            GameManager.Instance.TogglePause();
        }
    }

    public void OCGameOver()
    {
        if (gameOver != null) // UI호출만.
        {
            Debug.Log("UI gameover 메서드 작동");
            gameOver.SetActive(!gameOver.activeSelf);
        }
        else
        {
            Debug.LogWarning("GameOver UI를 찾을 수 없습니다.");
        }
    }
}
