using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject princess;
    public GameObject enemyPrefab;

    public bool endOfTutorial = true;

    [Range(1, 5)]
    public int enemySpawnPeriod = 3;

    public float pickUpRange = 0.2f;

    void Start()
    {

    }

    void Update()
    {
        Spawn();
    }

    private float elapsedSinceLastSpawn = 0;

    private void Spawn()
    {
        if ( endOfTutorial )
        {
            elapsedSinceLastSpawn += Time.smoothDeltaTime;
            if ( elapsedSinceLastSpawn > enemySpawnPeriod )
            {
                elapsedSinceLastSpawn %= enemySpawnPeriod;
                float angle = Random.Range( 0, 2 * Mathf.PI );
                GameObject enemy = Instantiate( enemyPrefab, transform );
                enemy.transform.position = new Vector3( Mathf.Cos( angle ), Mathf.Sin( angle ), 0 ) * 7;
            }
        }
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
