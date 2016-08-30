using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridSpace : MonoBehaviour {
    private GameController gameController;
    public Button button;
    public Text buttonText;

    public void SetSpace() {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }

    public void SetGameControllerReference(GameController controller) {
        gameController = controller;
    }

}
