using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scorekeeper : MonoBehaviour
{
    private TextMeshProUGUI scoreTextObject;
    private int correctAnswers = 0;
    private int questionsSeen = 0;

    private void Awake() 
    {
        scoreTextObject = GetComponent<TextMeshProUGUI>();
    }
    private void Start() 
    {
        scoreTextObject.text = "Score: 0%";
    }

    public void SubmittedCorrectAnswer()
    {
        questionsSeen++;
        correctAnswers++;
        DisplayScore();
    }

    public void SubmittedWrongAnswer()
    {
        questionsSeen++;
        DisplayScore();;
    }

    private void DisplayScore()
    {
        scoreTextObject.text = "Score: " + GetScore() + "%";
    }

    public int GetScore()
    {
        return Mathf.RoundToInt((100 * (correctAnswers / (float)questionsSeen)));
    }
}
