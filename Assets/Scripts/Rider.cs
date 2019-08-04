using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour
{

    public Main game;
    public GameObject bow;
    public GameObject arrowPrefab;

    void Awake()
    {
        game = GetComponentInParent<Main>();
    }

    void Update ()
    {
        Move();

        if ( arrows > 0 )
        {
            if ( !bow.activeSelf )
            {
                bow.SetActive( true );
                bowRange = 0;
            }

            IncreaseBowRange();
            Shoot( NearestEnemy() );
        }
        else
        {
            bow.SetActive( false );
        }

        foreach( GameObject arrow in game.Arrows() )
        {
            if ( WithinDistance( arrow, PICK_UP_RANGE ) )
            {
                Destroy( arrow );
                arrows++;
                game.endOfTutorial = true;
            }
        }
    }

    private const int ANTICLOCKWISE_TURN_RATIO = 30;

    private const float MAX_BOW_RANGE = 2f;

    private const float PICK_UP_RANGE = 0.2f;
    private const int SECONDS_TO_FULL_RANGE = 3;
    private const int MAX_ARROWS = 5;
    private int arrows = MAX_ARROWS;
    private float bowRange = MAX_BOW_RANGE;

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
            float distanceToNearest = ( nearest.transform.position - transform.position ).sqrMagnitude;

            if ( distanceToEnemy < distanceToNearest )
            {
                nearest = enemy;
            }
        }

        return nearest;
    }

    private void IncreaseBowRange()
    {
        if ( bowRange < MAX_BOW_RANGE )
        {
            bowRange += MAX_BOW_RANGE * Time.smoothDeltaTime / SECONDS_TO_FULL_RANGE;
            bow.transform.localScale = Vector3.one * ( bowRange / MAX_BOW_RANGE );
        }
    }

    private bool WithinDistance( GameObject target, float distance )
    {
        float distanceToTarget = ( target.transform.position - transform.position ).magnitude;
        return distanceToTarget < distance;
    }

    private void Shoot( GameObject target )
    {
        if ( target == null )
        {
            return;
        }

        if ( WithinDistance( target, bowRange ) )
        {
            GameObject arrow = Instantiate( arrowPrefab, game.transform );
            arrow.transform.position = target.transform.position;

            arrows--;

            Destroy( target );
            bowRange = 0;
        }
    }
}
