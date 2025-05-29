using System.Collections;

using UnityEngine;
using DG.Tweening;
using TMPro;

public class NPCTextBoardNextPage : MonoBehaviour
{
    public GameObject NPCTextBoard;// 대화창 오브젝트 (전체 UI 박스)
    public GameObject NPCText;// 대사 텍스트 오브젝트
    public GameObject NPCName;// NPC 이름이 출력되는 텍스트 오브젝트
    public int i = 0; // 현재 대화 페이지

    private TextMeshProUGUI textComponent;// TextMeshPro 컴포넌트 참조 (대사 텍스트 출력용)
    private Tween typingTween;// 현재 재생 중인 타이핑 애니메이션 Tween

    private void Start()// 시작 시 초기화 및 첫 대사 출력
    {
        i = 0;
        textComponent = NPCText.GetComponentInChildren<TextMeshProUGUI>();
        NextPage();
    }
    public void PageClick() // 클릭 시 페이지 넘김 처리
    {
        // 타이핑 애니메이션이 재생 중이면 바로 전체 텍스트 출력
        if (typingTween != null && typingTween.IsActive() && typingTween.IsPlaying())
        {
            typingTween.Complete();
            return;
        }

        i++;// 다음 대사로 넘어감
        NextPage();
    }

    private void Update()
    {
        // if (NPCTextBoard.activeSelf)// 키 입력으로 대화창 종료 처리
        // {
        //     if (Input.anyKeyDown &&
        //         !Input.GetMouseButtonDown(0) &&
        //         !Input.GetMouseButtonDown(1) &&
        //         !Input.GetMouseButtonDown(2))
        //     {

        //         CloseDialogue();
        //     }
        // }
    }

    private void NextPage()// 다음 대사 출력 처리
    {

        string npcName = NPCName.GetComponentInChildren<TextMeshProUGUI>().text;// 현재 NPC 이름 텍스트 추출
        string newText = "";

        // NPC 이름별로 대사 구성
        if (npcName == "Apple") //NPC Apple
        {
            if (i == 1) newText = "나는 Apple이야.";
            else if (i == 2) newText = "반가워!";
            else { CloseDialogue(); return;
            
            }
        }
        else if (npcName == "Apple(1)")//NPC Apple(1)
        {
            if (i == 1) newText = "Apple?";
            else if (i == 2) newText = "어";
            else { CloseDialogue(); return; }
        }
        else //정해져 있지 않을 경우
        {
            if (i == 1) newText = "대사 없음";
            else { CloseDialogue(); return; }
        }

        // 기존 애니메이션이 있다면 제거
        if (typingTween != null && typingTween.IsActive())
        {
            typingTween.Kill();
        }

        // DOTween을 이용한 타이핑 효과
        textComponent.text = newText;
        textComponent.maxVisibleCharacters = 0;
        typingTween = DOTween.To(() => textComponent.maxVisibleCharacters,
                                 x => textComponent.maxVisibleCharacters = (int)x,newText.Length,0.5f).SetEase(Ease.Linear);// 출력 시간 조절
    }

    public void CloseDialogue()// 대화 종료 처리
    {
        
        if (typingTween != null && typingTween.IsActive()) // 애니메이션 제거
        {
            typingTween.Kill();
        }
        i = 0;
        NPCTextBoard.SetActive(false); // 대화창 비활성화 및 페이지 인덱스 초기화
    }
}