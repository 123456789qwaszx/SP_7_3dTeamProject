using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallTree : MonoBehaviour
{
    private GetTree _tree;

    void Start()
    {
        _tree = FindChild<GetTree>(gameObject);

        PlayerInteraction interaction = _tree.GetComponent<PlayerInteraction>();
        interaction.InteractInterval = 0.2f;
        interaction.OnPlayerInteraction = OnPlayerTreeInteraction;
    }

    void OnPlayerTreeInteraction(PlayerController pc)
    {
        GameManager.Instance.SpawnLog();
    }



    public GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
}
