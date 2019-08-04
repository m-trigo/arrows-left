using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    private float lifeTime = 5f;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        Color color = transform.GetComponent<SpriteRenderer>().color;
        color.a = lifeTime / 20;
        transform.GetComponent<SpriteRenderer>().color = color;
    }
}
