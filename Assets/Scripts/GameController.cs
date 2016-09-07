using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Player {
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor {
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour {
    private string playerSide;
    private int moveCount;
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;
    public GameObject quit;

    void Update() {
        if (Input.GetKey("escape")) {
            Quit();
        }
    }

    public void Quit() {
        Application.Quit();
    }

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
            GameOver(false);
        } else if (++moveCount >= 9) {
            GameOver(true);
        } else {
            ChangeSides();
        }
    }

    void GameOver(bool tie) {
        SetBoardInteractable(false);
        if (tie) {
            SetPlayerColorsInactive();
        }
        gameOverText.text = tie ? "It's a draw!" : playerSide + " Wins!";
        gameOverPanel.SetActive(true);
        restartButton.SetActive(true);
    }

    void ChangeSides() {
        playerSide = playerSide == "X" ? "O" : "X";
        SetSide(playerSide);
    }

    void SetBoardInteractable(bool toogle) {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = toogle;
        }
    }

    void SetPlayerButtons(bool toogle) {
        playerX.button.interactable = toogle;
        playerO.button.interactable = toogle;
    }

    void Awake() {
        SetGameControllerReferenceOnButtons();
        Start();
    }

    public void Start() {
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        moveCount = 0;
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].text = "";
        }
        startInfo.SetActive(true);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
    }

    public void SetSide(string side) {
        SetPlayerButtons(false);
        playerSide = side;
        if (playerSide == "X") {
            SetPlayerColors(playerX, playerO);
        } else {
            SetPlayerColors(playerO, playerX);
        }
        startInfo.SetActive(false);
        SetBoardInteractable(true);
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer) {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void SetPlayerColorsInactive() {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

}
