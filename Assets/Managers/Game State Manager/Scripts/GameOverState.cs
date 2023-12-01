using UnityEngine;

public class GameOverState : IGameState
{
    public void OnEnter(GameStateController sc)
    {
        CanvasManager canvasManager = CanvasManager.instance;
        canvasManager.ActivateOnlyGameOverPanel();
        GameManager.instance.PauseGame();
    }
    public void UpdateState(GameStateController sc)
    {

    }

    public void OnExit(GameStateController sc)
    {
        GameManager.instance.ResumeGame();
    }
}