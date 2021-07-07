using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    
    public void RetryButton() {
        SceneManager.LoadScene("ForestScene");
    }

    public void ExitButton() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
