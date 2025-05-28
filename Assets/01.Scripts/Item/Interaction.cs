using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            curInteractGameObject = collision.collider.gameObject;
            curInteractable = collision.collider.GetComponent<IInteractable>();

            curInteractable.OnInteract();
        }
    }
}
