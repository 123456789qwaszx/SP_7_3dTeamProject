using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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

    public void PlaySFX(GameObject sfxPrefab, Vector3 position)
    {
        if (sfxPrefab == null) return;

        GameObject sfx = Instantiate(sfxPrefab, position, Quaternion.identity);
        float clipLength = sfx.GetComponent<AudioSource>().clip.length;
        Destroy(sfx, clipLength);


        //사용예시 public SFXMananger sfxMananger
        //sfxManager.PlaySFX(sfxManager.playerAttackSFX, transform.position);
    }
}
