using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( elapsed > 10f || ( elapsed > 0.5f && Input.anyKey ) )
        {
            SceneManager.LoadScene( "Start" );
        }
    }
}
