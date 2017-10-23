using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
    public class WorldPartPattern
    {
        public string name;
        public GameObject groundPart;
        public float groundOffset = 5.0f;
    }
    public List<WorldPartPattern> worldsPartPatterns;

	// Use this for initialization
	void Start () {
        var index = Random.Range(0, worldsPartPatterns.Count);
		for (var i = 0; i < 100; i++)
        {
            var newPart = Instantiate(worldsPartPatterns[index].groundPart) as GameObject;
            newPart.transform.position = new Vector3(newPart.transform.position.x + i * worldsPartPatterns[index].groundOffset,
                                                     newPart.transform.position.y,
                                                     newPart.transform.position.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
