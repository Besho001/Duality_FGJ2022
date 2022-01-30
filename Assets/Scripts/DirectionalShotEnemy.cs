using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShotEnemy : Enemy
{
	public float currentMoveDelay;
	public float moveDelay;

	protected override void Start()
	{
		base.Start();
		bulletRotation = gameObject.transform.rotation.eulerAngles.z;
	}

	protected override void Update()
	{
		base.Update();

		HandleShooting(shootDamageBullets);
		UpdateShotDirection();
		HandleMovement();
	}

	void UpdateShotDirection()
	{
		Vector3 playerPos = FindObjectOfType<PlayerControlller>().transform.position;

		//get the vector representing the mouse's position relative to the point...
		Vector3 direction = (transform.position + bulletOffset - playerPos).normalized;

		//use atan2 to get the angle; Atan2 returns radians
		float angleRadians = Mathf.Atan2(direction.y, direction.x);

		//convert to degrees
		float angleDegrees = angleRadians * Mathf.Rad2Deg;

		// angleDegrees will be in the range ]-180,180].
		if (angleDegrees < 0)
		{
			angleDegrees += 360;
		}

		//Debug.Log(angleDegrees);

		bulletRotation = angleDegrees - 90;
		gameObject.transform.rotation = Quaternion.Euler(0, 0, bulletRotation);
	}

	void HandleMovement()
	{
		Vector3 playerPos = FindObjectOfType<PlayerControlller>().transform.position;

		//get the vector representing the mouse's position relative to the point...
		Vector3 direction = (playerPos - transform.position + bulletOffset).normalized;

		if (currentMoveDelay <= 0)
		{
			rb.AddForce(direction * acceleration, ForceMode2D.Impulse);

			Vector2 velocity = rb.velocity;
			velocity = Vector3.ClampMagnitude(velocity, speed);
			rb.velocity = velocity;

			currentMoveDelay = moveDelay;
		}
		else if (currentMoveDelay > 0)
		{
			currentMoveDelay -= Time.deltaTime;
		}
	}
}