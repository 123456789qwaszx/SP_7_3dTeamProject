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
 
                // NPCSearch 오브젝트를 MarkNPCName의 자식으로 설정
                // TextMeshPro 컴포넌트를 찾아 이름 설정
                Instantiate(NPCSearch, MarkNPCName. transform).GetComponentInChildren<TextMeshProUGUI>().text = npcName;
                    NPCSearch.gameObject.name = npcName;

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string npcName = this.gameObject.name;

            // 자식 중에 npcName을 가진 오브젝트를 찾아서 제거

            Destroy(MarkNPCName.transform.Find(npcName + "(Clone)").gameObject);



        }
    }
}