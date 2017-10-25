using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class ChoiceCharapterScene : IState<GameSceneManager>
{
#region Choice charapter scene singletone instance
    private static ChoiceCharapterScene _instance;

    private ChoiceCharapterScene()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ChoiceCharapterScene Instance
    {
        get
        {
            if (_instance == null)
            {
                new ChoiceCharapterScene();
            }

            return _instance;
        }
    }
    #endregion

#region State interface metods
    public void EnterState(GameSceneManager _owner)
    {
        EchoLog.Print("----------------------");
        EchoLog.Print("Load Choice Charapter scene");
        _owner.LoadPrefabs();
        GameEventHandlers.FadeOut();
    }

    public void ExitState(GameSceneManager _owner)
    {
        _owner.DestroySceneResources();
        EchoLog.Print("Destroy Choice Charapter scene");
    }

    public void UpdateState(GameSceneManager _owner)
    {
        switch (_owner.currentGameScene)
        {
            case GameSceneManager.GameScene.MENU:
                _owner.stateMachine.ChangeState(MenuScene.Instance);
                break;
            case GameSceneManager.GameScene.GAMEPLAY:
                _owner.stateMachine.ChangeState(GamePlayScene.Instance);
                break;
            default:
                break;
        }
    }
#endregion
}
