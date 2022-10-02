using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 残影pool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();
    public static 残影pool Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        GrowPool();
    }
    private void GrowPool()
    {
        for(int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform, false);
            AddToPool(instanceToAdd);
        }
    }
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        pool.Enqueue(instance);
    }
    public GameObject GetFromPool()
    {
        if (pool.Count == 0)
        {
            GrowPool();
        }
        var instance = pool.Dequeue();
        instance.SetActive(true);
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
