using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Health;
    public static int Money;
    public int startHealth = 100;
    public int startMoney = 500;
    public static int wavesSurvived = 0;

	// Use this for initialization
	void Start () {
        Health = startHealth;
        Money = startMoney;
        wavesSurvived = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
