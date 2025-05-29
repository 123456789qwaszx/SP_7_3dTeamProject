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
        Debug.Log("인벤토리 작동");

        // inven.SetActive(!inven.activeSelf); // 현재 상태의 반대로 토글

        if (!isActive)
        {
            Debug.Log("인벤 단축키 입력 확인");
            isActive = true;
            inven.SetActive(true);
            GameManager.Instance.DisableGameCamLook();
        }
        else
        {
            Debug.Log("인벤 단축키 취소 확인");
            isActive = false;
            inven.SetActive(!inven.activeSelf);
            GameManager.Instance.EnableGameCamLook();
        }
        
    }

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
