using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPoolStuff;

public class LevelGenerator : MonoBehaviour {
    public Transform generatorPoint;
    public Transform generatorCursor;

    public Transform destroyerPoint;

    public Transform groundUsed;
    public Transform objectPoolTransform;


    public int worldIndex = 0;

    [System.Serializable]
    public class PlatformGroundPart
    {
        public GameObject groundPart;
        public float width;
        public float chance = 0.1f;
    }

    [System.Serializable]
    public class WorldPartPattern
    {
        public string name;
        public List<PlatformGroundPart> groundPartList;
    }
    public List<WorldPartPattern> worldsPartPatterns;

    private List<float> rangeChanceList;
    private ObjectPool[] pools;

	// Use this for initialization
	void Start () {
        generatorPoint.parent = Camera.main.transform;
        destroyerPoint.parent = Camera.main.transform;

        rangeChanceList = new List<float>();
        pools = new ObjectPool[worldsPartPatterns[worldIndex].groundPartList.Count];

        var sumChanse = 0.0f;
        var i = 0;
        foreach (var part in worldsPartPatterns[worldIndex].groundPartList)
        {
            sumChanse += part.chance;
            rangeChanceList.Add(sumChanse);

            pools[i].Capacity = (int)Mathf.Round(100.0f * part.chance);
            i++;
        }

        generatedGroundPutPool();
    }

    void generatedGroundPutPool()
    {
        //var indexPart = ChoiceRandomPart();

        for (int i = 0; i < pools.Length; i++)
        {
            for (int j = 0; j < pools[i].Capacity; j++)
            {
                var newGround = Instantiate(worldsPartPatterns[worldIndex].groundPartList[i].groundPart) as GameObject;
                newGround.transform.parent = objectPoolTransform;
                newGround.AddComponent<TypeID>().id = i;
                pools[i].PushObject(newGround);
            }
        }
    }
    // Update is called once per frame
    void Update () {
        generateWorldGround();
        destroyTraversedGround();
    }

    void destroyTraversedGround()
    {
        foreach (Transform child in groundUsed)
        {
            if (child.position.x < destroyerPoint.position.x)
            {
                Destroy(child.gameObject);
                break;
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

    void generateWorldGround()
    {
        if (generatorCursor.position.x < generatorPoint.position.x)
        {
            // Choice of a random part of ground in view of the chances of falling.
            int indexPart = ChoiceRandomPart();

            var newGround = Instantiate(worldsPartPatterns[worldIndex].groundPartList[indexPart].groundPart) as GameObject;
            newGround.transform.parent = groundUsed;
            newGround.transform.position = new Vector3(generatorCursor.position.x,
                                                       newGround.transform.position.y,
                                                       newGround.transform.position.z);

            generatorCursor.position = new Vector3(generatorCursor.position.x + worldsPartPatterns[worldIndex].groundPartList[indexPart].width,
                                                   generatorCursor.position.y,
                                                   generatorCursor.position.z);
        }
    }
}
