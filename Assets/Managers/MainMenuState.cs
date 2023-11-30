using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : IGameState
{

    public void OnEnter(GameStateController sc)
    {
        if (SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            SceneManager.LoadScene("MainMenuScene");
        }
        
        CanvasManager canvasManager = CanvasManager.instance;

        canvasManager.ActivateOnlyMainMenuPanel();
    }
    public void UpdateState(GameStateController sc)
    {

    }

    public void OnExit(GameStateController sc)
    {
    }
}