using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIOpenClose : MonoBehaviour
{
    public GameObject inven;
    public GameObject option;

    public void OCInven()
    {
        {
            if (inven != null)
            {
                inven.SetActive(!inven.activeSelf); // ���� ������ �ݴ�� ���
            }
        }
    }
    public void OCOption()
    {
        {
            Debug.Log(option);
            if (option != null)
            {
                option.SetActive(!option.activeSelf); // ���� ������ �ݴ�� ���
            }
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
                Debug.LogWarning("Option ã�� �� �����ϴ�.");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
