using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class GameSceneManager : MonoBehaviour {
    public static GameSceneManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public enum GameScene {
        MENU,
        CHOICE_CHAREPTER,
        GAMEPLAY
    }

    public StateMachine<GameSceneManager> stateMachine { get; set; }

    // TODO: this member is public only for testing in editor
    public GameScene currentGameScene;
	// Use this for initialization
	void Start () {
        stateMachine = new StateMachine<GameSceneManager>(this);
        switch (currentGameScene)
        {
            case GameScene.MENU:
                stateMachine.ChangeState(MenuScene.Instance);
                break;
            case GameScene.CHOICE_CHAREPTER:
                stateMachine.ChangeState(ChoiceCharapterScene.Instance);
                break;
            case GameScene.GAMEPLAY:
                stateMachine.ChangeState(GamePlayScene.Instance);
                break;
            default:
                break;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        stateMachine.Update();
    }

    private GameScene sceneForLoad;
    public void ChangeSceneWithFade(GameScene nextScene)
    {
        sceneForLoad = nextScene;
        UiManager.instance.FadeIn();
        Fade.OnFadeEnded += FadeInEnded;        
    }

    private void FadeInEnded()
    {
        currentGameScene = sceneForLoad;
        Fade.OnFadeEnded -= FadeInEnded;
    }

    [System.Serializable]
    public class LoadableResources
    {
        public GameScene forScene;
        public List<GameObject> prefabs;
    }
    public List<LoadableResources> loadableResources;
    private List<GameObject> loadedResourceInstances;

    public void LoadPrefabs()
    {
        DebugLog.instance.Print("----------------------");
        if (loadedResourceInstances == null)
            loadedResourceInstances = new List<GameObject>();

        foreach (var lr in loadableResources)
        {
            if (lr.forScene == currentGameScene)
            {
                foreach (var res in lr.prefabs)
                {
                    loadedResourceInstances.Add(Instantiate(res) as GameObject);
                    DebugLog.instance.Print("[INFO] loadedResources added GameObject: " + res.name);
                }
            }
        }
    }

    public void DestroySceneResources()
    {
        for (var i = loadedResourceInstances.Count - 1; i >= 0; i--)
        {
            Destroy(loadedResourceInstances[i]);
            loadedResourceInstances.RemoveAt(i);
        }
        DebugLog.instance.Print("[INFO] loadedResources not destroyed: " + loadedResourceInstances.Count);
    }

    private void OnEnable()
    {
        MenuUiManager.OnClickStart += OnClickStart;
    }

    private void OnDisable()
    {
        MenuUiManager.OnClickStart -= OnClickStart;
    }

    private void OnClickStart()
    {
        ChangeSceneWithFade(GameScene.CHOICE_CHAREPTER);
    }
}
