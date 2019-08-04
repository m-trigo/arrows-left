using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject target;
    public Sprite bloody;

    void Start()
    {
        
    }

    void Update()
    {
        if ( target == null )
        {
            GetComponent<SpriteRenderer>().sprite = bloody;
            return;
        }

        Vector2 vector = target.transform.position - transform.position;
        if ( vector.magnitude < 0.1f )
        {
            Destroy(target);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 12 * Time.smoothDeltaTime);
    }
}
