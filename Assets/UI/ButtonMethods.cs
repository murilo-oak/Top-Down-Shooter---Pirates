using UnityEngine;

public class ButtonMethods : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        GameStateController controller = GameManager.instance.gameStateController;
        controller.ChangeState(controller.gameplayState);
    }
}