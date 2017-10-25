using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data structure for display and editing in the Unity Editor.
/// </summary>
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
