using TMPro;
using UnityEngine;

public class SessionGameTimerUI : MonoBehaviour
{

    [SerializeField] CanvasInfo canvasInfo;
    [SerializeField] TextMeshProUGUI textmesh;
    [SerializeField] float counter = 0;
    bool finishedFinishedCounter;
    bool flagToTransistionToWinState = false;

    private void Start()
    {
        finishedFinishedCounter = false;
        counter = canvasInfo.timerLengthSeconds;
        flagToTransistionToWinState = false;
    }

    private void Update()
    {
        if (!finishedFinishedCounter)
        {
            CountAndUpdateTextMesh();
            if (counter == 0)
            {
                finishedFinishedCounter = true;
                flagToTransistionToWinState = true;
            }
            return;
        }

        if (flagToTransistionToWinState)
        {
            GameStateController gameStateController = GameManager.instance.gameStateController;
            gameStateController.ChangeState(gameStateController.winState);
            flagToTransistionToWinState = false;
        }
    }

    private void CountAndUpdateTextMesh()
    {
        counter -= Time.deltaTime;
        counter = Mathf.Max(0, counter);
        int seconds = Mathf.FloorToInt(counter % 60);
        int minutes = Mathf.FloorToInt(counter / 60);
        UpdateTextMesh(minutes, seconds);
    }

    private void UpdateTextMesh(int minutes, int seconds)
    {
        textmesh.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }
}
