using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGeneratorScript : MonoBehaviour
{
    public GameObject root;
    public TrailScript trailPrefab;

    public float elapsed = 0;
    public int i = 0;

    public Sprite left;
    public Sprite right;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;
        if (elapsed > 0.25f)
        {
            elapsed = 0;
            TrailScript trail = Instantiate(trailPrefab, root.transform);
            trail.transform.position = transform.position;

            if ( i++ % 2 == 0)
            {
                trail.GetComponent<SpriteRenderer>().sprite = left;
            }
            else
            {
                trail.GetComponent<SpriteRenderer>().sprite = right;
            }
        }
    }
}
