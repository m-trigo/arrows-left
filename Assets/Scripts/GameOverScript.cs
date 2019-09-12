using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( elapsed > 10f || ( elapsed > 0.5f && Input.anyKey && elapsed > 10f ) )
        {
            SceneManager.LoadScene( "Start" );
        }
    }
}
