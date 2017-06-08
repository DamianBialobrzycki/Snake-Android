using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{ 
    public Text scoreText;

    // Use this for initialization
    void Start () {
        ShowScore();
    }
	
    // Load the Game scene 
    public void GameLoad()
    {
        SceneManager.LoadScene(1);
    }

    // Load Main Menu scene
    public void MenuLoad()
    {
        SceneManager.LoadScene(0);
    }

    // Get score from the game and show it
    void ShowScore()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score").ToString();
    }
}
