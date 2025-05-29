using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerInput playerInput; //카메라 기능을 일시 비활성할 변수
    bool isPause = false; // 인게임 정지 상태를 확인하는 변수
    bool isGameOver = false; // 게임오버 상태를 확인하는 변수

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogWarning("Player를 찾을 수 없음.(<< GameManager)");
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

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0;

        if (UIManager.Instance != null)
        {
            
        }
    }
}