using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    
    int minutes = 3;
    int seconds = 0;

    private void Start()
    {
        TryParseTime(textMesh.text);
    }

    public void AddMinute()
    {
        minutes++;
        UpdateTextMesh();
    }

    public void SubtractMinute()
    {
        minutes--;
        if (minutes < 0) 
        {
            minutes = 0;
        }
        UpdateTextMesh();
    }

    public void AddSecond()
    {
        seconds++;

        if (seconds > 59)
        {
            seconds = 0;
        }

        UpdateTextMesh();
    }

    public void SubtractSecond()
    {
        seconds--;
        if (seconds < 0)
        {
            seconds = 59;
        }
        UpdateTextMesh();
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
