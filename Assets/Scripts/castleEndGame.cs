using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castleEndGame : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject GameWon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<UnitGeneral>().health <= 0) {
            if(this.name == "blue_fort") {
                GameOver();
            }
            if(this.name == "red_fort") {
                WinGame();
            }
        }
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
    }

    public void WinGame() {
        GameWon.gameObject.SetActive(true);
    }
}
