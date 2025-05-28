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
            Debug.LogWarning("CharacterManager.Instance�� ���� null�Դϴ�.");
            return;
        }

        if (CharacterManager.Instance.Player == null)
        {
            Debug.LogWarning("CharacterManager.Instance.Player�� ���� null�Դϴ�.");
            return;
        }

        if (CharacterManager.Instance.Player.PlayerStats == null)
        {
            Debug.LogWarning("PlayerStats�� ���� null�Դϴ�.");
            return;
        }

        //CharacterManager.Instance.Player.PlayerStats.uiCondition = this;
        Debug.Log(" UICondition�� PlayerStats�� ���������� �����!");
    }
}