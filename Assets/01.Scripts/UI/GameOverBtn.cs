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
