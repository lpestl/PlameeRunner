using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolStuff
{

    public class ObjectPool
    {
        public int Capacity = 100;

        List<GameObject> pooledObjects = new List<GameObject>();
        
        // Use this for initialization
        public bool PushObject(GameObject pooledObject)
        {
            if (pooledObjects.Count == 100)
            {
                return false;
            }
            else
            {
                pooledObject.SetActive(false);
                pooledObjects.Add(pooledObject);
                return true;
            }
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