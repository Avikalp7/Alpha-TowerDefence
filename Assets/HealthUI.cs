using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour {

    public Text healthText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = "HP: " + PlayerStats.Health.ToString();
	}
}
