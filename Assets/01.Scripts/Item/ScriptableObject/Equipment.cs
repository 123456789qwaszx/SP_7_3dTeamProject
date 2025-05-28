using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public EquipmentData _equipedWeapon;
    int _equipedWeaponIndex = 0;

    public float attackRate;
    private bool attacking;
    public float attackDistance;
    public float useStamina;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera camera;

    void Start()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    public void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            Invoke("OncanAttack", attackRate);
        }
    }
    void OncanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if (doesGatherResources && hit.collider.TryGetComponent(out EquipmentData resource))
            {

            }
        }
    }


    EquipmentData selectedItem;
    Resource resource;
    public void Gathering()
    {


        if (selectedItem.type == EquipmentType.Weapon)
        {
            for (int i = 0; i < selectedItem.weaponDamages.Length; i++)
            {
                switch (selectedItem.weaponDamages[i].type)
                {
                    case DamageType.ForMonster:
                        resource.resourceCondition.Add(selectedItem.weaponDamages[i].value);
                        break;
                    case DamageType.ForResource:
                        resource.resourceCondition.Add(selectedItem.weaponDamages[i].value);
                        break;
                }
            }
        }
    }
    
}


