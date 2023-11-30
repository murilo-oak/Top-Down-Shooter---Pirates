using TMPro;
using UnityEngine;

public class SessionGameTimer : MonoBehaviour
{

    [SerializeField] CanvasInfo canvasInfo;
    [SerializeField] TextMeshProUGUI textmesh;
    [SerializeField] float counter = 0;

    private void Start()
    {
        counter = canvasInfo.timerLengthSeconds;
    }

    private void Update()
    {
        counter -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(counter % 60);
        int minutes = Mathf.FloorToInt(counter / 60);

        textmesh.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }
}
