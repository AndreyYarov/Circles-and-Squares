using System.Collections.Generic;
using UnityEngine;

namespace Pickable
{
    public class PickableManager : MonoBehaviour
    {
        [SerializeField] private PickableObject m_Prefab;
        [SerializeField, Min(0)] private int m_PickableCount = 5;
        [SerializeField, Min(0f)] private float m_SpawnDelay = 3f;

        private float spawnTime;
        private int spawnedCount;
        private Stack<PickableObject> pool;

        private void Start()
        {
            pool = new Stack<PickableObject>();
            spawnedCount = 0;
            spawnTime = Time.time + m_SpawnDelay;
        }

        private void Update()
        {
            if (Time.time >= spawnTime && spawnedCount < m_PickableCount)
                SpawnItem();
        }

        private void SpawnItem()
        {
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f)));
            spawnPos.z = 0f;
            if (pool.Count == 0)
            {
                var item = Instantiate(m_Prefab, spawnPos, Quaternion.identity);
                item.Init(() => DisableItem(item));
            }
            else
            {
                var item = pool.Pop();
                item.transform.SetParent(null);
                item.transform.position = spawnPos;
                item.gameObject.SetActive(true);
            }
            spawnedCount++;
            spawnTime += m_SpawnDelay;
        }

        private void DisableItem(PickableObject item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(transform);
            pool.Push(item);
            if (spawnedCount == m_PickableCount)
                spawnTime = Time.time + m_SpawnDelay;
            spawnedCount--;

        }
    }
}
