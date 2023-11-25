using Controls;
using UnityEngine;

public class SchemeManager : MonoBehaviour
{
    public ActionsCommandsScheme playerGameplayScheme;
    private InputHandler inputHandler;

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        ActivatePlayerGameplayScheme();
    }

    public void ActivatePlayerGameplayScheme()
    {
        inputHandler.UpdateActionsCommandsList(playerGameplayScheme.actionCommandList);
        inputHandler.UpdateActionsCommandsBindings();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + transform.right * 0.5f, transform.position + transform.right * 1.5f);
    }
}
