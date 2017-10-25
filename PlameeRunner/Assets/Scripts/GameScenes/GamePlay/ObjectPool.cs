using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolStuff
{
    /// <summary>
    /// NOTE: My implementation of a simple object pool.
    /// </summary>
    public class ObjectPool
    {
        public int Capacity;

        List<GameObject> pooledObjects = new List<GameObject>();
        
        public ObjectPool(int capacity)
        {
            Capacity = capacity;
        }
        
        public bool PushObject(GameObject pooledObject)
        {
            if (pooledObjects.Count > Capacity)
            {
                Debug.Log("the pool is full.");
                Capacity++;
            }

            pooledObjects.Add(pooledObject);
            pooledObject.SetActive(false);
            return true;
        }

        public GameObject PopObject()
        {
            if (pooledObjects.Count == 0) return null;
            var popedObject = pooledObjects[0];
            pooledObjects.RemoveAt(0);
            popedObject.SetActive(true);
            return popedObject;
        }
    }
}