using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject princess;

    void Start()
    {

    }

    void Update()
    {

    }

    public List<GameObject> Enemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach ( Transform child in gameObject.transform )
        {
            if ( child.gameObject.GetComponent<Enemy>() != null )
            {
                enemies.Add( child.gameObject );
            }
        }

        return enemies;
    }
}
