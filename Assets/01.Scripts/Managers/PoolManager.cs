using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    // PoolManager의 함수들은 자체적으로 사용되는 게 아니라, ResourceManager를 통해서만 사용됨.

    // 아래 Pool 클래스의 메소드들은 직접 호출하는게 아니라, PoolManager의 동일한 이름의 메소드를 통해 사용됨.
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 3)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }


        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            Poolable component = go.GetComponent<Poolable>();
            if (component == null)
                go.AddComponent<Poolable>();
            return component;
        }

        // 풀에 넣기(오브젝트 비활성화)
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        // Pool 클래스의 메소드 '팝' = 풀로부터 꺼내오기 (오브젝트 활성화)
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0) // 스택이 빈상태x, 즉 1개라도 대기중이면
                poolable = _poolStack.Pop();
            else  // 지금 비어있다면 새로 생성 -> Create()로 넘어감.
                poolable = Create();

            poolable.gameObject.SetActive(true); // 직접 접근해서 활성화

            poolable.transform.parent = parent; // 파라미터로 받은 parent를 부모로 설정
            poolable.IsUsing = true;

            return poolable;
        }
    }

    // 여러 종류의 풀들을 모아둔 Dictionary, 즉 _pool에서 게임 내 모든 Pool들을 관리
    // 중요! Key는 원본 프리팹의 이름으로 쓸 것! 
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    GameObject _root;


    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" };
            Object.DontDestroyOnLoad(_root);
        }
        
    }

    // 다 사용한 오브젝트를, 다시 풀에 넣어 대기상태로 만들기
    // 그냥 _pool[name].Push(poolable) 해주면 땡
    // 이름(Key)과 일치하는 해당 풀에 해당 오브젝트 poolable을 Push 함수 호출해서 넣어줌.
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        //Pool클래스의 Push메소드를, Managers.Pool.Push를 통해 호출하는 셈.
        _pool[name].Push(poolable);
    }


    public void CreatePool(GameObject original, int count = 3)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root.transform;

        _pool.Add(original.name, pool);
    }


    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }


    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

}
