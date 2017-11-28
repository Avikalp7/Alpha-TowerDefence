using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour {

	public float startspeed = 10f;
    public int startHealth = 100;
    public float speedDamageTime = 5f;
    public bool isDecoy = false;

	private Transform target;
    private float speedCountdown;
    public int health;
    public float speed;
    public int wavepointIndex = 0;
    private int waveIndex;
    public int value = 20;

    public Image healthBar;

	void Start ()
	{
		target = Guiders.points[0];
        waveIndex = PlayerStats.wavesSurvived + 1;
        health = startHealth + waveIndex*20;
        speed = startspeed;
        speedCountdown = 0f;
	}

	void Update ()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}

        if (speedCountdown > 0f)
        {
            speedCountdown -= Time.deltaTime;
            if (speedCountdown <= 0f)
            {
                speed = startspeed;
                speedCountdown = 0f;
            }
        }
	}

    public void takeDamage(int amt)
    {
        // This is to account for decoy
        if (health >= 0)
        {
            health -= amt;
            healthBar.fillAmount = Math.Max(health / (float)(startHealth + waveIndex * 20), 0f);
        }
        if (health <= 0 && !isDecoy)
        {
            Die();
        }
    }

    public void takeSpeedDamage(int amt)
    {
        speed = Mathf.Max(speed - amt, 5f);
        speedCountdown = speedDamageTime;
    }

    void Die()
    {
        PlayerStats.Money += value;
        Destroy(gameObject);
    }

	void GetNextWaypoint()
	{
		//if (wavepointIndex >= Guiders.points.Length - 1)
		//{
		//	Destroy(gameObject);
		//	return;
		//}

        if (wavepointIndex == 5 || (wavepointIndex == 12 && goToLevel.levelNum == 1) || (wavepointIndex == 13 && goToLevel.levelNum == 2) || (wavepointIndex == 18 && goToLevel.levelNum == 2))
        {
            if (!isDecoy)
                PlayerStats.Health -= 10;
            Destroy(gameObject);
            return;
        }

        wavepointIndex = Guiders.GetNextWaypoint(wavepointIndex, (float)health/(float)startHealth, isDecoy, waveIndex);
        target = Guiders.points[wavepointIndex];

        //float maxHealth;
        //if (value == 100)
        //    maxHealth = 1000f;
        //else if (value == 40)
        //    maxHealth = 400f;
        //else if (value == 20)
        //    maxHealth = 150f;
        //else
        //    maxHealth = 100f;

        //if (wavepointIndex >= 1 && wavepointIndex <= 3)
        //{
        //    if (wavepointIndex == 1)
        //        Guiders.onescore += (20f * health / startHealth);
        //    else if (wavepointIndex == 2)
        //        Guiders.onescore += (40f * health/ startHealth);
        //    else
        //        Guiders.onescore += (240f * health / startHealth);
        //}

        //if (wavepointIndex >= 6 && wavepointIndex <= 10)
        //{
        //    if (wavepointIndex == 6)
        //        Guiders.sixscore += (50f * health / startHealth);
        //    else if (wavepointIndex == 7)
        //        Guiders.sixscore += (100f * health / startHealth);
        //    else if (wavepointIndex == 8)
        //        Guiders.sixscore += (100f * health / startHealth);
        //    else if (wavepointIndex == 9)
        //        Guiders.sixscore += (50f * health / startHealth);
        //}

        //if (wavepointIndex != 0)
        //{
        //    wavepointIndex++;
        //}
        //else
        //{
        //    if (Guiders.onescore >= Guiders.sixscore)
        //    {
        //        wavepointIndex = 1;
        //    }
        //    else
        //    {
        //        wavepointIndex = 6;
        //    }
        //}
    }

}
