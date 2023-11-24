using Controls;
using UnityEngine;

public class PlayerSchemeManager : MonoBehaviour
{
    public ActionsCommandsScheme gameplayScheme;

    void Start()
    {
        ActivateMenuScheme();
    }

    public void ActivateMenuScheme()
    {
        InputHandler.instance.UpdateActionsCommandsList(gameplayScheme.actionCommandList);
        InputHandler.instance.UpdateActionsCommandsBindings();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + transform.right * 0.5f, transform.position + transform.right * 1.5f);
    }
}
