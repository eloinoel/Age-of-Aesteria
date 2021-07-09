using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject money;
    
    public void RetryButton() {
        money.GetComponent<Money>().resetMoney();
        SceneManager.LoadScene("ForestScene");
    }

    public void ExitButton() {
        money.GetComponent<Money>().resetMoney();
        SceneManager.LoadScene("MainMenuScene");
    }
}
