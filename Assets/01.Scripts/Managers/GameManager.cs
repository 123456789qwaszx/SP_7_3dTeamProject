using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//싱글톤 상속하면서 GameManger 오브젝트를 생성함(UI를 작동해야 생성됨.)
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerInput playerInput; //카메라 기능을 일시 비활성할 변수
    [SerializeField] private UIOpenClose uiOpenClose; //UI 활성/비활성을 위한 변수
    bool isPause = false; // 인게임 정지 상태를 확인하는 변수
    public bool isGameOver = false; // 게임오버 상태를 확인하는 변수

    private MonsterSpawner monsterSpawner;
    private void Start()
    {
        uiOpenClose = FindObjectOfType<UIOpenClose>(); // GameManager 인게임 생성
        playerInput = FindObjectOfType<PlayerInput>(); // 자동으로 PlayerInput/UIOpenClose을 찾아 등록.
        monsterSpawner = GetComponent<MonsterSpawner>();
        //EnableGameCamLook();

        if (playerInput == null)
        {
            Debug.LogWarning("Player를 찾을 수 없음.(<< GameManager)");
        }
    }


    //UI 작동 시 카메라 움직임이나 인게임 시간 제어.
    public void DisableGameCamLook() // 카메라 움직임 비활성화 + 커서 보임
    {
        if (playerInput != null)
        {
            playerInput.actions["Look"].Disable();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EnableGameCamLook() // 카메라 움직임 활성화 + 커서 숨김
    {
        if (playerInput != null)
        {
            playerInput.actions["Look"].Enable();
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TogglePause() // 인게임 정지상태에 따라 카메라 움직임도 같이 제어.
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
    }

    public void GameOver() //게임 오버: UI 호출 및 인게임 정지.
    {
        if (isGameOver) return; // 재시작 시 게임 오버 중첩 방지 코드

        isGameOver = true;
        TogglePause(); // 게임 오버시 인게임 정지
        monsterSpawner.ClearAllMonsters();
        Debug.Log($"isPause: {isPause}, isGameOver: {isGameOver}");
        uiOpenClose.OCGameOver(); // 게임 오버 UI 호출용.
    }


    //GameOver 버튼 이벤트 관련
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Retry()
    {
#if UNITY_EDITOR
        Debug.Log("게임 재시작 (에디터)");
#endif
        monsterSpawner.ClearAllMonsters();
        Managers.Pool.ClearAllPools();
        // 현재 씬을 다시 로드하여 게임 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // **씬 로드 시 구독해서 갱신하는 로직 아래에 있음.
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
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


    //게임 재시작(씬 로드 후) 시 추가적으로 재할당.
    //GameManager이외 오브젝트가 재생성되어 참조 재설정을 위한 구독.
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
        RefreshReferences();
    }
        
    
    public void RefreshReferences()
    {
        uiOpenClose = FindObjectOfType<UIOpenClose>(); //UIOpenClose: GameOver UI 제어용
        playerInput = FindObjectOfType<PlayerInput>(); //PlayerInput: 카메라 제어용

        //TogglePause: 인게임 시간 흐르게 하는 용도.
        //isGameOver: 상태 초기화용
        Debug.Log("플레이어 상태: " + isGameOver); 
        if (isGameOver)
        {
            TogglePause();
            isGameOver = !isGameOver;
        }
    }
}