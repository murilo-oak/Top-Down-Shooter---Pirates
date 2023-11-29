using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameStateController gameStateController { get; private set; }
    public ScoreManager scoreManager { get; private set; }

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
    }
}
