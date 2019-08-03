using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour
{
    private const int ANTICLOCKWISE_TURN_RATIO = 30;

    private Main game;

    void Awake()
    {
        game = GetComponentInParent<Main>();
    }

    void Update ()
    {
        Move();
        CurrentWeapon().Attack( NearestEnemy() );
	}

    private void Move()
    {
        float dt = Time.smoothDeltaTime;

        if ( Input.GetKey( KeyCode.Space ) )
        {
            transform.RotateAround( transform.position, Vector3.forward, ANTICLOCKWISE_TURN_RATIO * dt );
        }

        transform.Translate( transform.up * dt );
    }

    private GameObject NearestEnemy()
    {
        GameObject nearest = null;
        foreach ( GameObject enemy in game.Enemies() )
        {
            if ( nearest == null )
            {
                nearest = enemy;
                continue;
            }

            float distanceToEnemy = ( enemy.transform.position - transform.position ).sqrMagnitude;
            float distanceToNearest = ( enemy.transform.position - transform.position ).sqrMagnitude;

            if ( distanceToEnemy < distanceToNearest )
            {
                nearest = enemy;
            }
        }

        return nearest;
    }

    private Weapon CurrentWeapon()
    {
        return GetComponentInChildren<Weapon>();
    }
}
