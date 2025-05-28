using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interaction : MonoBehaviour
{
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            Debug.Log("잡았다아이템");
            curInteractGameObject = collision.collider.gameObject;
            curInteractable = collision.collider.GetComponent<IInteractable>();

            curInteractable.OnInteract();
        }
    }
}
