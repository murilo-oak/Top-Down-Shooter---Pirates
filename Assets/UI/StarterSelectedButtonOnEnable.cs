using UnityEngine;
using UnityEngine.UI;

public class StarterSelectedButtonOnEnable : MonoBehaviour
{
    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.Select();
    }
}
