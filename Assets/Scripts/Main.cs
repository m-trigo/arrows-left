using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject hud;
    public GameObject arrowPrefab;
    public GameObject enemyPrefab;
    public GameObject arrowCounterPrefab;
    public GameObject arrowCounterHudPrefab;
    public GameObject enemyCounterPrefab;

    public GameObject village;
    public GameObject knight;
    public GameObject bow;

    public Sprite arrowCounterPresentSprite;
    public Sprite arrowCounterMissingSprite;

    public GameObject trailGenerator;
    public Sprite enemyCounterAliveSprite;
    public Sprite enemyCounterKilledSprite;

    public GameObject arrowCounterContainer;

    public Tutorial tutorial;

    #region Enemy Variables

    [Range( 1, 5 )]
    public int enemySpawnPeriod = 3;

    [Range( 1, 5 )]
    public int enemySpeed = 3;

    [Range( 1, 24 )]
    public int totalEnemies = 20;

    #endregion

    #region Bow Variables

    [Range( 1, 5 )]
    public int secondsToFullRange = 3;

    [Range( 0.5f, 3f )]
    public float maxBowRange = 2f;

    [Range( 1, 10 )]
    public int maxArrows = 5;

    #endregion

    #region Knight Variables

    [Range( 1, 3 )]
    public float pickUpRange;

    [Range( 15, 45 )]
    public float turnRatio;

    [Range( 1, 5 )]
    public float cameraThreshold;

    #endregion

    void Start()
    {
        bowRange = maxBowRange;
        arrowsOnKnight = maxArrows - Arrows().Count;
        elapsedSinceLastSpawn = enemySpawnPeriod; // spawn the first at the end of the tutorial;
        CreateArrowsCounters();
        ChangeArrowCountersDisplay();
        CreateEnemyCounters();
        SpawnAllEnemies();
    }

    private float elapsedVictory = 0;

    private bool HasInput()
    {
        return Input.anyKey;
    }

    void Update()
    {
        KnightMovement();
        KnightAttack();
        KnightPickup();

        EnemyMovement();

        if ( totalEnemiesKilled >= totalEnemies && Enemies().Count == 0 )
        {
            elapsedVictory += Time.smoothDeltaTime;
            if ( elapsedVictory > secondsToFullRange )
            {
                SceneManager.LoadScene( "Victory" );
            }
        }
    }

    private float elapsedSinceLastSpawn;
    private int totalEnemiesSpawned = 1; // Tutorial enemy starts in the game
    private int totalEnemiesKilled = 0;

    private bool drawing = false;
    private int arrowsOnKnight;
    private float bowRange = 0;

    private List<GameObject> arrowCounters = new List<GameObject>();
    private List<GameObject> enemyCounters = new List<GameObject>();

    #region Enemy Functions

    private void SpawnAllEnemies()
    {
        while ( totalEnemiesSpawned < totalEnemies )
        {
            float angle = Random.Range( 0, 2 * Mathf.PI );
            Vector3 position = new Vector2( Mathf.Cos( angle ), Mathf.Sin( angle ) ) * Camera.main.orthographicSize * 1.5f;

            Vector3 awayFromVillageVector = ( position - village.transform.position ).normalized;
            Vector3 startingPosition = position + ( ( enemySpeed / 5f ) * enemySpawnPeriod * totalEnemiesSpawned ) * awayFromVillageVector;

            GameObject enemy = Instantiate( enemyPrefab, transform );
            enemy.transform.position = startingPosition;
            totalEnemiesSpawned++;
        }
    }

    private void EnemyMovement()
    {
        foreach ( GameObject enemy in Enemies() )
        {
            Vector2 vectorToVilage = village.transform.position - enemy.transform.position;
            if ( vectorToVilage.magnitude < 0.1f )
            {
                SceneManager.LoadScene( "GameOver" );
            }

            vectorToVilage.Normalize();
            enemy.transform.Translate( enemySpeed * vectorToVilage * Time.smoothDeltaTime / 5 );
        }
    }

    #endregion

    #region HUD Functions

    private void AdjustHUD()
    {
        float y = Camera.main.orthographicSize;
        float x = y * Camera.main.aspect;

        float dx = Mathf.Abs( knight.transform.position.x ) - ( x - cameraThreshold );
        float dy = Mathf.Abs( knight.transform.position.y ) - ( y - cameraThreshold );

        Vector3 cameraPosition = Camera.main.transform.position;

        if ( dx > 0 )
        {
            if ( knight.transform.position.x < 0 )
            {
                dx *= -1;
            }

            cameraPosition.x = dx;
        }

        if ( dy > 0 )
        {
            if ( knight.transform.position.y < 0 )
            {
                dy *= -1;
            }

            cameraPosition.y = dy;
        }

        Camera.main.transform.position = cameraPosition;
    }

    private void CreateArrowsCounters()
    {
        float y = Camera.main.orthographicSize;
        float x = -Camera.main.orthographicSize * Camera.main.aspect;

        arrowCounterContainer = Instantiate( arrowCounterHudPrefab, hud.transform );

        Vector3 firstArrowPosition = new Vector2( x + 1 / 2f, y - 1 / 2f );
        for ( int i = 0; i < maxArrows; i++ )
        {
            GameObject arrowCounter = Instantiate( arrowCounterPrefab, arrowCounterContainer.transform );
            arrowCounter.transform.position = new Vector3( firstArrowPosition.x + ( i / 4f + i / 8f ), firstArrowPosition.y, 0 );
            arrowCounters.Add( arrowCounter );
        }
    }

    private void ChangeArrowCountersDisplay()
    {
        arrowCounterContainer.GetComponent<HudElement>().Blink();

        for ( int i = 0; i < arrowCounters.Count; i++ )
        {
            if ( arrowsOnKnight < ( i + 1 ) )
            {
                arrowCounters[ i ].GetComponent<SpriteRenderer>().sprite = arrowCounterMissingSprite;
            }
            else
            {
                arrowCounters[ i ].GetComponent<SpriteRenderer>().sprite = arrowCounterPresentSprite;
            }
        }
    }

    private void CreateEnemyCounters()
    {
        float y = Camera.main.orthographicSize;
        float x = Camera.main.orthographicSize * Camera.main.aspect;

        Vector3 lastEnemyPosition = new Vector2( x - 1 / 2f, y - 1 / 2f );
        for ( int i = 0; i < totalEnemies; i++ )
        {
            GameObject enemyCounter = Instantiate( enemyCounterPrefab, hud.transform );
            enemyCounter.transform.position = new Vector3( lastEnemyPosition.x - ( i / 4f + i / 8f ), lastEnemyPosition.y, 0 );
            enemyCounters.Add( enemyCounter );
        }
        enemyCounters.Reverse();
    }

    private void ChangeEnemyCountersDisplay()
    {
        for ( int i = 0; i < enemyCounters.Count; i++ )
        {
            if ( ( i + 1 ) <= totalEnemiesKilled )
            {
                enemyCounters[ i ].GetComponent<SpriteRenderer>().sprite = enemyCounterKilledSprite;
            }
            else
            {
                enemyCounters[ i ].GetComponent<SpriteRenderer>().sprite = enemyCounterAliveSprite;
            }
        }
    }

    #endregion

    #region Knight Functions

    private void KnightMovement()
    {
        float dt = Time.smoothDeltaTime;

        if ( !HasInput() )
        {
            knight.transform.RotateAround( knight.transform.position, Vector3.forward, turnRatio * dt );
        }

        knight.transform.Translate( knight.transform.up * dt );
        trailGenerator.SetActive( true );

        AdjustHUD();
    }

    private void KnightAttack()
    {
        if ( drawing )
        {
            DecreaseBowRange();
            return;
        }

        if ( arrowsOnKnight > 0 )
        {
            if ( !bow.activeSelf )
            {
                bow.SetActive( true );
                bowRange = 0;
            }

            IncreaseBowRange();
            Shoot( NearestEnemy() );
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
                if ( arrow.GetComponent<ArrowScript>().target == null )
                {
                    Destroy( arrow );
                    arrowsOnKnight++;
                    ChangeArrowCountersDisplay();
                }
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
            Vector3 scale = Vector3.one * ( bowRange / maxBowRange );

            if ( scale.x > 1 )
            {
                scale.x = 1;
            }
            if ( scale.y > 1 )
            {
                scale.y = 1;
            }
            if ( scale.z > 1 )
            {
                scale.z = 1;
            }

            bow.transform.localScale = scale;
        }
    }

    private void DecreaseBowRange()
    {
        if ( bowRange > 0 )
        {
            bowRange -= Time.smoothDeltaTime * 10;
            bow.transform.localScale = Vector3.one * ( bowRange / maxBowRange );

            if ( bowRange < 0 )
            {
                bowRange = 0;
                drawing = false;
            }
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
            drawing = true;
            GameObject arrow = Instantiate( arrowPrefab, transform );
            arrow.transform.position = knight.transform.position;
            ArrowScript arrowScript = arrow.GetComponent<ArrowScript>();
            arrowScript.target = target;

            totalEnemiesKilled++;
            arrowsOnKnight--;

            ChangeEnemyCountersDisplay();
            ChangeArrowCountersDisplay();
        }
    }

    #endregion

    public List<GameObject> GameObjectsWithTag( string tag )
    {
        List<GameObject> list = new List<GameObject>();
        foreach ( Transform child in transform )
        {
            if ( child.CompareTag( tag ) )
            {
                list.Add( child.gameObject );
            }
        }

        return list;
    }

    public List<GameObject> Arrows()
    {
        return GameObjectsWithTag( "Arrow" );
    }

    public List<GameObject> Enemies()
    {
        return GameObjectsWithTag( "Enemy" );
    }
}
