using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition hydration;
    public Condition hunger;

    private void Start()
    {
        TryRegisterToPlayer();
        //CharacterManager.Instance.Player.PlayerStats.uiCondition = this;
    }
    private void TryRegisterToPlayer()
    {
        if (CharacterManager.Instance == null)
        {
            Debug.LogWarning("CharacterManager.Instance가 아직 null입니다.");
            return;
        }

        if (CharacterManager.Instance.Player == null)
        {
            Debug.LogWarning("CharacterManager.Instance.Player가 아직 null입니다.");
            return;
        }

        if (CharacterManager.Instance.Player.PlayerStats == null)
        {
            Debug.LogWarning("PlayerStats가 아직 null입니다.");
            return;
        }

        CharacterManager.Instance.Player.PlayerStats.uiCondition = this;
        Debug.Log(" UICondition이 PlayerStats에 성공적으로 연결됨!");
    }
}