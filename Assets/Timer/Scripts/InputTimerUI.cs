using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class InputTimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] CanvasInfo canvasInfo;
    
    [Min(0)] int minutes = 3;
    [Min(0)] int seconds = 0;

    private void Start()
    {
        float timerStored = canvasInfo.timerLengthSeconds;
        
        seconds = Mathf.FloorToInt(timerStored % 60);
        minutes = Mathf.FloorToInt(timerStored / 60);

        UpdateTextMesh();
    }

    public void AddMinute()
    {
        minutes++;
        StoreInfoAndUpdateTextMesh();
    }

    public void SubtractMinute()
    {
        minutes--;
        if (minutes < 0) 
        {
            minutes = 0;
        }
        StoreInfoAndUpdateTextMesh();
    }

    public void AddSecond()
    {
        seconds++;

        if (seconds > 59)
        {
            seconds = 0;
        }

        StoreInfoAndUpdateTextMesh();
    }

    public void SubtractSecond()
    {
        seconds--;
        if (seconds < 0)
        {
            seconds = 59;
        }
        StoreInfoAndUpdateTextMesh();
    }

    void StoreInfoAndUpdateTextMesh()
    {
        StoreCanvasInfo();
        UpdateTextMesh();
    }

    void StoreCanvasInfo()
    {
        int lengthForTimer = minutes * 60 + seconds;
        
        if (lengthForTimer == 0)
        {
            seconds++;
            canvasInfo.timerLengthSeconds = seconds;
        }
        else
        {
            canvasInfo.timerLengthSeconds = lengthForTimer;
        }
    }

    void UpdateTextMesh()
    {
        textMesh.text = $"{minutes:00}:{seconds:00}";
    }

    bool TryParseTime(string input)
    {
        minutes = 0;
        seconds = 0;

        string[] parts = input.Split(':');

        if (parts.Length == 2 && int.TryParse(parts[0], out minutes) && int.TryParse(parts[1], out seconds))
        {
            return true;
        }

        return false;
    }
}
