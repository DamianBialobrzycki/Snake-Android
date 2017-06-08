using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Load the Game scene
    public void GameLoad()
    {
        SceneManager.LoadScene(1);
    }

    // Quit from the Game
    public void Quit()
    {
        Application.Quit();
    }
}
