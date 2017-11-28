using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToLevel : MonoBehaviour {

    public string level1name = "MainScene";
    public string level2name = "Level2";
    public string level3name = "Level3";
    public static int levelNum;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goToLevel1()
    {
        SceneManager.LoadScene(level1name);
        levelNum = 1;
    }

    public void goToLevel2()
    {
        SceneManager.LoadScene(level2name);
        levelNum = 2;
    }

    public void goToLevel3()
    {
        SceneManager.LoadScene(level3name);
        levelNum = 2;
    }
}
