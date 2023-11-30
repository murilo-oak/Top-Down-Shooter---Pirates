using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public IGameState currentState { get; private set; }
    public IGameState menuState { get; private set; } = new MainMenuState();
    public IGameState gameplayState { get; private set; } = new GameplayState();

    private void Start()
    {
        currentState = gameplayState;
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