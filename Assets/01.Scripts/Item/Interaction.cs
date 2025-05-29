using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interaction : MonoBehaviour
{
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public EquipTool curEquip;
    public Transform equipParent;

    private PlayerController controller;
    private PlayerStats stats;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            curInteractGameObject = collision.collider.gameObject;
            curInteractable = curInteractGameObject.GetComponent<IInteractable>();

            curInteractable.OnInteract();
        }
    }

    void Start()
    {
        controller = GetComponent<PlayerController>();
        stats = GetComponent<PlayerStats>();
    }

    public void EquipNew(EquipmentData data)
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<EquipTool>();
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
