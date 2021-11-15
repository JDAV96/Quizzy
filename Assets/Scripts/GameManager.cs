using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Quiz quiz;
    [SerializeField] MenuScreen menuScreen;
    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        menuScreen.gameObject.SetActive(true);
        menuScreen.DisplayStartScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if(quiz.gameFinished)
        {
            quiz.gameObject.SetActive(false);
            menuScreen.gameObject.SetActive(true);
            menuScreen.DisplayEndScreen(quiz.GetScore());
        }
    }

    public void OnMenuButtonClicked()
    {
        if (gameStarted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            menuScreen.gameObject.SetActive(false);
            quiz.gameObject.SetActive(true);
            gameStarted = true;
        }
    }
}
