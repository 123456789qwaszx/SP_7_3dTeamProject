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
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.DisableGameCamLook();
        }

        // Alt 키를 떼면 커서 비활성화
        if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.EnableGameCamLook();
        }
    }

    public void OCInven()
    {
        if ((option != null && option.activeSelf) || (gameOver != null && gameOver.activeSelf))
        {
            Debug.Log("다른 UI가 열려 있어서 인벤토리를 열 수 없습니다.");
            return;
        }
        
        if (!isActive)
        {
            isActive = !isActive;
        }

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
        if ((inven != null && inven.activeSelf) || (gameOver != null && gameOver.activeSelf))
        {
            Debug.Log("다른 UI가 열려 있어서 옵션을 열 수 없습니다.");
            return;
        }

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
