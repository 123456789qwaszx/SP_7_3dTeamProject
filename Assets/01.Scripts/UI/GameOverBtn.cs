using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBtn : MonoBehaviour
{
    public void StartBtn()
    {
        GameManager.Instance.GameStart();
    }

    public void RetryBtn()
    {
        if (!GameManager.Instance.isGameOver) // 인게임 중 재시작 시에만 작동.
        {
            GameManager.Instance.TogglePause();
        }

        GameManager.Instance.Retry(); //
    }

    public void TitleBtn()
    {
        GameManager.Instance.Title();
    }

    public void ExitBtn()
    {
        GameManager.Instance.GameExit();
    }
}
