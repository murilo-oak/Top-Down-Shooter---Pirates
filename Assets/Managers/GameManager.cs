using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameStateController gameStateController { get; private set; }
    public ScoreManager scoreManager { get; private set; }
    public PlayerBoundsGenerator playerBoundsGenerator { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        instance = this;

        gameStateController = GetComponent<GameStateController>();
        scoreManager = GetComponent<ScoreManager>();
        playerBoundsGenerator = GetComponent<PlayerBoundsGenerator>();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            gameStateController.ChangeState(gameStateController.mainMenuState);
            return;
        }

        if (SceneManager.GetActiveScene().name == "GameplayScene")
        {
            gameStateController.ChangeState(gameStateController.gameplayState);
        }
    }

    public void ResetScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
