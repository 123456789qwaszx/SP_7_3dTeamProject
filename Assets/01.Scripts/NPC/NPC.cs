using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject MarkNPCName;
    public GameObject NPCSearch;



    private void OnTriggerEnter(Collider other)
    {
        string npcName = this.gameObject.name;
        if (other.CompareTag("Player"))
        {
 
                // NPCSearch ������Ʈ�� MarkNPCName�� �ڽ����� ����
                // TextMeshPro ������Ʈ�� ã�� �̸� ����
                Instantiate(NPCSearch, MarkNPCName. transform).GetComponentInChildren<TextMeshProUGUI>().text = npcName;
                    NPCSearch.gameObject.name = npcName;

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string npcName = this.gameObject.name;

            // �ڽ� �߿� npcName�� ���� ������Ʈ�� ã�Ƽ� ����

            Destroy(MarkNPCName.transform.Find(npcName + "(Clone)").gameObject);



        }
    }
}