using System.Collections;

using UnityEngine;
using DG.Tweening;
using TMPro;

public class NPCTextBoardNextPage : MonoBehaviour
{
    public GameObject NPCTextBoard; // 대화창 오브젝트 (전체 UI 박스)
    public GameObject NPCText;      // 대사 텍스트 오브젝트
    public GameObject NPCName;      // NPC 이름 텍스트 오브젝트
    public int i = 0;               // 현재 대화 페이지 인덱스

    private TextMeshProUGUI textComponent; // 대사 출력용 TMP 컴포넌트
    private Tween typingTween;             // 타이핑 애니메이션 Tween

    private void Start()
    {
        i = 0;
        textComponent = NPCText.GetComponentInChildren<TextMeshProUGUI>();
        NextPage();
    }

    private void Update()
    {
        if (NPCTextBoard.activeSelf)
        {
            // 마우스 클릭 또는 스페이스바로 페이지 넘기기
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                PageClick();
            }
        }
    }

    public void PageClick()
    {
        // 타이핑 애니메이션이 진행 중이면 즉시 완료
        if (typingTween != null && typingTween.IsActive() && typingTween.IsPlaying())
        {
            typingTween.Complete();
            return;
        }

        i++; // 다음 대사로 이동
        NextPage();
    }

    private void NextPage()
    {
        if (i == 0)
        {
            SetDialogueMode(true); // 대화 시작 시 설정
        }

        string npcName = NPCName.GetComponentInChildren<TextMeshProUGUI>().text;
        string newText = "";

        // NPC별 대사 정의
        if (npcName == "Apple")
        {
            if (i == 1) newText = "나는 Apple이야.";
            else if (i == 2) newText = "반가워!";
            else { CloseDialogue(); return; }
        }
        else if (npcName == "Apple(1)")
        {
            if (i == 1) newText = "Apple?";
            else if (i == 2) newText = "어";
            else { CloseDialogue(); return; }
        }
        else // 정의되지 않은 NPC
        {
            if (i == 1) newText = "대사 없음";
            else { CloseDialogue(); return; }
        }

        // 기존 애니메이션 제거
        if (typingTween != null && typingTween.IsActive())
        {
            typingTween.Kill();
        }

        // DOTween을 이용한 타이핑 효과 적용
        textComponent.text = newText;
        textComponent.maxVisibleCharacters = 0;
        typingTween = DOTween.To(() => textComponent.maxVisibleCharacters,
                                 x => textComponent.maxVisibleCharacters = (int)x,
                                 newText.Length, 0.5f)
                             .SetEase(Ease.Linear);
    }

    public void CloseDialogue()
    {
        if (typingTween != null && typingTween.IsActive())
        {
            typingTween.Kill();
        }

        i = 0;
        NPCTextBoard.SetActive(false);
        SetDialogueMode(false); // 대화 종료 시 설정 복구
    }

    private void SetDialogueMode(bool isDialogue)
    {
        if (isDialogue)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.DisableGameCamLook();
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.EnableGameCamLook();
        }
    }
}