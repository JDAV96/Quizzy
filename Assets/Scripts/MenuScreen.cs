using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] GameObject menuButton;

    public void DisplayStartScreen()
    {
        displayText.text = "Test your videogame knowledge with these 5 questions!";
        TextMeshProUGUI startButton = menuButton.GetComponentInChildren<TextMeshProUGUI>();
        startButton.text = "Start Game!";
    }

    public void DisplayEndScreen(int score)
    {
        displayText.text = "Your Final Score was:\n" + score + "%";
        TextMeshProUGUI playAgainButton = menuButton.GetComponentInChildren<TextMeshProUGUI>();
        playAgainButton.text = "Play Again?";
    }


}
