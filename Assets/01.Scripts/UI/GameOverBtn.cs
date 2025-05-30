using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBtn : MonoBehaviour
{
    public void StartBtn()
    {
        GameManager.Instance.GameStart();
    }

    //버그#1: 인게임 중 재시작 시 시간 정지. >> 해결 후 버그#2 발생.
    //버그#2: 게임 오버 후 재시작 시간 정지.
    //버그(해결): 게임 오버가 아닌 경우를 조건을 걸고 TogglePause 함수 실행.
    public void RetryBtn()
    {
        if (!GameManager.Instance.isGameOver) // 인게임 중 재시작 시에만 작동.
        {
            GameManager.Instance.TogglePause();
        }

        GameManager.Instance.Retry();
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
