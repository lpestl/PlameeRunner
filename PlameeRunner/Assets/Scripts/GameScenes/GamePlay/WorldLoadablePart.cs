using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WorldLoadablePart {

    public string name;

    [System.Serializable]
    public class LoadeblePart
    {
        public GameObject objectPart;
        public float width;
        public float chance = 0.1f;
    }
    public List<LoadeblePart> loadeblePartList;
}
