using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//싱글톤 상속하면서 GameManger 오브젝트를 생성함(UI를 작동해야 생성됨.)
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerInput playerInput; //카메라 기능을 일시 비활성할 변수
    bool isPause = false; // 인게임 정지 상태를 확인하는 변수
    bool isGameOver = false; // 게임오버 상태를 확인하는 변수

    //
    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>(); // 인게임 생성으로 자동으로 PlayerInput을 찾아 등록.
        //다만, 게임 재시작 시 플레이어 오브젝트를 다시 못찾는 버그
        Cursor.lockState = CursorLockMode.Confined;

        if (playerInput == null)
        {
            Debug.LogWarning("Player를 찾을 수 없음.(<< GameManager)");
        }
    }


    //UI 작동 시 카메라 움직임이나 인게임 시간 제어.
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

    public bool TogglePause() // 인게임 정지상태에 따라 카메라 움직임도 같이 제어.
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
        Time.timeScale = 0; //인게임 정지

        TogglePause(); // 게임 오버시 인게임 정지
    }
}