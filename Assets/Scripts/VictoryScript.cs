using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( elapsed > 10f || ( Input.anyKeyDown && elapsed > 1 ) )
        {
            SceneManager.LoadScene( "Title" );
        }
    }
}
