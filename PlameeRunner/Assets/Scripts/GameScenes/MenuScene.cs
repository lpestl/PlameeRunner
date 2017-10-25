using UnityEngine;
using StateStuff;

public class MenuScene : IState<GameSceneManager> {
#region Menu scene singletone instance
    private static MenuScene _instance;

    private MenuScene()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static MenuScene Instance
    {
        get
        {
            if (_instance == null)
            {
                new MenuScene();
            }

            return _instance;
        }
    }
    #endregion
    
#region State interface metods
    public void EnterState(GameSceneManager _owner)
    {
        EchoLog.Print("----------------------");
        EchoLog.Print("Load Menu scene");
        _owner.LoadPrefabs();
        GameEventHandlers.FadeOut();
    }

    public void ExitState(GameSceneManager _owner)
    {
        _owner.DestroySceneResources();
        EchoLog.Print("Destroy Menu scene");
    }

    public void UpdateState(GameSceneManager _owner)
    {
        switch (_owner.currentGameScene)
        {
            case GameSceneManager.GameScene.CHOICE_CHAREPTER:
                _owner.stateMachine.ChangeState(ChoiceCharapterScene.Instance);
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
