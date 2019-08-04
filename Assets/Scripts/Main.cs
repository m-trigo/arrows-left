using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Show enemies left

// Tutorial

// Add sword

// Victory screen
// Defeat screen


// Knight roof marks / trail
// Shotting "animation" ( shrink circle fast, shoot little arrow, only then grow circle )

public class Main : MonoBehaviour
{
    public GameObject hud;
    public GameObject arrowPrefab;
    public GameObject enemyPrefab;
    public GameObject arrowCounterPrefab;

    public GameObject princess;
    public GameObject knight;
    public GameObject bow;

    public Sprite arrowCounterPresentSprite;
    public Sprite arrowCounterMissingSprite;

    #region Enemy Variables

    [Range(1, 5)]
    public int enemySpawnPeriod = 3;

    [Range( 1, 5 )]
    public int enemySpeed = 3;

    #endregion

    #region Bow Variables

    [Range( 1, 5 )]
    public int secondsToFullRange = 3;

    [Range(0.5f, 3f)]
    public float maxBowRange = 2f;

    [Range(1, 10)]
    public int maxArrows = 5;

    #endregion

    #region Knight Variables

    [Range(1, 3)]
    public int pickUpRange;

    [Range(15, 45)]
    public int turnRatio = 30;

    [Range( 3, 5 )]
    public int cameraThreshold = 4;

    #endregion

    private int arrowsOnKnight;
    private float bowRange;

    private List<GameObject> arrowCounters;

    void Start()
    {
        bowRange = maxBowRange;
        arrowsOnKnight = maxArrows - 2;
        CreateArrowsCounters();
    }

    void Update()
    {
        if ( Input.GetKey( KeyCode.Space ) )
        {
            startOfTutorial = true;
        }

        if ( !startOfTutorial )
        {
            return;
        }

        KnightMovement();
        KnightAttack();
        KnightPickup();

        EnemySpawn();
        EnemyMovement();
    }

    private bool startOfTutorial = false;
    private bool endOfTutorial = false;
    private float elapsedSinceLastSpawn = 0;

    #region Enemy Functions

    private void EnemySpawn()
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

    private void EnemyMovement()
    {
        foreach( GameObject enemy in Enemies() )
        {
            Vector2 vectorToPrincess = princess.transform.position - enemy.transform.position;
            if ( vectorToPrincess.magnitude < 0.1f )
            {
                Debug.Log( "Game Over!" );
            }

            vectorToPrincess.Normalize();
            enemy.transform.Translate( enemySpeed * vectorToPrincess * Time.smoothDeltaTime / 5 );
        }
    }

    #endregion

    #region HUD Functions

    private void CreateArrowsCounters()
    {
        arrowCounters = new List<GameObject>();
        Vector3 firstArrowPosition = arrowCounterPrefab.transform.position;
        for ( int i = 0; i < maxArrows; i++ )
        {
            GameObject arrowCounter = Instantiate( arrowCounterPrefab, hud.transform );
            arrowCounter.transform.position = new Vector3( firstArrowPosition.x + (i * 0.2f), firstArrowPosition.y, 0 );
            arrowCounters.Add( arrowCounter );
        }
    }

    private void ChangeArrowCountersDisplay()
    {
        for ( int i = 0; i < arrowCounters.Count; i++ )
        {
            if ( arrowsOnKnight < (i + 1) )
            {
                arrowCounters[i].GetComponent<SpriteRenderer>().sprite = arrowCounterMissingSprite;
            }
            else
            {
                arrowCounters[i].GetComponent<SpriteRenderer>().sprite = arrowCounterPresentSprite;
            }
        }
    }

    #endregion

    #region Knight Functions

    private void KnightMovement()
    {
        float dt = Time.smoothDeltaTime;

        if ( Input.GetKey( KeyCode.Space ) )
        {
            knight.transform.RotateAround( knight.transform.position, Vector3.forward, turnRatio * dt );
        }

        knight.transform.Translate( knight.transform.up * dt );

        Vector3 distanceFromCenter = knight.transform.position - transform.position;

        float dx = Mathf.Abs( distanceFromCenter.x ) - cameraThreshold;
        float dy = Mathf.Abs( distanceFromCenter.y ) - cameraThreshold;

        Vector3 cameraPosition = Camera.main.transform.position;

        if ( dx > 0 )
        {
            if ( distanceFromCenter.x < 0 )
            {
                dx *= -1;
            }

            cameraPosition.x = dx;
            Camera.main.transform.position = cameraPosition;
        }

        if ( dy > 0 )
        {
            if ( distanceFromCenter.y < 0 )
            {
                dy *= -1;
            }

            cameraPosition.y = dy;
            Camera.main.transform.position = cameraPosition;
        }

        Camera.main.transform.position = cameraPosition;
    }

    private void KnightAttack()
    {
        if ( arrowsOnKnight > 0 )
        {
            if ( !bow.activeSelf )
            {
                bow.SetActive( true );
                bowRange = 0;
            }

            IncreaseBowRange();
            Shoot( NearestEnemy() );
            ChangeArrowCountersDisplay();
        }
        else
        {
            bow.SetActive( false );
        }
    }

    private void KnightPickup()
    {
        foreach ( GameObject arrow in Arrows() )
        {
            if ( WithinDistanceOfKnight( arrow, pickUpRange / 10f ) )
            {
                Destroy( arrow );
                arrowsOnKnight++;
                ChangeArrowCountersDisplay();
                endOfTutorial = true;
            }
        }
    }

    private GameObject NearestEnemy()
    {
        GameObject nearest = null;
        foreach ( GameObject enemy in Enemies() )
        {
            if ( nearest == null )
            {
                nearest = enemy;
                continue;
            }

            float distanceToEnemy = ( enemy.transform.position - knight.transform.position ).sqrMagnitude;
            float distanceToNearest = ( nearest.transform.position - knight.transform.position ).sqrMagnitude;

            if ( distanceToEnemy < distanceToNearest )
            {
                nearest = enemy;
            }
        }

        return nearest;
    }

    private void IncreaseBowRange()
    {
        if ( bowRange < maxBowRange )
        {
            bowRange += maxBowRange * Time.smoothDeltaTime / secondsToFullRange;
            bow.transform.localScale = Vector3.one * ( bowRange / maxBowRange );
        }
    }

    private bool WithinDistanceOfKnight( GameObject target, float distance )
    {
        float sqrDistanceToTarget = ( target.transform.position - knight.transform.position ).sqrMagnitude;
        return sqrDistanceToTarget < ( distance * distance );
    }

    private void Shoot( GameObject target )
    {
        if ( target == null )
        {
            return;
        }

        if ( WithinDistanceOfKnight( target, bowRange ) )
        {
            GameObject arrow = Instantiate( arrowPrefab, transform );
            arrow.transform.position = target.transform.position;

            arrowsOnKnight--;

            Destroy( target );
            bowRange = 0;
        }
    }

    #endregion

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
