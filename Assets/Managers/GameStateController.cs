using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public IGameState currentState { get; private set; }
    public IGameState mainMenuState { get; private set; } = new MainMenuState();
    public IGameState gameplayState { get; private set; } = new GameplayState();
    public IGameState gameOverState { get; private set; } = new GameOverState();
    public IGameState winState { get; private set; } = new WinState();

    private void Start()
    {
        currentState = mainMenuState;
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