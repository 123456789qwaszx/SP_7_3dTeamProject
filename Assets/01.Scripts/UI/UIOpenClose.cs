using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIOpenClose : MonoBehaviour
{
    public GameObject inven;
    public GameObject option;
    public GameObject gameOver;

    public void OCInven()
    {
        {
            if (inven != null)
            {
                inven.SetActive(!inven.activeSelf); // 현재 상태의 반대로 토글
            }
        }
    }
    public void OCOption()
    {
        {
            Debug.Log(option);
            if (option != null)
            {
                option.SetActive(!option.activeSelf); // 현재 상태의 반대로 토글
            }
        }
    }
    public void OCGameOver()
    {
        if (gameOver != null)
        {
            gameOver.SetActive(gameOver);
        }
        else
        {
            Debug.LogWarning("GameOver UI를 찾을 수 없습니다.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        if (inven == null)
            inven = GameObject.Find("UI_Inventory");

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

    // Update is called once per frame
    void Update()
    {

    }
}
