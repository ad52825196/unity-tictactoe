using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    private string playerSide;
    public Text[] buttonList;

    void SetGameControllerReferenceOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide() {
        return playerSide;
    }

    public void EndTurn() {
        bool win = false;

        for (int i = 0; i < 3 && !win; i++) {
            if (playerSide == buttonList[i * 3].text && buttonList[i * 3].text == buttonList[i * 3 + 1].text && buttonList[i * 3 + 1].text == buttonList[i * 3 + 2].text ||
                playerSide == buttonList[i].text && buttonList[i].text == buttonList[i + 3].text && buttonList[i + 3].text == buttonList[i + 6].text) {
                win = true;
            }
        }
        if (!win) {
            if (playerSide == buttonList[0].text && buttonList[0].text == buttonList[4].text && buttonList[4].text == buttonList[8].text ||
                playerSide == buttonList[2].text && buttonList[2].text == buttonList[4].text && buttonList[4].text == buttonList[6].text) {
                win = true;
            }
        }

        if (win) {
            GameOver();
        } else {
            ChangeSides();
        }
    }

    void GameOver() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
    }

    void ChangeSides() {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void Awake() {
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
    }

}
