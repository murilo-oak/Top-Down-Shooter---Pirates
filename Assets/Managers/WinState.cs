using UnityEngine;

public class WinState : IGameState
{
    public void OnEnter(GameStateController sc)
    {
        CanvasManager canvasManager = CanvasManager.instance;
        canvasManager.ActivateOnlyWinPanel();
        canvasManager.UpdateScoreWinUI();
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