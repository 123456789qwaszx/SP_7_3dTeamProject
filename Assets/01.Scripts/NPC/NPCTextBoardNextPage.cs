using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCTextBoardNextPage : MonoBehaviour
{
    public GameObject NPCTextBoard;
    public GameObject NPCText;
    public GameObject NPCName;

    private int i = 0;

    private void Start()
    {
        i = 0;
        NextPage();
    }

    public void PageClick()
    {
        i++;
        NextPage();
    }

    private void Update()
    {
        if (NPCTextBoard.activeSelf)//아무 키나 누르면 종료
        {
            if (Input.anyKeyDown &&
                !Input.GetMouseButtonDown(0) &&
                !Input.GetMouseButtonDown(1) &&
                !Input.GetMouseButtonDown(2))
            {
                CloseDialogue();
            }
        }
    }

    private void NextPage()
    {
        var textComponent = NPCText.GetComponentInChildren<TextMeshProUGUI>();//대사창 가져오기
        string npcName = NPCName.GetComponentInChildren<TextMeshProUGUI>().text;//NPC 이름
        string newText = "";//대사창에 출력할 대사

        if (npcName == "Apple")
        {
            if (i == 1) newText = "나는 Apple이야.";
            else if (i == 2) newText = "반가워!";
            else CloseDialogue();
        }
        else if (npcName == "Apple(1)")
        {
            if (i == 1) newText = "Apple?";
            else if (i == 2) newText = "어";
            else CloseDialogue();
        }
        else
        {
            if (i == 1) newText = "대사 없음";
             else CloseDialogue();
        }

        // 텍스트 실제 반영
        textComponent.text = newText;
    }

    private void CloseDialogue()
    {
        NPCTextBoard.SetActive(false);
        i = 0;
    }
}