using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameStateController gameStateController { get; private set; }
    public ScoreManager scoreManager { get; private set; }
    public PlayerBoundsGenerator playerBoundsGenerator { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        gameStateController = GetComponent<GameStateController>();
        scoreManager = GetComponent<ScoreManager>();
        playerBoundsGenerator = GetComponent<PlayerBoundsGenerator>();
    }
}
