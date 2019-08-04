using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGeneratorScript : MonoBehaviour
{
    public GameObject root;
    public TrailScript trailPrefab;

    public float elapsed = 0;

    void Start()
    {
        
    }

    void Update()
    {
        elapsed += Time.smoothDeltaTime;
        if ( elapsed > 0.2f )
        {
            elapsed %= 0.2f;
            TrailScript trail = Instantiate(trailPrefab, root.transform);
            trail.transform.position = transform.position;
        }
    }
}
