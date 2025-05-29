using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour 
{
    public GameObject MarkNPCName;
    public GameObject NPCSearch;
    public GameObject NPCManager;
    public GameObject NPCTextBoard;



    private void OnTriggerEnter(Collider other)
    {
        string npcName = this.gameObject.name;

        //플레이어 테그붙은 오브젝트가 닿은경우

        if (other.CompareTag("Player"))
        {
            // NPCSearch 오브젝트 생성 및 부모 지정

            GameObject instance = Instantiate(NPCSearch, MarkNPCName.transform);

            // 텍스트 설정

            instance.GetComponentInChildren<TextMeshProUGUI>().text = npcName;

            // 이름 지정

            instance.name = npcName;

            // 버튼 클릭 이벤트 연결

             instance.GetComponent<Button>().onClick.AddListener(NPCManager.GetComponent<SearchNPCNameBtn>().ButtonClick);


        }
    }



    private void OnTriggerExit(Collider other)//멀어질 경우 해당 생성 오브젝트 삭제
    {

        string npcName = this.gameObject.name;



        if (other.CompareTag("Player"))
        {
            Transform child = MarkNPCName.transform.Find(npcName);
            if (child != null)
            {
                Destroy(child.gameObject);
            }

            if (NPCTextBoard.activeSelf)
            {
                NPCTextBoard.SetActive(false);
            }


        }

    }

}
