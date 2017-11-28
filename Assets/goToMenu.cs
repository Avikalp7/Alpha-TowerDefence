using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMenu : MonoBehaviour {

    public string sceneToLoad = "StartMenu";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Menu()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
