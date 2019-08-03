using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;

    void Start()
    {
        range = MAX_RANGE;

        for( int i = 0; i < MAX_ARROWS; i++ )
        {
            Instantiate( arrowPrefab, transform );
        }
    }

    void Update()
    {
        IncreaseRange();
    }

    const int MAX_ARROWS = 5;
    List<GameObject> arrows = new List<GameObject>();

    const float MAX_RANGE = 0.5f;
    float range;

    void IncreaseRange()
    {
        if ( range < MAX_RANGE )
        {
            range += MAX_RANGE * Time.smoothDeltaTime;
            transform.localScale = Vector3.one * ( range / MAX_RANGE ); 
        }
    }

    public void Shoot( GameObject target )
    {
        if ( target == null )
        {
            return;
        }

        float distance = ( target.transform.position - transform.position ).magnitude;

        Debug.Log( range );
        if ( distance < range )
        {
            Destroy( target );
            range = 0;
        }

    }
}
