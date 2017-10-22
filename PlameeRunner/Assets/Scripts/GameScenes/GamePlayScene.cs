using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class GamePlayScene : IState<GameSceneManager>
{
#region Game play scene singletone instance
    private static GamePlayScene _instance;

    private GamePlayScene()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static GamePlayScene Instance
    {
        get
        {
            if (_instance == null)
            {
                new GamePlayScene();
            }

            return _instance;
        }
    }
#endregion

#region State interface metods
    public void EnterState(GameSceneManager _owner)
    {
        DebugLog.instance.Print("Load Gameplay scene");
        _owner.LoadPrefabs();
        UiManager.instance.FadeOut();
    }

    public void ExitState(GameSceneManager _owner)
    {
        _owner.DestroySceneResources();
        DebugLog.instance.Print("Destroy Gameplay scene");
    }

    public void UpdateState(GameSceneManager _owner)
    {
        switch (_owner.currentGameScene)
        {
            case GameSceneManager.GameScene.MENU:
                _owner.stateMachine.ChangeState(MenuScene.Instance);
                break;
            case GameSceneManager.GameScene.CHOICE_CHAREPTER:
                _owner.stateMachine.ChangeState(ChoiceCharapterScene.Instance);
                break;
            default:
                break;
        }
    }
#endregion
}
