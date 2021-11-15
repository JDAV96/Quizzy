using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionTextObject;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite baseButtonImage;
    [SerializeField] Sprite correctButtonImage;
    int correctAnswerIndex;

    [Header("Timer")]
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowAnswer = 10f;
    Timer timer;
    bool isOnQuestion;

    Scorekeeper scorekeeper;

    Slider progressSlider;
    public bool gameFinished = false;


    private void Awake() 
    {
        timer = FindObjectOfType<Timer>();
        scorekeeper = FindObjectOfType<Scorekeeper>();
        progressSlider = FindObjectOfType<Slider>();
    }

    void Start()
    {
        SwitchToNextQuestion();
    }

    private void InitializeSlider()
    {
        progressSlider.maxValue = questions.Count;
        progressSlider.minValue = 0;
    }

    private void Update() 
    {
        CheckTimerStatus();
    }

    private void CheckTimerStatus()
    {
        if (timer.timerFinished)
        {
            if(isOnQuestion)
            {
                DisplayAnswer(-1);
            }
            else
            {
                SwitchToNextQuestion();
            }
        }
    }

    private void SwitchToNextQuestion()
    {
        if (RetrieveRandomQuestion())
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            SwitchToBaseButtonImage();
            ToggleButtonsInteractable(true);
            DisplayQuestion();
            timer.StartTimer(timeToCompleteQuestion);
            isOnQuestion = true;
        }
        else
        {
            gameFinished = true;
        }
    }

    private bool RetrieveRandomQuestion()
    {
        if (questions.Count > 0)
        {
            int randIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[randIndex];
            questions.Remove(questions[randIndex]);
            return true;
        }
        
        return false;
    }

    private void DisplayQuestion()
    {
        questionTextObject.text = currentQuestion.GetQuestion();

        for(int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
        }
    }

    private void SwitchToBaseButtonImage()
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image currentButtonImage = answerButtons[i].GetComponent<Image>();
            currentButtonImage.sprite = baseButtonImage;
        }
    }

    public void OnAnswerChosen(int buttonIndex)
    {
        DisplayAnswer(buttonIndex);
        ToggleButtonsInteractable(false);
    }

    private void DisplayAnswer(int providedAnswerIndex)
    {
        progressSlider.value++;
        if(correctAnswerIndex == providedAnswerIndex)
        {
            questionTextObject.text = "Correct!";
            Image currentButtonImage = answerButtons[providedAnswerIndex].GetComponent<Image>();
            currentButtonImage.sprite = correctButtonImage;
            scorekeeper.SubmittedCorrectAnswer();
        }
        else 
        {
            questionTextObject.text = "Sorry, the correct answer was:\n" + currentQuestion.GetAnswer(correctAnswerIndex);
            Image currentCorrectButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            currentCorrectButtonImage.sprite = correctButtonImage;
            scorekeeper.SubmittedWrongAnswer();
        }

        timer.StartTimer(timeToShowAnswer);
        isOnQuestion = false;
    }

    private void ToggleButtonsInteractable(bool interactable)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button buttonComponent = answerButtons[i].GetComponent<Button>();
            buttonComponent.interactable = interactable;
        }
    }

    public int GetScore()
    {
        return scorekeeper.GetScore();
    }
}
