using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System;

public class GameSceneManager : MonoBehaviour {
#region Properties
    public enum GameScene {
        MENU,
        CHOICE_CHAREPTER,
        GAMEPLAY
    }

    public StateMachine<GameSceneManager> stateMachine { get; set; }
    // TODO: this member is public only for testing in editor
    public GameScene currentGameScene;

    // Class for storing prefabs for loaded.
    [System.Serializable]
    public class LoadableResources
    {
        public GameScene forScene;
        public List<GameObject> prefabs;
    }
    // Public list for loading resources.
    public List<LoadableResources> loadableResources;
    // List with loaded GameObject instances.
    private List<GameObject> loadedResourceInstances;
#endregion

#region Start and Update metotds
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
	
	void Update () {
        stateMachine.Update();
    }
#endregion

#region Implementing blackout before changing scene
    private GameScene sceneForLoad;
    public void ChangeSceneWithFade(GameScene nextScene)
    {
        sceneForLoad = nextScene;
        GameEventHandlers.FadeIn();
        GameEventHandlers.OnFadeEnded += FadeInEnded;        
    }

    private void FadeInEnded()
    {
        GameEventHandlers.OnFadeEnded -= FadeInEnded;
        currentGameScene = sceneForLoad;
    }
#endregion

#region Actions when changing state
    public void LoadPrefabs()
    {
        if (loadedResourceInstances == null)
            loadedResourceInstances = new List<GameObject>();

        foreach (var lr in loadableResources)
        {
            if (lr.forScene == currentGameScene)
            {
                foreach (var res in lr.prefabs)
                {
                    loadedResourceInstances.Add(Instantiate(res) as GameObject);
                    EchoLog.Print("[INFO] loadedResources added GameObject: " + res.name);
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
        EchoLog.Print("[INFO] loadedResources not destroyed: " + loadedResourceInstances.Count);
    }
#endregion

#region Subscribe to events
    private void OnEnable()
    {
        MenuManager.OnClickStart += ClickStart;
    }

    private void OnDisable()
    {
        MenuManager.OnClickStart -= ClickStart;
    }
#endregion

#region Actions on events
    private void ClickStart()
    {
        ChangeSceneWithFade(GameScene.GAMEPLAY);
    }
#endregion
}
