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
}
