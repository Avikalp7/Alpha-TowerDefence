using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Waves : MonoBehaviour {

	public Transform enemyPrefab;
    public Wave[] waves;

	public Transform spawnPoint;

	public float timeBetweenWaves = 12f;
	private float countdown = 10f;
    private float timer = -0.5f;

	public Text waveCountdownText;

	public int waveIndex = 0;

	void Update ()
	{
        if (waveIndex > 10)
            timeBetweenWaves = 16f;
        if (waveIndex == 17)
            timeBetweenWaves = 20f;
        if (waveIndex == 5)
            timeBetweenWaves = 20f;
        if (waveIndex > 20)
            timeBetweenWaves = 13f;

        if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;
        timer += Time.deltaTime;

		//waveCountdownText.text = "Wave " + Mathf.Round(timer).ToString();
        waveCountdownText.text = "Wave " + (waveIndex + 1).ToString();
    }

	IEnumerator SpawnWave ()
	{
        Wave wave = waves[waveIndex];
		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f/wave.rate);
		}
        PlayerStats.wavesSurvived += 1;
		waveIndex++;
        if (waveIndex == waves.Length)
        {
            this.enabled = false;
        }
	}

	void SpawnEnemy (GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
	}

}
