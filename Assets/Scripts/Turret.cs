using UnityEngine;
using System.Collections;
using System;

public class Turret : MonoBehaviour {

	private Transform target;

	public float range = 15f;
	public float fireRate = 1f;
    public bool useFreezeBullet = false;
	private float fireCountdown = 0f;

	public string enemyTag = "Enemy";

    public int currentEnemyWavepointTo;
	public Transform partToRotate;
	public float turnSpeed = 10f;

	public GameObject bulletPrefab;
	public Transform firePoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.35f);
	}
	
	void UpdateTarget ()
	{
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];

        float shortestDistance = Mathf.Infinity;
        int maxEnemyHealth = -100000;
        Enemy nearestEnemy = null;
        Enemy maxHealthEnemy = null;
        foreach (Enemy enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                if (enemy.health > maxEnemyHealth && distanceToEnemy <= range)
                {
                    maxEnemyHealth = enemy.health;
                    maxHealthEnemy = enemy;
                }
            }
        }

        //      float shortestDistance = Mathf.Infinity;
        //GameObject nearestEnemy = null;
        //foreach (GameObject enemy in enemies)
        //{
        //	float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        //	if (distanceToEnemy < shortestDistance)
        //	{
        //		shortestDistance = distanceToEnemy;
        //		nearestEnemy = enemy;
        //	}
        //}

        //if (nearestEnemy != null && shortestDistance <= range)
        //{
        //	target = nearestEnemy.transform;
        //          currentEnemyWavepointTo = nearestEnemy.wavepointIndex;
        //}
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = maxHealthEnemy.transform;
            currentEnemyWavepointTo = maxHealthEnemy.wavepointIndex;
        }
        else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
			return;

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;

	}

	void Shoot ()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (useFreezeBullet)
        {
            FreezeBullet freezebullet = bulletGO.GetComponent<FreezeBullet>();
            if (freezebullet != null)
                freezebullet.Seek(target, currentEnemyWavepointTo);
        }
        else {
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
                bullet.Seek(target, currentEnemyWavepointTo);
        }
	}

	// void OnDrawGizmosSelected ()
	// {
	// 	Gizmos.color = Color.red;
	// 	Gizmos.DrawWireSphere(transform.position, range);
	// }
}
