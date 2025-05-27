using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 매니저 작성시 Monobehaviour 을 때어줘야 Managers에서 new로 생성가능
public class ResourceManager
{
    // 원본 'GameObject prefab'을 참조하여 씬에 복사를 해줌
    // 풀을 뒤져서 있으면 그걸 활성화 시킴
    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject original = GameObject.Instantiate(prefab);

        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    // 풀에 들어갈 공간이 있으면 비활성화 시킴
    // 아니라면 파괴
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
