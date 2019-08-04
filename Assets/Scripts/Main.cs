using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Victory screen
// Defeat screen

// Prepare game's page

public class Main : MonoBehaviour
{
    public GameObject hud;
    public GameObject arrowPrefab;
    public GameObject enemyPrefab;
    public GameObject arrowCounterPrefab;
    public GameObject enemyCounterPrefab;

    public GameObject princess;
    public GameObject knight;
    public GameObject bow;

    public Sprite arrowCounterPresentSprite;
    public Sprite arrowCounterMissingSprite;

    public GameObject trailGenerator;
    public Sprite enemyCounterAliveSprite;
    public Sprite enemyCounterKilledSprite;

    #region Enemy Variables

    [Range(1, 5)]
    public int enemySpawnPeriod = 3;

    [Range( 1, 5 )]
    public int enemySpeed = 3;

    [Range(1, 24)]
    public int totalEnemies = 20;

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

    void Start()
    {
        bowRange = maxBowRange;
        arrowsOnKnight = maxArrows - 2; // Two tutorial arrows start on the ground
        elapsedSinceLastSpawn = enemySpawnPeriod; // spawn the first at the end of the tutorial;
        CreateArrowsCounters();
        ChangeArrowCountersDisplay();
        CreateEnemyCounters();
    }

    private float elapsedTutorial = 0;
    private float elapsedVictory = 0;

    void Update()
    {
        elapsedTutorial += Time.deltaTime;

        if ( elapsedTutorial > 1 && displayTutorialText && Input.anyKey )
        {
            DestroyTutorialText();
            displayTutorialText = false;
        }

        if ( displayTutorialText )
        {
            return;
        }

        KnightMovement();
        KnightAttack();
        KnightPickup();

        EnemySpawn();
        EnemyMovement();

        if (totalEnemiesKilled >= totalEnemies && Enemies().Count == 0)
        {
            elapsedVictory += Time.smoothDeltaTime;
            if ( elapsedVictory > secondsToFullRange)
            {
                SceneManager.LoadScene("Victory");
            }
        }
    }

    private bool displayTutorialText = true;

    private float elapsedSinceLastSpawn;
    private int totalEnemiesSpawned = 1; // Tutorial enemy starts in the game
    private int totalEnemiesKilled = 0;

    private bool drawing = false;
    private int arrowsOnKnight;
    private float bowRange = 0;

    private List<GameObject> arrowCounters = new List<GameObject>();
    private List<GameObject> enemyCounters = new List<GameObject>();

    #region Enemy Functions

    private void EnemySpawn()
    {
        if ( totalEnemiesKilled > 0 && totalEnemiesSpawned < totalEnemies )
        {
            elapsedSinceLastSpawn += Time.smoothDeltaTime;
            if ( elapsedSinceLastSpawn > enemySpawnPeriod )
            {
                elapsedSinceLastSpawn %= enemySpawnPeriod;
                float angle = Random.Range( 0, 2 * Mathf.PI );
                GameObject enemy = Instantiate( enemyPrefab, transform );
                enemy.transform.position = new Vector3( Mathf.Cos( angle ), Mathf.Sin( angle ), 0 ) * 7;
                totalEnemiesSpawned++;
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
                SceneManager.LoadScene("GameOver");
            }

            vectorToPrincess.Normalize();
            enemy.transform.Translate( enemySpeed * vectorToPrincess * Time.smoothDeltaTime / 5 );
        }
    }

    #endregion

    #region HUD Functions

    private void AdjustHUD()
    {
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

    private void CreateArrowsCounters()
    {
        Vector3 firstArrowPosition = arrowCounterPrefab.transform.position;
        for ( int i = 0; i < maxArrows; i++ )
        {
            GameObject arrowCounter = Instantiate( arrowCounterPrefab, hud.transform );
            arrowCounter.transform.position = new Vector3( firstArrowPosition.x + ( i * 0.2f ), firstArrowPosition.y, 0 );
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

    private void CreateEnemyCounters()
    {
        Vector3 lastEnemyPosition = enemyCounterPrefab.transform.position;
        for ( int i = 0; i < totalEnemies; i++ )
        {
            GameObject enemyCounter = Instantiate( enemyCounterPrefab, hud.transform );
            enemyCounter.transform.position = new Vector3( lastEnemyPosition.x - ( i * 0.3f ), lastEnemyPosition.y, 0 );
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
                enemyCounters[i].GetComponent<SpriteRenderer>().sprite = enemyCounterKilledSprite;
            }
            else
            {
                enemyCounters[i].GetComponent<SpriteRenderer>().sprite = enemyCounterAliveSprite;
            }
        }
    }

    private void DestroyTutorialText()
    {
        Text[] tutorialTextObjects = FindObjectsOfType<Text>();
        for ( int i = 0; i < tutorialTextObjects.Length; i++ )
        {
            Destroy( tutorialTextObjects[i].gameObject );
        }
    }

    #endregion

    #region Knight Functions

    private void KnightMovement()
    {
        float dt = Time.smoothDeltaTime;

        if ( Input.anyKey )
        {
            knight.transform.RotateAround( knight.transform.position, Vector3.forward, turnRatio * dt );
        }

        knight.transform.Translate( knight.transform.up * dt );
        trailGenerator.SetActive(true);

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
                if ( arrow.GetComponent<ArrowScript>().target == null )
                {
                    Destroy(arrow);
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
            bow.transform.localScale = Vector3.one * ( bowRange / maxBowRange );
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
            ChangeEnemyCountersDisplay();
            arrowsOnKnight--;
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
