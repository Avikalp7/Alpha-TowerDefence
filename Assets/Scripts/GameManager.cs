using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameEnded = false;
    public GameObject gameOverUI;

	// Use this for initialization
	void Start () {
        gameEnded = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameEnded)
            return;

        if (PlayerStats.Health <= 0)
            EndGame();	
	}

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }
}
