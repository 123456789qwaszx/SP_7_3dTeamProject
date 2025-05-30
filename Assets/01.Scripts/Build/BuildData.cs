using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuilding", menuName = "Building System/Building")]
public class BuildData : ScriptableObject
{
    public string buildingName; //건축물 이름
    public GameObject prefab; // 실제 건축물 프리팹
    public GameObject previewPrefab; // 미리보기 프리팹
    public Sprite icon; // UI용 아이콘
    public int cost; // 건축 비용
    public ResourceType costType; // 소모되는 건축재료
}
