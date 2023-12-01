using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayState : IGameState
{

    public void OnEnter(GameStateController sc)
    {
        if (SceneManager.GetActiveScene().name != "GameplayScene")
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
    public void UpdateState(GameStateController sc)
    {

    }

    public void OnExit(GameStateController sc)
    {

    }
}