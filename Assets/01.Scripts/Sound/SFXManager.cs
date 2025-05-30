using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static Unity.VisualScripting.Member;


public class SFXManager : MonoBehaviour
{
    [Header("Bear SFX")]
    public GameObject bearAttackSFX;
    public GameObject bearDieSFX;
    public GameObject bearHitSFX;

    [Header("Player SFX")]
    public GameObject playerAttackSFX;
    public GameObject playerDieSFX;
    public GameObject playerHitSFX;
    public GameObject playerJumpSFX;
    [Header("UI창 체크")]
    public GameObject optionUI;
    public GameObject inventoryUI;

    public void PlaySFX(GameObject sfxPrefab, Vector3 position)
    {
        if ((optionUI != null && optionUI.activeSelf) ||
        (inventoryUI != null && inventoryUI.activeSelf))
        {
            return; // 실행 중지
        }
        if (sfxPrefab == null) return;

        GameObject sfx = Instantiate(sfxPrefab, position, Quaternion.identity);
        AudioSource source = sfx.GetComponent<AudioSource>();
        if (source == null || source.clip == null) return;

        source.Play();
        float clipLength = sfx.GetComponent<AudioSource>().clip.length;
        Destroy(sfx, clipLength);


        //사용예시 public SFXMananger sfxMananger
        //sfxManager.PlaySFX(sfxManager.playerAttackSFX, transform.position);
    }
}
