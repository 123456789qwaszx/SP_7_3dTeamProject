using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // UI 작동 시 카메라 잠금 혹은 게임 일시 정시 기능
    // 테스트 1: 플레이어 인풋시스템(카메라)를 잠금. 작동 확인
    // 테스트 2: 게임오버, 옵션 창 활성화 시 인게임 일시 정지.

    public static UIManager Instance { get; private set; }
    [SerializeField] private PlayerInput playerInput; //카메라 기능을 일시 비활성할 변수
    bool isPause = false; // 인게임 정지 상태를 확인하는 변수


    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void DisableGameCamLook() // 카메라 움직임 비활성화
    {
        if (playerInput != null)
        {
            playerInput.actions["Look"].Disable();
        }
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableGameCamLook() // 카메라 움직임 활성화
    {
        if (playerInput != null)
        {
            playerInput.actions["Look"].Enable();
        }
    }

    public bool TogglePause() // 일시 정지
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;

        if (isPause)
        {
            DisableGameCamLook();
        }
        else
        {
            EnableGameCamLook();
        }
        
        return isPause;
    }
}
