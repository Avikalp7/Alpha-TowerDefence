using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
    private int enemyWavepointTo;

    public int damage = 20;
	public float speed = 70f;
	public GameObject impactEffect;
	
	public void Seek (Transform _target, int _enemyWavepointTo)
	{
		target = _target;
        enemyWavepointTo = _enemyWavepointTo;
	}

	// Update is called once per frame
	void Update () {

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

	void HitTarget ()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 2f);

		Destroy(gameObject);

        Damage(target);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        // Enemy might already be dead!
        if (e != null)
            e.takeDamage(damage);

    }
}
