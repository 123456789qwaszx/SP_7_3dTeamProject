using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // UI 작동 시 카메라 잠금 혹은 게임 일시 정시 기능
    // 테스트 1: 플레이어 인풋시스템(카메라)를 잠금.
    public static UIManager Instance { get; private set; }
    [SerializeField] private PlayerInput playerInput; //카메라 기능을 일시 비활성할 변수
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

    public void DisableGameCamLook()
    {
        if (playerInput != null)
        {
            playerInput.actions["Look"].Disable();
        }
        Cursor.lockState = CursorLockMode.None; // 마우스 커서 표시
    }

    public void EnableGameCamLook()
    {
        if (playerInput != null)
        {
            playerInput.actions["Look"].Enable();
        }
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨김
    }
}
