using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Awake()
    {
        main_ = GetComponentInParent<Main>();
        target_ = main_.princess;
    }

    void Update ()
    {
        Seek();
    }

    /* Private */

    private const float REACH_DISTANCE = 0.1f;

    private Main main_;
    private GameObject target_;

    private void Seek()
    {
        Vector2 direction = target_.transform.position - transform.position;
        direction.Normalize();
        transform.Translate( direction * Time.smoothDeltaTime );

        if ( HasReachedTarget() )
        {
            Debug.Log( "Game Over!" );
        }
    }

    private bool HasReachedTarget()
    {
        return ( target_.transform.position - transform.position ).magnitude < REACH_DISTANCE;
    }
}
