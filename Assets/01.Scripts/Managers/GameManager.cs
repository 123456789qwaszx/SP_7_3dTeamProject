using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//싱글톤 상속하면서 GameManger 오브젝트를 생성함(UI를 작동해야 생성됨.)
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerInput playerInput; //카메라 기능을 일시 비활성할 변수
    bool isPause = false; // 인게임 정지 상태를 확인하는 변수
    bool isGameOver = false; // 게임오버 상태를 확인하는 변수

    public void GameStart()
    {
        SceneManager.LoadScene("YJHTestScene");
    }

    public void Retry()
    {
#if UNITY_EDITOR
        Debug.Log("게임 재시작 (에디터)");
#endif
        // 현재 씬을 다시 로드하여 게임 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void Title()
    {
        SceneManager.LoadScene("YJHTestTitle");
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        // 에디터 환경에서는 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#elif DEVELOPMENT_BUILD
        // 개발 빌드에서는 로그 출력 후 종료
        Debug.Log("게임 종료 (개발 빌드)");
        Application.Quit();
#else
        // 릴리즈 빌드에서는 바로 종료
        Application.Quit();
#endif
    }

    //씬 로드하면서 PlayerInput 추가 재할당.
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }


    private void Start()
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
        if (isGameOver) return; // 재시작 시 리턴하면서 

        isGameOver = true;
        // Time.timeScale = 0; //인게임 정지
        // 재시작 시 

        TogglePause(); // 게임 오버시 인게임 정지
    }
}