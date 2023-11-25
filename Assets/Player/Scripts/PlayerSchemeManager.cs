using Controls;
using UnityEngine;

public class PlayerSchemeManager : MonoBehaviour
{
    public ActionsCommandsScheme gameplayScheme;
    private InputHandler inputHandler;

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        ActivateMenuScheme();
    }

    public void ActivateMenuScheme()
    {
        inputHandler.UpdateActionsCommandsList(gameplayScheme.actionCommandList);
        inputHandler.UpdateActionsCommandsBindings();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + transform.right * 0.5f, transform.position + transform.right * 1.5f);
    }
}
