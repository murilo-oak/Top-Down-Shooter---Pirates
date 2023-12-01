using UnityEngine;
using UnityEngine.UI;

public class AutoSelectButtonOnEnable : MonoBehaviour
{
    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.Select();
    }
}
