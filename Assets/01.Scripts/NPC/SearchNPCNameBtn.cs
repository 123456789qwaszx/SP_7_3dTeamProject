using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SearchNPCNameBtn : MonoBehaviour
{
    public GameObject MarkNPCName;
    public GameObject NPCTextBoard;
    public GameObject NPCText;
    public GameObject NPCName;


    public void ButtonClick()
    {
       

        // 텍스트 보드 활성화
        NPCTextBoard.SetActive(true);

        // 버튼들 제거
        foreach (Transform child in MarkNPCName.transform)
        {
            Destroy(child.gameObject);
        }
        // 클릭된 버튼 이름 텍스트로 설정
        string clickedName = EventSystem.current.currentSelectedGameObject.name;
        NPCText.GetComponentInChildren<TextMeshProUGUI>().text = "안녕?";
        NPCName.GetComponentInChildren<TextMeshProUGUI>().text = clickedName;
        Button btn = NPCTextBoard.GetComponentInChildren<Button>(); //NPCTextBoard에 버튼 기능을 가져옴
        btn.onClick.Invoke(); //누름



    }
}
