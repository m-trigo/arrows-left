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

    public static List<GameObject> All<T>( Transform transform )
    {
        List<GameObject> list = new List<GameObject>();
        foreach ( Transform child in transform )
        {
            if ( child.gameObject.GetComponent<T>() != null )
            {
                list.Add( child.gameObject );
            }
        }

        return list;
    }

    public List<GameObject> Enemies()
    {
        return All<Enemy>( transform );
    }

    public List<GameObject> Arrows()
    {
        return All<Arrow>( transform );
    }
}
