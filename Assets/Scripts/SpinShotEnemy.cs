using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinShotEnemy : Enemy
{
    [SerializeField]
    private float shotRotationSpeed;
    [SerializeField]
    private bool rotateClockwise;

    protected override void Start()
    {
        base.Start();

        if (rotateClockwise)
		{
            shotRotationSpeed = -shotRotationSpeed;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(!isAlive)
		{
            return;
		}

        transform.rotation = rotationQ;
        bulletRotation += shotRotationSpeed * Time.deltaTime;
        HandleShooting(shootDamageBullets);
    }
}
