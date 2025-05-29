using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
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
    }
}
