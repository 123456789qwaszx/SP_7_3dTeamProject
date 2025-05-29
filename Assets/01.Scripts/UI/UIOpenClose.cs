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
        if (Input.GetKeyDown(KeyCode.I))
        {
            OCInven();
        }
    }

    public void OCInven()
    {
        if (!isActive)
        {
            isActive = true;
            inven.SetActive(!inven.activeSelf);
            GameManager.Instance.DisableGameCamLook();
        }
        else
        {
            isActive = false;
            inven.SetActive(!inven.activeSelf);
            GameManager.Instance.EnableGameCamLook();
        }
    }

    // 옵션 버튼 누르면 작동:
    public void OCOption()
    {
        Debug.Log(option);

        if (option != null)
        {
            option.SetActive(!option.activeSelf); // 현재 상태의 반대로 토글
            GameManager.Instance.TogglePause();
        }
    }

    public void OCGameOver()
    {
        if (gameOver != null)
        {
            gameOver.SetActive(!option.activeSelf); // 현재 상태의 반대로 토글
        }
        else
        {
            Debug.LogWarning("GameOver UI를 찾을 수 없습니다.");
        }
    }
}
