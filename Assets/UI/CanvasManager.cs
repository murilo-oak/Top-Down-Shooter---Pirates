using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance { get; private set; }

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject gameOverPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        UpdateCurrentSceneCanvas();
        
        if(SceneManager.GetActiveScene().name == "GameplayScene")
        {
            DeactivateAllCanvas();
            return;
        }

        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            ActivateOnlyMainMenuPanel();
            return;
        }
    }

    public void UpdateCurrentSceneCanvas()
    {
        canvas = GameObject.Find("Canvas");
        Transform canvasTf = canvas.transform;

        mainMenuPanel = canvasTf.Find("Main Menu").gameObject;
        optionsPanel = canvasTf.Find("Options Panel").gameObject;
        gameOverPanel = canvasTf.Find("Game Over Menu").gameObject;
    }

    public void DeactivateAllCanvas()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ActivateOnlyMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ActivateOnlyOptionsPanel()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }
    public void ActivateOnlyGameOverPanel()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
