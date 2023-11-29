using UnityEngine;

public class GameStateController : MonoBehaviour
{
    IGameState currentState;
    IGameState startMenuState = new StartMenuState();
    IGameState gameplayState = new GameplayState();

    private void Start()
    {
        ChangeState(startMenuState);
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }
    public void ChangeState(IGameState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}