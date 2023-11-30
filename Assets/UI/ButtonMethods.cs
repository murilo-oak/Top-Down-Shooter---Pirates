using UnityEngine;

public class ButtonMethods : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        GameStateController controller = GameManager.instance.gameStateController;
        controller.ChangeState(controller.gameplayState);
    }

    public void OnPlayAgainClick()
    {
        GameStateController controller = GameManager.instance.gameStateController;
        controller.ChangeState(controller.gameplayState);

        GameManager.instance.ResetScene();
    }

    public void OnMainMenuClick()
    {
        GameStateController controller = GameManager.instance.gameStateController;
        controller.ChangeState(controller.mainMenuState);
    }
}