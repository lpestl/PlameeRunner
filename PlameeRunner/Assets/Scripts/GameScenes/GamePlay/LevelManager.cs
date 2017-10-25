using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPoolStuff;

public class LevelManager : MonoBehaviour
{
    public int worldIndex = 0;
    public int charapterIndex = 0;

    public List<Generator> generators;
    public List<GameObject> charapterList;

    private GameObject player;

    void Awake()
    {
        //foreach(var gen in generators)
        //{
        //    gen.SetWorldIndex(worldIndex);
        //    gen.CreatPools();
        //}    
    }

    // Use this for initialization
    void Start () {
        LevelEventSystem.StartLevel();
    }

    //
    // Update is called once per frame
    void Update () {
        
    }

    private void OnEnable()
    {
        charapterIndex = PlayerPrefs.GetInt("Charapter", 0);
        worldIndex = PlayerPrefs.GetInt("World", 0);

        foreach (var gen in generators)
        {
            gen.SetWorldIndex(worldIndex);
            gen.CreatPools();
        }

        player = Instantiate(charapterList[charapterIndex], transform) as GameObject;

        GetComponent<CameraFollow>().SetTarget(player);
    }

    private void OnDisable()
    {
        Destroy(player);

        foreach (var gen in generators)
        {
            gen.DestroyPools();
        }
    }
}
