using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    public float TTL = 1;
    public float lifeTime = 0;

    public Sprite large;
    public Sprite small;

    void Start()
    {
        
    }

    void Update()
    {
        lifeTime += Time.smoothDeltaTime;
        if ( lifeTime > TTL )
        {
            Destroy(gameObject);
        }
    }
}
