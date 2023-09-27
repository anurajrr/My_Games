using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int poolSize = 10;
    }

    [SerializeField] private List<PoolItem> poolItems = new List<PoolItem>();
    private Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();

    private void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (PoolItem item in poolItems)
        {
            List<GameObject> pool = new List<GameObject>();

            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = Instantiate(item.prefab, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }

            pools.Add(item.prefab, pool);
        }
    }

    public GameObject GetObjectFromPool(GameObject prefab)
    {
        if (pools.ContainsKey(prefab))
        {
            List<GameObject> pool = pools[prefab];
            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
        }

        return null;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = transform.position;
    }
}
