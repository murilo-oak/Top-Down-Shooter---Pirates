using UnityEngine;

public class MainMenuState : IGameState
{

    public void OnEnter(GameStateController sc)
    {
        Debug.Log("Main Menu State");
        
    }
    public void UpdateState(GameStateController sc)
    {

    }

    public void OnExit(GameStateController sc)
    {
    }
}