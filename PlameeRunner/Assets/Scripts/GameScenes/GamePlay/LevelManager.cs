using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPoolStuff;

public class LevelManager : MonoBehaviour
{
#region Properties
    public int worldIndex = 0;
    public int charapterIndex = 0;

    public List<Generator> generators;
    public List<GameObject> charapterList;

    private GameObject player;
#endregion

#region Unity methods
    void Start () {
        LevelEventSystem.StartLevel();
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
#endregion
}
