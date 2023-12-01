public interface IGameState
{
    public void OnEnter(GameStateController controller);
    public void UpdateState(GameStateController controller);
    public void OnExit(GameStateController controller);
}