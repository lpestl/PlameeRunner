using ObjectPoolStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple implementation of a universal generator.
/// With this setup, you can use this generator for the following purposes:
/// - parallax background, with its own object pools;
/// - generation of level ground, with its own pools;
/// - generation of obstacles...etc.
/// </summary>
public class Generator : MonoBehaviour
{
#region Properties
    private int worldIndex = 0;

    public Transform generatePoint;
    public Transform destroyPoint;

    public List<WorldLoadablePart> worldPartList;

    private GameObject cursorGameObject;

    private GameObject activatePoolGameObject;
    private GameObject objectPoolGameObject;

    private List<float> rangeChanceList;
    private ObjectPool[] pools;

    private bool beforeStart;
#endregion

#region Creation and destruction of pools, as well as related objects.
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

        // Before start generator loading onli first part object
        beforeStart = true;

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
#endregion

#region Check every tick
    private void Update()
    {
        generatePart();
        destroyPart();
    }
    
    void destroyPart()
    {
        foreach (Transform child in activatePoolGameObject.transform)
        {
            var dynamicScript = child.GetComponent<TypeID>();

            if (child.position.x < destroyPoint.position.x)
            {
                child.transform.parent = objectPoolGameObject.transform;

                pools[dynamicScript.id].PushObject(child.gameObject);
                //break;
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

    void generatePart()
    {
        if (cursorGameObject.transform.position.x < generatePoint.position.x)
        {
            // Choice of a random part of ground in view of the chances of falling.
            int indexPart = beforeStart ? 0: ChoiceRandomPart();

            bool emptyPlace = false;
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
                }
            }

            if (!emptyPlace)
            {
                newPart.transform.parent = activatePoolGameObject.transform;
                newPart.transform.position = new Vector3(cursorGameObject.transform.position.x,
                                                         newPart.transform.position.y,
                                                         newPart.transform.position.z);
            }
            
            cursorGameObject.transform.position = new Vector3(cursorGameObject.transform.position.x + worldPartList[worldIndex].loadeblePartList[indexPart].width,
                                                              cursorGameObject.transform.position.y,
                                                              cursorGameObject.transform.position.z);
        }
    }
    #endregion

#region Subscribe on start level and setter for WorldIndex
    public void SetWorldIndex(int index)
    {
        worldIndex = index;
    }

    private void OnEnable()
    {
        LevelEventSystem.OnStartLevel += OnStartLevel;
    }

    private void OnDisable()
    {
        LevelEventSystem.OnStartLevel -= OnStartLevel;
    }

    private void OnStartLevel()
    {
        beforeStart = false;
    }
#endregion
}
