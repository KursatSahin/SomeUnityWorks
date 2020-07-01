using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler SharedInstance;
    
        [Serializable]
        public class ObjectPoolItem {
            public int amountToPool;
            public GameObject objectToPool;
            public bool shouldExpand;
        }
    
        public List<GameObject> pooledObjects;
        public List<ObjectPoolItem> itemsToPool;
    
        private void Awake()
        {
            SharedInstance = this;
        }

        void Start () 
        {
            pooledObjects = new List<GameObject>();
            
            foreach (ObjectPoolItem item in itemsToPool) 
            {
                for (int i = 0; i < item.amountToPool; i++) 
                {
                    GameObject obj = Instantiate(item.objectToPool,gameObject.transform);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }

        public GameObject GetPooledObject(string objectTag) 
        {
            for (int i = 0; i < pooledObjects.Count; i++) 
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(objectTag)) 
                {
                    return pooledObjects[i];
                }
            }
            
            foreach (ObjectPoolItem item in itemsToPool) 
            {
                if (item.objectToPool.CompareTag(objectTag)) 
                {
                    if (item.shouldExpand) 
                    {
                        GameObject obj = Instantiate(item.objectToPool,transform);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}