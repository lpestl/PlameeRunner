using ObjectPoolStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private int worldIndex = 0;

    public Transform generatePoint;
    public Transform destroyPoint;

    public List<WorldLoadablePart> worldPartList;

    private GameObject cursorGameObject;

    private GameObject activatePoolGameObject;
    private GameObject objectPoolGameObject;

    private List<float> rangeChanceList;
    private ObjectPool[] pools;

    //private Vector3 start;
    //private Vector3 generateDistanceToCamera;

    public void CreatPools()
    {
        // Create pool Transform objects
        activatePoolGameObject = new GameObject();
        objectPoolGameObject = new GameObject();

        activatePoolGameObject.name = "activePool";
        objectPoolGameObject.name = "objectPool";

        activatePoolGameObject.transform.parent = transform;
        objectPoolGameObject.transform.parent = transform;

        // Create cursore
        cursorGameObject = new GameObject();

        cursorGameObject.name = "Cursore";

        cursorGameObject.transform.parent = transform;

        cursorGameObject.transform.position = new Vector3(destroyPoint.position.x,
                                                          cursorGameObject.transform.position.y,
                                                          cursorGameObject.transform.position.z);

        // Keep the distance to the camera.
        //generateDistanceToCamera = Camera.main.transform.position - generatePoint.position;
        //destroyDistanceToCamera = Camera.main.transform.position - destroyPoint.position;

        // New arrays
        rangeChanceList = new List<float>();
        pools = new ObjectPool[worldPartList[worldIndex].loadeblePartList.Count];

        var sumChance = 0.0f;
        var i = 0;
        foreach (var part in worldPartList[worldIndex].loadeblePartList)
        {
            sumChance += part.chance;
            rangeChanceList.Add(sumChance);

            int capacity = (int)Mathf.Ceil((generatePoint.transform.position.x - destroyPoint.transform.position.x) / part.width * part.chance);
            pools[i] = new ObjectPool(capacity);

            i++;
        }

        InstantiateInPool();
    }

    public void DestroyPools()
    {
        rangeChanceList.Clear();

        Destroy(cursorGameObject);

        foreach (Transform child in objectPoolGameObject.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(objectPoolGameObject);

        foreach (Transform child in activatePoolGameObject.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(activatePoolGameObject);

    }

    private void Update()
    {
        //generatePoint.position = Camera.main.transform.position - generateDistanceToCamera;
        //destroyPoint.position = Camera.main.transform.position - destroyDistanceToCamera;

        generatePart();
        destroyPart();
    }

    void InstantiateInPool()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            for (int j = 0; j < pools[i].Capacity; j++)
            {
                if (worldPartList[worldIndex].loadeblePartList[i].objectPart != null)
                {
                    var newPart = Instantiate(worldPartList[worldIndex].loadeblePartList[i].objectPart) as GameObject;
                    newPart.transform.parent = objectPoolGameObject.transform;

                    var scaleCurr = worldPartList[worldIndex].loadeblePartList[i].objectPart.transform.localScale.x;
                    var dynamicScript = newPart.AddComponent<TypeID>();
                    dynamicScript.id = i;
                    //dynamicScript.width = worldPartList[worldIndex].loadeblePartList[i].objectPart.transform.localScale.x * scaleCurr;

                    pools[i].PushObject(newPart);
                }
            }
        }
    }

    private int ChoiceRandomPart()
    {
        // Choice of a random part of ground in view of the chances of falling.
        int indexPart = -1;
        var fallenChance = Random.Range(0.0f, rangeChanceList[rangeChanceList.Count - 1]);
        for (var i = 0; i < rangeChanceList.Count; i++)
        {
            if (fallenChance <= rangeChanceList[i])
            {
                indexPart = i;
                break;
            }
        }
        return indexPart;
    }

    void destroyPart()
    {
        foreach (Transform child in activatePoolGameObject.transform)
        {
            var dynamicScript = child.GetComponent<TypeID>();

            if (child.position.x < destroyPoint.position.x)
            {
                child.transform.parent = objectPoolGameObject.transform;
                //var id = child.GetComponent<TypeID>().id;

                pools[dynamicScript.id].PushObject(child.gameObject);
                //break;
            }
        }
    }

    void generatePart()
    {
        if (cursorGameObject.transform.position.x < generatePoint.position.x)
        {
            // Choice of a random part of ground in view of the chances of falling.
            int indexPart = ChoiceRandomPart();

            bool emptyPlace = false;
            //float scaleCurr = 1.0f;
            GameObject newPart = pools[indexPart].PopObject();
            if (newPart == null)
            {
                if (emptyPlace = (worldPartList[worldIndex].loadeblePartList[indexPart].objectPart == null))
                {
                    //EchoLog.Print("Empty place");
                }
                else
                {
                    newPart = Instantiate(worldPartList[worldIndex].loadeblePartList[indexPart].objectPart) as GameObject;
                    var dynamicScript = newPart.AddComponent<TypeID>();
                    dynamicScript.id = indexPart;
                    //scaleCurr = worldPartList[worldIndex].loadeblePartList[indexPart].objectPart.transform.localScale.x;
                    //dynamicScript.width = worldPartList[worldIndex].loadeblePartList[indexPart].width * scaleCurr;
                }
            }

            if (!emptyPlace)
            {
                //var dynamicScript = newPart.AddComponent<TypeID>();
                newPart.transform.parent = activatePoolGameObject.transform;
                newPart.transform.position = new Vector3(cursorGameObject.transform.position.x,
                                                         newPart.transform.position.y,
                                                         newPart.transform.position.z);
                //newPart.transform.eulerAngles = Vector3.zero;
                //if (newPart.GetComponent<Rigidbody>() != null)
                //{
                //    newPart.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //}
            }

            //var scaleCurr = emptyPlace ? 1 : worldPartList[worldIndex].loadeblePartList[indexPart].objectPart.transform.localScale.x;
            cursorGameObject.transform.position = new Vector3(cursorGameObject.transform.position.x + worldPartList[worldIndex].loadeblePartList[indexPart].width,
                                                              cursorGameObject.transform.position.y,
                                                              cursorGameObject.transform.position.z);
        }
    }

    public void SetWorldIndex(int index)
    {
        worldIndex = index;
    }
}
